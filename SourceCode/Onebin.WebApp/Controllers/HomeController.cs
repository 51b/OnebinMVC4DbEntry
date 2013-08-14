using Onebin.Extra.Attr;
using Onebin.MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Onebin.WebApp.Controllers
{
    [Menu("M01", "首页", "/Home/Index")]
    public class HomeController : AuthorizationController
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
