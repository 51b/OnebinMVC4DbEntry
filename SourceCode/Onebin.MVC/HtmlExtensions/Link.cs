using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Onebin.MVC.Domain;
using System.Web.Routing;
using Onebin.MVC;

namespace System.Web.Mvc.Html
{
    public static partial class HtmlExtensions
    {
        /// <summary>
        /// 基本原型：&lt;a href="[ 权限所对应的Action链接 ]"&gt;[ 权限名称 | { InnerHtml } ]&lt;/a&gt;
        /// </summary>
        /// <param name="tagparams">可选参数：Attributes/ClassName/InnerHtml</param>
        /// <returns></returns>
        public static MvcHtmlString TagActionLink(
            this HtmlHelper helper,
            TagParams tagParams
            )
        {
            if (tagParams.VerifyPermission())
            {
                Permission permission = AuthorizationManager.GetInstance().GetPermission(tagParams.PermissionId);
                var attrs = new RouteValueDictionary(tagParams.Attributes);
                attrs.Add("href", permission.Action + tagParams.Params);
                return HtmlBuilder(TagType.a, null, attrs, tagParams.ClassName, string.IsNullOrWhiteSpace(tagParams.InnerHtml) ? permission.Name : tagParams.InnerHtml);
            }
            return null;
        }

        /// <summary>
        /// 基本原型：&lt;a href="[ 权限所对应的菜单链接 ]"&gt;[ 权限所对应的菜单名称 | { InnerHtml } ]&lt;/a&gt;
        /// </summary>
        /// <param name="tagparams">可选参数：TagId/Attributes/ClassName/InnerHtml</param>
        /// <returns>a标签</returns>
        public static MvcHtmlString TagMenuLink(
            this HtmlHelper helper,
            TagParams tagParams
            )
        {
            if (tagParams.VerifyPermission())
            {
                Permission permission = AuthorizationManager.GetInstance().GetPermission(tagParams.PermissionId);
                var attrs = new RouteValueDictionary(tagParams.Attributes);
                attrs.Add("href", permission.Menu.Url + tagParams.Params);
                return HtmlBuilder(TagType.a, tagParams.TagId, attrs, tagParams.ClassName, string.IsNullOrWhiteSpace(tagParams.InnerHtml) ? permission.Menu.Name : tagParams.InnerHtml);
            }
            return null;
        }


        /// <summary>
        /// <para> 基本原型：&lt;a id="{ TagId }" actionUrl="[ 权限所对应的Action ]" href="javascript:{ Script }"&gt;"</para>
        /// <para> [ 权限名称 | { InnerHtml } ] &lt;/a&gt;</para>
        /// </summary>
        /// <param name="tagParams">必须参数：TagId/Script，可选参数：Attributes/ClassName/InnerHtml</param>
        /// <returns>a标签</returns>
        public static MvcHtmlString TagScriptLink(
            this HtmlHelper helper,
            TagParams tagParams
            )
        {
            if (tagParams.VerifyPermission())
            {
                Permission permission = AuthorizationManager.GetInstance().GetPermission(tagParams.PermissionId);
                var attrs = new RouteValueDictionary(tagParams.Attributes);
                attrs.Add("actionUrl", permission.Action);
                attrs.Add("href", "javascript:" + tagParams.Script);
                return HtmlBuilder(TagType.a, tagParams.TagId, attrs, tagParams.ClassName, string.IsNullOrWhiteSpace(tagParams.InnerHtml) ? permission.Name : tagParams.InnerHtml);
            }
            return null;
        }

        /// <summary>
        /// 生成一个模板链接，然后使用$("#tagId").html().replace("[params]", "参数列表").replace("[text]", "显示文本")生成可替换的链接;
        /// </summary>
        /// <param name="tagParams">必须值：TagId，可选值：Attributes</param>
        /// <returns></returns>
        public static MvcHtmlString TagPlaceholderLink(
            this HtmlHelper helper,
            TagParams tagParams
            )
        {
            if (tagParams.VerifyPermission())
            {
                Permission permission = AuthorizationManager.GetInstance().GetPermission(tagParams.PermissionId);
                var attrs = new RouteValueDictionary(tagParams.Attributes);
                attrs.Add("href", permission.Action + "[params]");
                string innerHtml = HtmlBuilder(TagType.a, null, attrs, string.Empty, "[text]").ToHtmlString();
                return HtmlBuilder(TagType.div, tagParams.TagId, new { style = "display: none;" }, null, innerHtml);
            }
            return null;
        }
    }
}
