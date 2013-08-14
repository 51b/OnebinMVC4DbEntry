using Onebin.Extra.Dic;
using Onebin.Extra.Util;
using Onebin.MVC;
using Onebin.MVC.Controllers;
using Onebin.MVC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Onebin.WebApp.Controllers
{
    public class LoginController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Account account, string returnUrl)
        {
            if (!account.LoginId.IsNullOrEmpty() && !account.Password.IsNullOrEmpty())
            {
                Account validAccount = Account.VerifyLogin(account.LoginId, account.Password);
                if (validAccount != null)
                {
                    InitLoginStatus(validAccount);
                    return RedirectToURL(returnUrl);
                }
                TempData["ReturnData"] = "用户名或密码有误";
            }
            else
            {
                TempData["ReturnData"] = "请输入用户名和密码";
            }
            return View(account);
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectToURL("/Login/Index?ReturnUrl=%2f");
        }

        private void InitLoginStatus(Account account)
        {
            var authorizationManager = AuthorizationManager.GetInstance();
            string roleKey = authorizationManager.GetRoleKey(account.RoleList);
            Session[GlobalConstant.CURRENT_ACCOUNT] = account;
            Session[GlobalConstant.ROLE_KEY] = roleKey;
            Session[GlobalConstant.ACCOUNT_MENU] = authorizationManager.GetMenu(roleKey);
            account.LoginTimes = account.LoginTimes == null ? 1 : account.LoginTimes.Value + 1;
            account.LastLoginTime = DateTime.Now;
            account.LastLoginIP = IPAddress.GetIPAddress();
            account.Save();
            FormsAuthentication.SetAuthCookie(roleKey, false);
        }

        private ActionResult RedirectToURL(string url)
        {
            if (Url.IsLocalUrl(url))
            {
                return Redirect(url);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
