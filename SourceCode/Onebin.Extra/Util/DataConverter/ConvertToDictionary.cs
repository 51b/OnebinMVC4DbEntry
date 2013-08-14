using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Onebin.Extra.Util
{
    public static class ConvertToDictionary
    {
        public static Dictionary<string, object> ToDictionary<T>(this DataConverter<T> dataConverter, T data) where T : class
        {
            return data == null ? null : Conver(dataConverter, new List<T>() { data }).FirstOrDefault();
        }

        public static List<Dictionary<string, object>> ToDictionary<T>(this DataConverter<T> dataConverter, IList<T> listData) where T : class
        {
            return listData == null ? null : Conver(dataConverter, listData);
        }

        private static List<Dictionary<string, object>> Conver<T>(DataConverter<T> dataConverter, IList<T> listData) where T : class
        {
            List<Dictionary<string, object>> lst = new List<Dictionary<string, object>>();
            if (listData.Count > 0)
            {
                foreach (var data in listData)
                {
                    Dictionary<string, object> item = new Dictionary<string, object>();
                    foreach (var property in dataConverter._propertyToShow)
                    {
                        string key, val;
                        dataConverter.GetKeyValFromDataProperty(data, property, out key, out val);
                        item.Add(key, val);
                    }
                    lst.Add(item);
                }
            }
            return lst;
        }
    }
}
