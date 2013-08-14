using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leafing.Data.Definition;

namespace Onebin.MVC.Domain
{
    public partial class AccountDetail : DbObjectModel<AccountDetail>
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [AllowNull]
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int? Sex { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [AllowNull]
        public string Email { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [AllowNull]
        public string Phone { get; set; }

        /// <summary>
        /// 用户描述
        /// </summary>
        [AllowNull]
        public string Desc { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [SpecialName]
        public DateTime SavedOn { get; set; }

        /// <summary>
        /// 帐户信息
        /// </summary>
        [BelongsTo]
        public Account Account { get; set; }

    }
}
