using System;
using System.Collections.Generic;
using System.Linq;
using Leafing.Data;
using Onebin.MVC.Domain;
using Onebin.Extra.Dic;

namespace Onebin.MVC
{
    public class AuthorizationManager
    {
        private static AuthorizationManager instance = null;
        private static readonly object locker = new object();

        private List<Menu> menuDic = new List<Menu>();
        private List<Permission> permissionDic = new List<Permission>();

        private Dictionary<string, List<MenuItem>> userMenuDic = new Dictionary<string, List<MenuItem>>();
        private Dictionary<string, string[]> userFuncDic = new Dictionary<string, string[]>();

        private Dictionary<string, List<Role>> roleDic = new Dictionary<string, List<Role>>();

        private AuthorizationManager()
        {
            menuDic = Menu.FindAll();
            permissionDic = Permission.FindAll();
        }

        /// <summary>
        /// 获取AuthorizationManager的唯一实例
        /// </summary>
        /// <returns></returns>
        public static AuthorizationManager GetInstance()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new AuthorizationManager();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// 获取RoleKey
        /// </summary>
        /// <param name="roleList"></param>
        /// <returns></returns>
        public string GetRoleKey(IEnumerable<Role> roleList)
        {
            string roleKey = string.Join(GlobalConstant.ROLE_KEY_DIVIDER.ToString(), roleList.OrderBy(x => x.Id).Select(x => x.Id));

            if (!roleDic.ContainsKey(roleKey))
            {
                roleDic.Add(roleKey, roleList.ToList());
            }

            return roleKey;
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="roleKey">roleKey</param>
        /// <returns>角色列表</returns>
        private List<Role> GetRoleList(string roleKey)
        {
            List<Role> roleLst = null;
            var roleIdLst = roleKey.Split(GlobalConstant.ROLE_KEY_DIVIDER).Select(x => long.Parse(x)).ToArray();
            if (roleIdLst.Length > 0)
            {
                roleLst = Role.Find(x => x.Id.In(roleIdLst));
            }
            return roleLst;
        }

        /// <summary>
        /// 获取用户菜单
        /// </summary>
        /// <param name="roleKey">roleKey</param>
        /// <returns>用户菜单项</returns>
        public List<MenuItem> GetMenu(string roleKey)
        {
            List<MenuItem> returnVal = null;
            if (!userMenuDic.TryGetValue(roleKey, out returnVal))
            {
                returnVal = BuildMenu(roleKey);
                userMenuDic.Add(roleKey, returnVal);
            }
            return returnVal;
        }

        /// <summary>
        /// 获取子菜单
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <param name="roleKey">用户角色标识</param>
        /// <returns>菜单项</returns>
        public List<MenuItem> GetSubMenu(string MenuId, string roleKey)
        {
            List<MenuItem> returnVal = null;
            returnVal = userMenuDic.Where(e => e.Key == roleKey).First().Value
                .Where(e => e.ParentId == MenuId).Select(e => e).ToList<MenuItem>();
            return returnVal;
        }

        /// <summary>
        /// 建立菜单字典并返回用户菜单项
        /// </summary>
        /// <param name="roleKey">roleKey</param>
        /// <returns>用户菜单项</returns>
        private List<MenuItem> BuildMenu(string roleKey)
        {
            Dictionary<string, MenuItem> menuAccumulator = new Dictionary<string, MenuItem>();

            foreach (var role in GetRoleList(roleKey))
            {
                foreach (var permission in role.PermissionList)
                {
                    if (menuAccumulator.ContainsKey(permission.MenuId))
                    {
                        continue;
                    }
                    else
                    {
                        var permissionMenu = menuDic.Where(e => e.Id == permission.MenuId).First();
                        MenuItem item = new MenuItem()
                        {
                            Id = permission.MenuId,
                            ParentId = permissionMenu.ParentId,
                            Name = permissionMenu.Name,
                            Url = permissionMenu.Url,
                            Category = permissionMenu.Category,
                            Level = permissionMenu.Level,
                            Tier =  0
                        };

                        menuAccumulator.Add(permission.MenuId, item);
                    }
                }
            }

            var subMenuHasParent = menuAccumulator.Where(e => e.Value.ParentId != string.Empty)
                .Select(e => e.Value).ToArray();

            if (subMenuHasParent.Count() > 0)
            {
                GetParentMenu(subMenuHasParent, menuAccumulator, 0);
            }

            List<MenuItem> returnVal = menuAccumulator.Values.ToList();
            return returnVal;
        }

        /// <summary>
        /// 获取父菜单
        /// </summary>
        /// <param name="subMenu">子菜单</param>
        /// <param name="menuAccumulator">累加菜单</param>
        private void GetParentMenu(IEnumerable<MenuItem> subMenu, IDictionary<string, MenuItem> menuAccumulator, int tier)
        {
            tier = ++tier;
            foreach (var item in subMenu)
            {
                if (!menuAccumulator.ContainsKey(item.ParentId))
                {
                    var menu = (from e in menuDic where (e.Id.Equals(item.ParentId)) select e).First();

                    MenuItem menuItem = new MenuItem();
                    menuItem.Id = menu.Id;
                    menuItem.ParentId = menu.ParentId;
                    menuItem.Name = menu.Name;
                    menuItem.Url = menu.Url;
                    menuItem.Category = menu.Category;
                    menuItem.Level = menu.Level;
                    menuItem.Tier = tier;

                    menuAccumulator.Add(menuItem.Id, menuItem);
                }
            }

            var subMenuHasParent = menuAccumulator.Where(e => e.Value.ParentId != string.Empty);
            var parentMenuNotInAccumulator = subMenuHasParent
                .Where(e => !menuAccumulator.ContainsKey(e.Value.ParentId))
                .Select(e => e.Value).ToArray();

            if (parentMenuNotInAccumulator.Count() > 0)
            {
                GetParentMenu(parentMenuNotInAccumulator, menuAccumulator, tier);
            }
        }

        /// <summary>
        /// 获取用户所拥有的操作权限Id
        /// </summary>
        /// <param name="roleKey">角色Id列表</param>
        /// <returns>用户所拥有的操作权限Id</returns>
        public string[] GetFunc(string roleKey)
        {
            string[] func = null;
            if (!userFuncDic.TryGetValue(roleKey, out func))
            {
                func = BuildFunc(roleKey);
                userFuncDic.Add(roleKey, func);
            }
            return func;
        }

        /// <summary>
        /// 建立权限字典并返回用户操作权限Id列表
        /// </summary>
        /// <param name="roleKey">角色Id列表</param>
        /// <returns>用户所拥有的操作权限Id</returns>
        private string[] BuildFunc(string roleKey)
        {
            IList<string> funcAccumulator = new List<string>();

            foreach (var role in GetRoleList(roleKey))
            {
                foreach (var permission in role.PermissionList)
                {
                    if (funcAccumulator.Contains(permission.Id))
                    {
                        continue;
                    }
                    else
                    {
                        funcAccumulator.Add(permission.Id);
                    }
                }
            }
            return funcAccumulator.ToArray();
        }

        /// <summary>
        /// 验证用户角色是否拥有指定的操作权限
        /// </summary>
        /// <param name="permissionId">权限Id</param>
        /// <param name="roleKey">角色Id列表</param>
        /// <returns>用户角色拥有指定操作权限返回true,否则反之</returns>
        public bool VerifyPermission(string permissionId, string roleKey)
        {
            return GetFunc(roleKey).Contains(permissionId);
        }

        /// <summary>
        /// 根据权限Id获取权限实例
        /// </summary>
        /// <param name="permissionId">权限Id</param>
        /// <returns>权限实例</returns>
        public Permission GetPermission(string permissionId)
        {
            var permissionList = permissionDic.Where(e => e.Id == permissionId).Take(1);
            if (permissionList != null && permissionList.Count() == 1)
            {
                return permissionList.First();
            }
            return null;
        }

        /// <summary>
        /// 重新设定角色的功能权限
        /// </summary>
        /// <param name="roldId">角色Id</param>
        public void RebuildFuncAndMenuDic(long roldId)
        {
            var userMenuDicRebuildItem = userMenuDic.Where(e => e.Key.Split(GlobalConstant.ROLE_KEY_DIVIDER).Contains(roldId.ToString())).Select(e => e.Key).ToArray();
            var userFuncDicRebuildItem = userFuncDic.Where(e => e.Key.Split(GlobalConstant.ROLE_KEY_DIVIDER).Contains(roldId.ToString())).Select(e => e.Key).ToArray();

            foreach (var removeItem in userMenuDicRebuildItem)
            {
                userMenuDic.Remove(removeItem);
            }
            foreach (var removeItem in userFuncDicRebuildItem)
            {
                userFuncDic.Remove(removeItem);
            }
        }
    }
}
