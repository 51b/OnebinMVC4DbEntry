using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leafing.Data.Definition;
using Leafing.Data;

namespace Onebin.MVC.Domain
{
    /// <summary>
    /// 权限
    /// </summary>
    public class Permission : IDbObjectModelHelper<Permission, string>
    {
        private readonly Lazy<Menu> _menu;

        public Permission()
        {
            _menu = new Lazy<Menu>(() => Menu.FindById(this.MenuId));
        }

        /// <summary>
        /// 权限所属菜单
        /// </summary>
        [Exclude]
        public Menu Menu
        {
            get { return _menu.Value; }

        }

        public string MenuId { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 操作(URL)
        /// </summary>
        public string Action { get; set; }
    }
}
