/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using System.ComponentModel;

namespace SharpSword
{
    /// <summary>
    /// 记录等级
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 调试
        /// </summary>
        [Description("调试")]
        Debug,

        /// <summary>
        /// 消息
        /// </summary>
        [Description("消息")]
        Information,

        /// <summary>
        /// 警告
        /// </summary>
        [Description("警告")]
        Warning,

        /// <summary>
        /// 错误
        /// </summary>
        [Description("错误")]
        Error,

        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Fatal
    }
}
