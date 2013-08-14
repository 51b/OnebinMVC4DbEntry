using Onebin.Extra.Attr;
using Onebin.MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Onebin.WebApp.Areas.SystemSettings.Controllers
{
    [Menu("M0202", "角色管理", "/SystemSettings/Role/Index")]
    public class RoleController : AuthorizationController
    {
        [Permission("M0202P01", "页面访问")]
        public ActionResult Index()
        {
            return View();
        }

    }
}
