using System.Collections.Generic;
using System;
using System.Reflection;

namespace Onebin.Extra.Util
{
    public class EasyUiDataGrid
    {
        /// <summary>
        /// 记录总数
        /// </summary>
        private long _total = 0;

        /// <summary>
        /// rows
        /// </summary>
        private IList<Dictionary<string, object>> _rows = new List<Dictionary<string, object>>();

        /// <summary>
        /// footer
        /// </summary>
        private IList<Dictionary<string, object>> _footer = new List<Dictionary<string, object>>();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public EasyUiDataGrid() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="total">记录总数</param>
        /// <param name="rows">行记录</param>
        public EasyUiDataGrid(long total, IList<Dictionary<string, object>> rows)
        {
            this._total = total;
            this._rows = rows;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="total">记录总数</param>
        /// <param name="rows">行记录</param>
        /// <param name="footer">汇总记录</param>
        public EasyUiDataGrid(long total, IList<Dictionary<string, object>> rows, IList<Dictionary<string, object>> footer)
        {
            this._total = total;
            this._rows = rows;
            this._footer = footer;
        }

        /// <summary>
        /// 根据提供的List数据生成JsonModel
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="total">总记录数</param>
        /// <param name="rows">List数据</param>
        /// <returns>EasyUiDataGrid实例</returns>
        public EasyUiDataGrid SetRows<T>(long total, IList<T> rows) where T : class
        {
            this._total = total;
            if (total > 0)
            {
                this._rows = new DataConverter<T>().ToDictionary(rows);
            }
            return this;
        }

        /// <summary>
        /// 添加行记录
        /// </summary>
        /// <param name="rows">行记录</param>
        /// <returns>EasyUiDataGrid实例</returns>
        public EasyUiDataGrid AddRows(IList<Dictionary<string, object>> rows)
        {
            foreach (var row in rows)
            {
                _rows.Add(row);
            }
            return this;
        }

        /// <summary>
        /// 添加汇总信息
        /// </summary>
        /// <param name="footer">数据</param>
        /// <returns>EasyUiDataGrid实例</returns>
        public EasyUiDataGrid AddFooter(IList<Dictionary<string, object>> footer)
        {
            foreach (var row in footer)
            {
                _footer.Add(row);
            }
            return this;
        }

        /// <summary>
        /// 获取JsonModel
        /// </summary>
        /// <returns>JsonModel</returns>
        public object GetJsonModel()
        {
            if (_footer.Count != 0)
            {
                return new { total = _total, rows = _rows, footer = _footer };
            }
            else
            {
                return new { total = _total, rows = _rows };
            }
        }
    }
}
