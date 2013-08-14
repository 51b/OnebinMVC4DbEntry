using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leafing.Data.Definition;
using Leafing.Data;

namespace Onebin.MVC.Domain
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class Menu : IDbObjectModelHelper<Menu, string>
    {

        private readonly Lazy<Menu> _parent;
        private readonly Lazy<IList<Permission>> _permissionList;

        public Menu()
        {
            _parent = new Lazy<Menu>(() => ParentId == null ? null : Menu.FindById(this.ParentId));
            _permissionList = new Lazy<IList<Permission>>(() => 
                DbEntry.From<Permission>()
                    .Where(e => e.MenuId == this.Id)
                    .Select(e => e).ToList());
        }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父菜单Id
        /// </summary>
        [AllowNull]
        public string ParentId { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        [AllowNull]
        public string Url { get; set; }

        /// <summary>
        /// 菜单类别
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 菜单级别
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 上一级菜单
        /// </summary>
        [Exclude]
        public Menu Parent { get { return _parent.Value; } }

        /// <summary>
        /// 权限列表
        /// </summary>
        [Exclude]
        public IList<Permission> PermissionList { get { return _permissionList.Value; } }
    }
}
