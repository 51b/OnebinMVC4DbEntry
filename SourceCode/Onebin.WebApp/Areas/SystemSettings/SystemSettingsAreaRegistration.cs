using Onebin.Extra.Attr;
using System.Web.Mvc;

namespace Onebin.WebApp.Areas.SystemSettings
{
    [Menu("M02","系统设置")]
    public class SystemSettingsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SystemSettings";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SystemSettings_default",
                "SystemSettings/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
