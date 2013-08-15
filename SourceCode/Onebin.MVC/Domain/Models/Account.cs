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
    /// 帐户
    /// </summary>
    [SoftDelete]
    public class Account : DbObjectModel<Account>
    {
        /// <summary>
        /// 登录ID
        /// </summary>
        public string LoginId { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

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
        public string Memo { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public int? LoginTimes { get; set; }

        /// <summary>
        /// 最近登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 最近登录IP
        /// </summary>
        [AllowNull]
        public string LastLoginIP { get; set; }

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
        /// 所属角色列表
        /// </summary>
        [HasAndBelongsToMany(OrderBy = "Id")]
        public IList<Role> RoleList { get; private set; }

        private static string GetLoginCondition(string user)
        {
            string field = "LoginId";

            return field;
        }

        /// <summary>
        /// 验证登录
        /// </summary>
        /// <param name="loginId">登录Id</param>
        /// <param name="password">密码</param>
        /// <returns>验证成功返回用户实例，否则返回Null</returns>
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
        /// 校验密码
        /// </summary>
        /// <param name="accountId">用户Id</param>
        /// <param name="password">原密码</param>
        /// <returns>校验成功返回True,否则返回False</returns>
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
        /// 更新帐户密码
        /// </summary>
        /// <param name="accountId">帐户Id</param>
        /// <param name="password">新密码</param>
        public static void UpdateAccountPassword(long accountId, string password)
        {
            Account account = Account.FindById(accountId);
            account.Password = MD5Encryption.Encode(password);
            account.Save();
        }

        /// <summary>
        /// 更新帐户角色
        /// </summary>
        /// <param name="accountId">帐户Id</param>
        /// <param name="roleIds">角色Id列表</param>
        /// <returns>执行成功返回true,否则返回false</returns>
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
