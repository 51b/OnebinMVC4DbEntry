using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onebin.MVC.Domain
{
    /// <summary>
    /// 菜单项
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// 菜单项Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 父菜单项Id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 菜单项名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单项链接地址
        /// </summary>
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
        /// 层级
        /// </summary>
        public int Tier { get; set; }
    }
}
