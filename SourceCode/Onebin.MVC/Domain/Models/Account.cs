using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leafing.Data.Definition;
using Leafing.Data;
using Onebin.Extra.Util;

namespace Onebin.MVC.Domain
{
    /// <summary>
    /// �ʻ�
    /// </summary>
    [SoftDelete]
    public class Account : DbObjectModel<Account>
    {
        /// <summary>
        /// ��¼ID
        /// </summary>
        public string LoginId { get; set; }

        /// <summary>
        /// ��¼����
        /// </summary>
        public string Password { get; set; }

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
        public string Memo { get; set; }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// ��¼����
        /// </summary>
        public int? LoginTimes { get; set; }

        /// <summary>
        /// �����¼ʱ��
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// �����¼IP
        /// </summary>
        [AllowNull]
        public string LastLoginIP { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        [SpecialName]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// �������ʱ��
        /// </summary>
        [SpecialName]
        public DateTime? UpdatedOn { get; set; }

        /// <summary>
        /// ������ɫ�б�
        /// </summary>
        [HasAndBelongsToMany(OrderBy = "Id")]
        public IList<Role> RoleList { get; private set; }

        private static string GetLoginCondition(string user)
        {
            string field = "LoginId";

            return field;
        }

        /// <summary>
        /// ��֤��¼
        /// </summary>
        /// <param name="loginId">��¼Id</param>
        /// <param name="password">����</param>
        /// <returns>��֤�ɹ������û�ʵ�������򷵻�Null</returns>
        public static Account VerifyLogin(string user, string password)
        {
            Account validAccount = Account.FindOne(x => x.LoginId == user);
            if (validAccount != null &&
                validAccount.IsEnabled == true &&
                MD5Encryption.VerifyMd5Hash(password, validAccount.Password))
            {
                return validAccount;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// У������
        /// </summary>
        /// <param name="accountId">�û�Id</param>
        /// <param name="password">ԭ����</param>
        /// <returns>У��ɹ�����True,���򷵻�False</returns>
        public static bool VerifyPassword(long accountId, string password)
        {
            Account validAccount = Account.FindById(accountId);
            if (validAccount != null && MD5Encryption.VerifyMd5Hash(password, validAccount.Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// �����ʻ�����
        /// </summary>
        /// <param name="accountId">�ʻ�Id</param>
        /// <param name="password">������</param>
        public static void UpdateAccountPassword(long accountId, string password)
        {
            Account account = Account.FindById(accountId);
            account.Password = MD5Encryption.Encode(password);
            account.Save();
        }

        /// <summary>
        /// �����ʻ���ɫ
        /// </summary>
        /// <param name="accountId">�ʻ�Id</param>
        /// <param name="roleIds">��ɫId�б�</param>
        /// <returns>ִ�гɹ�����true,���򷵻�false</returns>
        public static bool UpdateAccountRole(long accountId, string[] roleIds)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    DbEntry.Provider.ExecuteNonQuery(
                        string.Format("DELETE FROM dbo.Account_Role WHERE Account_Id = '{0}'", accountId));

                    if (roleIds.Length > 0)
                    {
                        string sql = "INSERT INTO dbo.Account_Role(Account_Id, Role_Id) ";
                        List<string> insertValue = new List<string>();
                        foreach (var roleId in roleIds)
                        {
                            insertValue.Add(string.Format("SELECT '{0}', '{1}'", accountId, roleId));
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
