using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Onebin.MVC;
using Onebin.MVC.Domain;
using Onebin.Extra.Dic;

namespace System.Web.Mvc.Html
{
    public static partial class HtmlExtensions
    {
        /// <summary>
        /// 生成用户定制的标签元素
        /// </summary>
        /// <param name="roleIdList">用户Id</param>
        /// <param name="permissionId">权限Id</param>
        /// <param name="tagType">标签类型</param>
        /// <param name="tagId">标签Id</param>
        /// <param name="attributes">属性</param>
        /// <param name="className">样式</param>
        /// <param name="innerHtml">内部Html</param>
        /// <returns>tagType指定的标签元素</returns>
        public static MvcHtmlString Tag(
            this HtmlHelper helper,
            string roleIdList,
            string permissionId,
            TagType tagType,
            string tagId,
            object attributes,
            string className,
            string innerHtml
            )
        {
            if (AuthorizationManager.GetInstance().VerifyPermission(permissionId, roleIdList))
            {
                return HtmlBuilder(tagType, tagId, attributes, className, innerHtml);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 构建Html
        /// </summary>
        /// <param name="tagType">标签类型</param>
        /// <param name="tagId">标签Id</param>
        /// <param name="attributes">属性</param>
        /// <param name="className">样式</param>
        /// <param name="innerHtml">内部Html</param>
        /// <returns>标签元素</returns>
        private static MvcHtmlString HtmlBuilder(
            TagType tagType,
            string tagId,
            object attributes,
            string className,
            string innerHtml
            )
        {
            var attrs = new RouteValueDictionary(attributes);
            return HtmlBuilder(tagType, tagId, attrs, className, innerHtml);
        }

        /// <summary>
        /// 构建Html
        /// </summary>
        /// <param name="tagType">标签类型</param>
        /// <param name="tagId">标签Id</param>
        /// <param name="attributes">属性</param>
        /// <param name="className">样式</param>
        /// <param name="innerHtml">内部Html</param>
        /// <returns>标签元素</returns>
        private static MvcHtmlString HtmlBuilder(
            TagType tagType,
            string tagId,
            RouteValueDictionary attributes,
            string className,
            string innerHtml
            )
        {

            TagBuilder builder = new TagBuilder(tagType.ToString());
            builder.IdAttributeDotReplacement = "_";
            if (!string.IsNullOrWhiteSpace(tagId))
            {
                builder.GenerateId(tagId);
            }
            if (!attributes.ContainsKey("class"))
            {
                if (!string.IsNullOrWhiteSpace(className))
                {
                    builder.AddCssClass(className);
                }
            }
            builder.MergeAttributes(attributes);
            builder.InnerHtml = innerHtml;
            return MvcHtmlString.Create(builder.ToString());
        }
    }

    /// <summary>
    /// 标签类型
    /// </summary>
    public enum TagType
    {
        a,
        input,
        button,
        div,
        span,
        li,
        script
    }

    /// <summary>
    /// 封装了Tag所需的参数信息
    /// </summary>
    public class TagParams
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="roleIdList">角色Id列表</param>
        /// <param name="permissionId">权限Id</param>
        public TagParams(string permissionId)
        {
            this.PermissionId = permissionId;
        }

        /// <summary>
        /// 角色ID列表
        /// </summary>
        public string RoleIdList { get; set; }

        /// <summary>
        /// 权限Id
        /// </summary>
        public string PermissionId { get; set; }

        /// <summary>
        /// 标签类型
        /// </summary>
        public TagType TagType { get; set; }

        /// <summary>
        /// 标签Id
        /// </summary>
        public string TagId { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public object Attributes { get; set; }

        /// <summary>
        /// 样式
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 内部Html
        /// </summary>
        public string InnerHtml { get; set; }

        /// <summary>
        /// 脚本
        /// </summary>
        public string Script { get; set; }

        /// <summary>
        /// 附加参数
        /// </summary>
        public string Params { get; set; }

        /// <summary>
        /// 验证权限
        /// </summary>
        /// <returns></returns>
        public bool VerifyPermission()
        {
            return AuthorizationManager.GetInstance().VerifyPermission(this.PermissionId, HttpContext.Current.Session[GlobalConstant.ROLE_KEY].ToString());
        }
    }
}
