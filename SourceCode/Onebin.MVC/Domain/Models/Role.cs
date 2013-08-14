using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leafing.Data.Definition;
using Leafing.Data;
using Leafing.Core.Setting;

namespace Onebin.MVC.Domain
{
    /// <summary>
    /// 角色
    /// </summary>
    [SoftDelete]
    public class Role : DbObjectModel<Role>
    {
        private readonly Lazy<List<Permission>> _permissionList;

        public Role()
        {
            _permissionList = new Lazy<List<Permission>>(() => DbEntry.ExecuteList<Permission>(
                    "SELECT * FROM [Permission] WHERE [Id] IN (" +
                        "SELECT [Permission_Id] FROM [Role_Permission] WHERE [Role_Id] = ?)", this.Id));
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 主页
        /// </summary>
        [AllowNull]
        public string HomePage { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        [AllowNull]
        public string Memo { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Ordinal { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SpecialName]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// 最近更新时间
        /// </summary>
        [SpecialName]
        public DateTime? UpdatedOn { get; set; }

        /// <summary>
        /// 用户列表
        /// </summary>
        [HasAndBelongsToMany]
        public IList<Account> AccountList { get; private set; }

        /// <summary>
        /// 权限列表
        /// </summary>
        [Exclude]
        public List<Permission> PermissionList
        {
            get
            {
                return _permissionList.Value;
            }
        }

        public static bool UpdatePermission(long roleId, string[] permissionIds)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    DbEntry.Provider.ExecuteNonQuery(
                        string.Format("DELETE FROM dbo.Permission_Role WHERE Role_Id = '{0}'", roleId));

                    if (permissionIds.Length > 0)
                    {
                        string sql = "INSERT INTO dbo.Permission_Role(Role_Id, Permission_Id) ";
                        List<string> insertValue = new List<string>();
                        foreach (var permissionId in permissionIds)
                        {
                            insertValue.Add(string.Format("SELECT '{0}', '{1}'", roleId, permissionId));
                        }
                        sql += string.Join(" UNION ", insertValue);
                        DbEntry.Provider.ExecuteNonQuery(sql);
                    }
                });
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
