using Leafing.Core.Logging;
using Leafing.Data;
using Onebin.Domain.Views;
using Onebin.Extra.Attr;
using Onebin.Extra.Dic;
using Onebin.Extra.Util;
using Onebin.MVC.Controllers;
using Onebin.MVC.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Onebin.WebApp.Areas.SystemSettings.Controllers
{
    [Menu("M0201", "帐户管理", "/SystemSettings/Account/Index")]
    public class AccountController : AuthorizationController
    {
        [Permission("M0201P01", "页面访问")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
