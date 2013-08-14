using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leafing.Data.Definition;
using Leafing.Data;

namespace Onebin.MVC.Domain
{
    /// <summary>
    /// 数据字典
    /// </summary>
    [SoftDelete]
    public class DictionaryDetail : DbObjectModel<DictionaryDetail>
    {
        [BelongsTo]
        public DictionaryCatalog DictionaryCatalog { get; set; }

        /// <summary>
        /// 文本信息
        /// </summary>
        public string Txt { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>
        public string Val { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Ordinal { get; set; }

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
