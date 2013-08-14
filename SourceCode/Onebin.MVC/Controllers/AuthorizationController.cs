using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Onebin.MVC.Filter;

namespace Onebin.MVC.Controllers
{
    [AuthorizationFilter]
    public class AuthorizationController : BaseController
    {
    }
}
