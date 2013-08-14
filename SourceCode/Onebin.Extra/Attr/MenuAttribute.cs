using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Onebin.Extra.Attr
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class MenuAttribute : Attribute
    {
        public MenuAttribute() { }

        public MenuAttribute(string id, string name)
        {
            this.Id = id;
            this.Name = name;
            Match categroyMatch = Regex.Match(Id, @"^\D*");
            Regex level = new Regex(@"\d{2}");
            this.Category = categroyMatch.Success ? categroyMatch.Value : null;
            this.Level = level.Matches(id).Count;
            if (this.Level > 1)
            {
                this.ParentId = this.Id.Substring(0, this.Id.Length - 2);
            }
        }

        public MenuAttribute(string id, string name, string url)
            :this(id,name)
        {
            this.Url = url;
        }

        public MenuAttribute(string id, string name, string url, string parentId)
            : this(id, name, url)
        {
            this.ParentId = parentId;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string ParentId { get; set; }

        public string Category { get; set; }

        public int Level { get; set; }
    }
}
