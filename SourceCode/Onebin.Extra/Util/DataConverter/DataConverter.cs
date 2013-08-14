using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Onebin.Extra.Util
{
    public class DataConverter<T> where T : class
    {
        public DataConverter() 
        {
            CheckPropertyToShow();
        }

        public Dictionary<string, Dictionary<string, string>> _dataFormatter = new Dictionary<string, Dictionary<string, string>>();

        public Dictionary<string, string> _fieldFormatter = new Dictionary<string, string>();

        public List<PropertyInfo> _propertyToShow = new List<PropertyInfo>();

        public DataConverter<T> AddDataFormatter(string filedName, Dictionary<string, string> dic)
        {
            this._dataFormatter.Add(filedName, dic);
            return this;
        }

        public DataConverter<T> AddFieldFormatter(string filedName, string newFieldName)
        {
            this._fieldFormatter.Add(filedName, newFieldName);
            return this;
        }

        protected bool IsTypeOfDateTime(Type type)
        {
            return type.Equals(typeof(DateTime)) ||
                  (type.IsGenericType &&
                   type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)) &&
                   Nullable.GetUnderlyingType(type).Equals(typeof(DateTime)));
        }

        protected int CheckPropertyToShow()
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                if (property.PropertyType.IsValueType || property.PropertyType.Equals(typeof(string)))
                {
                    _propertyToShow.Add(property);
                }
            }
            return _propertyToShow.Count;
        }

        public void GetKeyValFromDataProperty(T data, PropertyInfo property, out string key, out string val)
        {
            if (!_fieldFormatter.TryGetValue(property.Name, out key))
            {
                key = property.Name;
            }
            if (IsTypeOfDateTime(property.PropertyType))
            {
                DateTime? dateTime = property.GetValue(data, null) as DateTime?;
                val = dateTime != null ? dateTime.Value.ToString(@"yyyy\/MM\/dd HH:mm:ss") : "";
            }
            else
            {
                object propertyVal = property.GetValue(data, null);
                Dictionary<string, string> dic;
                val = propertyVal == null ? string.Empty : propertyVal.ToString();
                if (_dataFormatter.TryGetValue(property.Name, out dic))
                {
                    string dicVal = string.Empty;
                    if (dic.TryGetValue(val, out dicVal))
                    {
                        val = dicVal;
                    }
                }
            }
        }
    }
}
