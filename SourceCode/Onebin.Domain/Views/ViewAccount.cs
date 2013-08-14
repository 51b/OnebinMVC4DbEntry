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
        /// ��¼ID
        /// </summary>
        [DbColumn("Account.LoginId")]
        public string LoginId { get; set; }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        [DbColumn("Account.IsEnabled")]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// ��¼����
        /// </summary>
        [DbColumn("Account.LoginTimes")]
        public int? LoginTimes { get; set; }

        /// <summary>
        /// �����¼ʱ��
        /// </summary>
        [DbColumn("Account.LastLoginTime")]
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// �����¼IP
        /// </summary>
        [AllowNull]
        [DbColumn("Account.LastLoginIP")]
        public string LastLoginIP { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        [SpecialName]
        [DbColumn("Account.CreatedOn")]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// ���ʻ�������ʱ��
        /// </summary>
        [SpecialName]
        [DbColumn("Account.UpdatedOn")]
        public DateTime? UpdatedOn { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [AllowNull]
        [DbColumn("AccountDetail.Name")]
        public string Name { get; set; }

        /// <summary>
        /// �Ա�
        /// </summary>
        [DbColumn("AccountDetail.Sex")]
        public int? Sex { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [AllowNull]
        [DbColumn("AccountDetail.Email")]
        public string Email { get; set; }

        /// <summary>
        /// �ֻ�
        /// </summary>
        [AllowNull]
        [DbColumn("AccountDetail.Phone")]
        public string Phone { get; set; }

        /// <summary>
        /// �˻�����
        /// </summary>
        [AllowNull]
        [DbColumn("AccountDetail.Desc")]
        public string Desc { get; set; }

        /// <summary>
        /// ���ʻ����飩����ʱ��
        /// </summary>
        [SpecialName]
        [DbColumn("AccountDetail.SavedOn")]
        public DateTime SavedOn { get; set; }
    }
}
