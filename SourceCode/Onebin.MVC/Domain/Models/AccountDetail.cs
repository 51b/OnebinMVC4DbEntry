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
        /// ����
        /// </summary>
        [AllowNull]
        public string Name { get; set; }

        /// <summary>
        /// �Ա�
        /// </summary>
        public int? Sex { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [AllowNull]
        public string Email { get; set; }

        /// <summary>
        /// �ֻ�
        /// </summary>
        [AllowNull]
        public string Phone { get; set; }

        /// <summary>
        /// �û�����
        /// </summary>
        [AllowNull]
        public string Desc { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        [SpecialName]
        public DateTime SavedOn { get; set; }

        /// <summary>
        /// �ʻ���Ϣ
        /// </summary>
        [BelongsTo]
        public Account Account { get; set; }

    }
}
