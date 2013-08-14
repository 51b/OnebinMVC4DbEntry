using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Onebin.Extra.Util
{
    public static class ConvertToDictionaryTree
    {
        private static string _idFieldName, _parentIdFieldName, _subNodeFieldName;
        private static PropertyInfo _idProperty;

        public static List<Dictionary<string, object>> ToDictionaryTree<T, TKey>(
            this DataConverter<T> dataConverter, 
            IList<T> listData,
            string subNodeFieldName = "Children", 
            string idFieldName = "Id",
            string parentIdFieldName = "ParentId",
            Dictionary<string, object> rootNode = null
            ) where T: class where TKey : struct
        {
            _idFieldName = idFieldName;
            _parentIdFieldName = parentIdFieldName;
            _subNodeFieldName = subNodeFieldName;
            _idProperty = dataConverter._propertyToShow.Where(x => x.PropertyType == typeof(TKey) && x.Name == parentIdFieldName).FirstOrDefault();
            return listData == null ? null : Conver<T, TKey>(dataConverter, listData, rootNode);
        }

        private static List<Dictionary<string, object>> Conver<T, TKey>(
            DataConverter<T> dataConverter,
            IList<T> listData,
            Dictionary<string, object> rootNode
            )
            where T : class 
            where TKey : struct
        {
            List<Dictionary<string, object>> root = new List<Dictionary<string, object>>();
            if (listData.Count > 0)
            {
                TKey defaultVal = default(TKey);
                if (rootNode != null)
                {
                    rootNode.Add(_subNodeFieldName, GetChildren(dataConverter, listData, defaultVal.ToString()));
                    root.Add(rootNode);
                }
                else
                {
                    root.AddRange(GetChildren(dataConverter, listData, defaultVal.ToString()));
                }
            }
            else
            {
                root.Add(rootNode);
            }
            return root;
        }

        private static List<Dictionary<string, object>> GetChildren<T>(
            DataConverter<T> dataConverter, 
            IList<T> listData, 
            string parentId,
            int level = 0
            ) where T : class
        {
            List<Dictionary<string, object>> children = new List<Dictionary<string, object>>();
            var subData = listData.Where(x => _idProperty.GetValue(x, null).ToString() == parentId);
            var noParentData = listData.Where(x => _idProperty.GetValue(x, null).ToString() != parentId);
            foreach (var data in subData)
            {
                Dictionary<string, object> child = new Dictionary<string, object>();
                string pid = string.Empty;
                foreach (var property in dataConverter._propertyToShow)
                {
                    string key, val;
                    dataConverter.GetKeyValFromDataProperty(data, property, out key, out val);
                    child.Add(key, val);
                    if (property.Name.Equals(_idFieldName))
                    {
                        pid = val;
                    }
                }
                child.Add(_subNodeFieldName, GetChildren(dataConverter, noParentData.ToList(), pid, ++level));
                children.Add(child);
            }
            return children;
        }
    }
}
