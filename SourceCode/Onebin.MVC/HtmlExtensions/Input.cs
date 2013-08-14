using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Onebin.MVC;
using System.Web.Routing;
using Onebin.MVC.Domain;

namespace System.Web.Mvc.Html
{
    public static partial class HtmlExtensions
    {

        /// <summary>
        /// 判断用户是否具有指定权限，当不具体该权限时，则把用className作为标识的标签元素所包含Html内容清空，
        /// 否则，将生成一个隐藏的Input，并根据权限Id把值设置为相对应的Action链接地址
        /// 注：建议把生成的内容包含在指定标签元素内部
        /// </summary>
        /// <param name="tagParams">必须参数：TagId/ClassName</param>
        /// <returns></returns>
        public static MvcHtmlString TagProtectArea(
            this HtmlHelper helper,
            TagParams tagParams
            )
        {
            if (tagParams.VerifyPermission())
            {
                Permission permission = AuthorizationManager.GetInstance().GetPermission(tagParams.PermissionId);
                return HtmlBuilder(TagType.input, tagParams.TagId, new { value = permission.Action, type = "hidden" }, null, null);
            }
            else
            {
                string script = "$(function() { $('." + tagParams.ClassName + "').html('');});";
                return HtmlBuilder(TagType.script, null, new { type = "text/javascript" }, null, script);
            }
        }

        /// <summary>
        /// 生成一个隐藏的Input，并根据权限Id把值设置为相对应的Action链接地址
        /// <para>
        /// 基本原型：&lt;input type="hidden" id="{ TagId }" value="[ 权限所对应的Action ]" /&gt;
        /// </para>
        /// </summary>
        /// <param name="tagParams">必须参数：TagId</param>
        /// <returns></returns>
        public static MvcHtmlString TagHiddenActionUrl(
            this HtmlHelper helper,
            TagParams tagParams
            )
        {
            if (tagParams.VerifyPermission())
            {
                Permission permission = AuthorizationManager.GetInstance().GetPermission(tagParams.PermissionId);
                Object attrs = new { value = permission.Action, type = "hidden" };
                return HtmlBuilder(TagType.input, tagParams.TagId, attrs, null, null);
            }
            return null;
        }
    }
}
