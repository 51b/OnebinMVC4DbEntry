using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leafing.Data;
using Leafing.Data.Definition;

namespace Onebin.MVC.Domain
{
    public abstract class IDbObjectModelHelper<T, TKey> : IDbObjectModelHelper<T> where T : class, IDbObject
    {
        [DbKey(IsDbGenerate = false)]
        public TKey Id { get; set; }
    }

    public abstract class IDbObjectModelHelper<T> : IDbObject where T : class, IDbObject
    {
        /// <summary>
        /// 查找所有对象实例
        /// </summary>
        /// <returns></returns>
        public static List<T> FindAll()
        {
            return DbEntry.From<T>().Where(Condition.Empty).Select();
        }

        /// <summary>
        /// 根据Id查找对象实例
        /// </summary>
        /// <returns></returns>
        public static T FindById(object Id)
        {
            return DbEntry.GetObject<T>(Id);
        }
    }
}
