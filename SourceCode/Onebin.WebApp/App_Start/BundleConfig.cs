using System.Web;
using System.Web.Optimization;

namespace Onebin.WebApp
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/Plugins").Include(
                        "~/Scripts/Plugins/jquery.min.js",
                        "~/Scripts/Plugins/EasyUI/jquery.easyui.min.js",
                        "~/Scripts/Plugins/EasyUI/locale/easyui-lang-zh_CN.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Scripts/Plugins/EasyUI/themes/metro/easyui.css"));
        }
    }
}