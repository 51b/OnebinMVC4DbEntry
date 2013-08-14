using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using Onebin.Extra.Attr;
using Onebin.Extra.Dic;

namespace Onebin.MVC.Filter
{
    public class AuthorizationFilter : AuthorizeAttribute
    {
        private HttpContextBase _httpContext;

        public AuthorizationFilter() { }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }
            _httpContext = httpContext;
            return true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (!HttpContext.Current.Request.IsAuthenticated)
            {
                filterContext.Result = RedirectLoginPage();
            }
            else
            {
                VerifyPermission(filterContext);
            }
        }

        private void VerifyPermission(AuthorizationContext filterContext)
        {
            object[] attrs = filterContext.ActionDescriptor.GetCustomAttributes(false);

            for (int i = 0; i < attrs.Length; i++)
            {
                object attr = attrs[i];
                if (attr is PermissionAttribute)
                {
                    PermissionAttribute permission = attr as PermissionAttribute;
                    if (!permission.UnverifyByFilter)
                    {
                        string roleKey;
                        if (IsUserSessionOutOfDate(filterContext, out roleKey))
                        {
                            filterContext.Result = RedirectLoginPage();
                        }
                        else if (!AuthorizationManager.GetInstance().VerifyPermission(permission.Id, roleKey))
                        {
                            _httpContext.Response.StatusCode = 403;
                            filterContext.Result = new ViewResult() { ViewName = "NoPermission" };
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 判断用户Session是否过期
        /// </summary>
        public bool IsUserSessionOutOfDate(AuthorizationContext filterContext, out string roleKey)
        {
            roleKey = filterContext.HttpContext.Session[GlobalConstant.ROLE_KEY] as string;
            return roleKey == null;
        }

        /// <summary>
        /// 重定向到登录页
        /// </summary>
        public ActionResult RedirectLoginPage()
        {
            return new RedirectToRouteResult("Default",
                new RouteValueDictionary(
                    new
                    {
                        controller = "Login",
                        action = "Logout"
                    }
                )
            );
        }
    }
}
