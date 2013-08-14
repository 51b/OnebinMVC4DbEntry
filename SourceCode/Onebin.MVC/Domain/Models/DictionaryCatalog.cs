using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leafing.Data.Definition;
using Leafing.Data;

namespace Onebin.MVC.Domain
{
    /// <summary>
    /// 字典目录
    /// </summary>
    [SoftDelete]
    public class DictionaryCatalog : DbObjectModel<DictionaryCatalog>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Val { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [AllowNull]
        public string Memo { get; set; }

        /// <summary>
        /// 字典明细列表
        /// </summary>
        [HasMany]
        public IList<DictionaryDetail> DictionaryDetailList { get; private set; }

        /// <summary>
        /// 修改人Id
        /// </summary>
        public long Account_Id { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [SpecialName]
        public DateTime SavedOn { get; set; }
    }
}
