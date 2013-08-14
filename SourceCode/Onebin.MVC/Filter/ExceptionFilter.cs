using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Onebin.Extra.Dic;
using Leafing.Core.Logging;

namespace Onebin.MVC.Filter
{
    public class ExceptionFilter : FilterAttribute, IExceptionFilter
    {
        void IExceptionFilter.OnException(ExceptionContext filterContext)
        {
            filterContext.Controller.ViewData[GlobalConstant.ERROR_MSG] = filterContext.Exception.Message;
            Logger.System.Error(filterContext.Exception);
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error",
                ViewData = filterContext.Controller.ViewData,
            };
            filterContext.ExceptionHandled = true;
        }
    }  
}
