using Onebin.Extra.Attr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onebin.Extra.Dic
{
    /// <summary>
    /// 审核状态
    /// </summary>
    public enum AuditStatus
    {
        /// <summary>
        /// 审核不通过
        /// </summary>
        [EnumDescription("审核不通过")]
        UnApproved = 0,

        /// <summary>
        /// 已审核通过
        /// </summary>
        [EnumDescription("已审核通过")]
        Approved = 1,
    }
}
