using Onebin.Extra.Attr;

namespace Onebin.Extra.Dic
{
    /// <summary>
    /// 返回结果状态
    /// </summary>
    public enum ResultStatus
    {
        /// <summary>
        /// 失败
        /// </summary>
        Failure = 0,

        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,

        /// <summary>
        /// 错误
        /// </summary>
        Error = 2,

        /// <summary>
        /// 没有权限
        /// </summary>
        Refused = 3
    }

    /// <summary>
    /// 操作类型
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        /// 添加
        /// </summary>
        [EnumDescription("添加")]
        Insert = 1,

        /// <summary>
        /// 修改
        /// </summary>
        [EnumDescription("修改")]
        Update = 2,

        /// <summary>
        /// 保存
        /// </summary>
        [EnumDescription("保存")]
        Save = 3,

        /// <summary>
        /// 删除
        /// </summary>
        [EnumDescription("删除")]
        Delete = 0
    }
}
