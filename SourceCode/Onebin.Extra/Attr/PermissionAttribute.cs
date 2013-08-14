using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Onebin.Extra.Attr
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class PermissionAttribute : Attribute
    {
        public PermissionAttribute() { }

        public PermissionAttribute(string id)
        {
            this.Id = id;
            this.Dependent = true;
        }

        public PermissionAttribute(string id, string name)
        {
            this.Id = id;
            this.Name = name;
            Match matchResults = Regex.Match(Id, @"^\D*\d*");
            this.MenuId = matchResults.Success ? matchResults.Value : null;
        }

        public PermissionAttribute(string id, string name, string action)
            : this(id, name)
        {
            this.Action = action;
        }

        public string Id { get; set; }

        public string MenuId { get; set; }

        public string Name { get; set; }

        public string Action { get; set; }

        public bool UnverifyByFilter { get; set; }

        public bool Dependent { get; private set; }
    }
}
