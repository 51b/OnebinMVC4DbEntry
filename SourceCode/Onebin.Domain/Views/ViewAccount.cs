using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leafing.Data.Definition;
using Onebin.MVC.Domain;

namespace Onebin.Domain.Views
{
    [SoftDelete]
    [JoinOn(0,typeof(Account), "Id", typeof(AccountDetail), "account_Id", JoinMode.Left)]
    public class ViewAccount : IDbObject
    {
        [DbColumn("Account.Id")]
        public long Id { get; set; }

        /// <summary>
        /// 登录ID
        /// </summary>
        [DbColumn("Account.LoginId")]
        public string LoginId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [DbColumn("Account.IsEnabled")]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        [DbColumn("Account.LoginTimes")]
        public int? LoginTimes { get; set; }

        /// <summary>
        /// 最近登录时间
        /// </summary>
        [DbColumn("Account.LastLoginTime")]
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 最近登录IP
        /// </summary>
        [AllowNull]
        [DbColumn("Account.LastLoginIP")]
        public string LastLoginIP { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SpecialName]
        [DbColumn("Account.CreatedOn")]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// （帐户）更新时间
        /// </summary>
        [SpecialName]
        [DbColumn("Account.UpdatedOn")]
        public DateTime? UpdatedOn { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [AllowNull]
        [DbColumn("AccountDetail.Name")]
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DbColumn("AccountDetail.Sex")]
        public int? Sex { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [AllowNull]
        [DbColumn("AccountDetail.Email")]
        public string Email { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [AllowNull]
        [DbColumn("AccountDetail.Phone")]
        public string Phone { get; set; }

        /// <summary>
        /// 账户描述
        /// </summary>
        [AllowNull]
        [DbColumn("AccountDetail.Desc")]
        public string Desc { get; set; }

        /// <summary>
        /// （帐户详情）更新时间
        /// </summary>
        [SpecialName]
        [DbColumn("AccountDetail.SavedOn")]
        public DateTime SavedOn { get; set; }
    }
}
