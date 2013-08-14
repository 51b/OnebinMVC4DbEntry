using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onebin.MVC.Domain
{
    public class UserPassword
    {
        /// <summary>
        /// 登录帐户名称
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 旧密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        public string ConfirmPassword { get; set; }
    }
}
