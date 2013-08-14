using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;

namespace Onebin.Extra.Util
{
    public static class ConvertToDataTable
    {

        public static DataTable ToDataTable<T>(this DataConverter<T> dataConverter, IList<T> listData) where T : class
        {
            return listData == null ? null : Conver(dataConverter, listData);
        }

        private static DataTable Conver<T>(DataConverter<T> dataConverter, IList<T> listData) where T : class
        {
            DataTable dataTable = new DataTable();
            Type type = typeof(T);
            dataTable.TableName = type.Name;
            if (listData.Count > 0)
            {
                string fieldName;
                dataTable.Columns.AddRange(dataConverter._propertyToShow.Select(x => new DataColumn(dataConverter._fieldFormatter.TryGetValue(x.Name, out fieldName) ? fieldName : x.Name, typeof(string))).ToArray());

                foreach (var data in listData)
                {
                    DataRow dataRow = dataTable.NewRow();
                    foreach (var property in dataConverter._propertyToShow)
                    {
                        string key, val;
                        dataConverter.GetKeyValFromDataProperty(data, property, out key, out val);
                        dataRow.SetField(key, val);
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }
            return dataTable;
        }
    }
}
