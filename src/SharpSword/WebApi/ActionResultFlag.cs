/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/26/2015 4:02:14 PM
 * ****************************************************************/
using System.ComponentModel;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 所有接口返回代码枚举
    /// </summary>
    public enum ActionResultFlag
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("成功")]
        SUCCESS = 0,

        /// <summary>
        /// 系统错误
        /// </summary>
        [Description("系统错误")]
        FAIL = 100,

        /// <summary>
        /// 密钥验证失败
        /// </summary>
        [Description("验证密钥失败")]
        FAIL_SIGN = 101,

        /// <summary>
        /// 解密上送参数失败
        /// </summary>
        [Description("上送参数解密失败")]
        FAIL_DECRYPT_REQUEST = 102,

        /// <summary>
        /// 参数绑定失败
        /// </summary>
        [Description("上送参数绑定失败")]
        FAIL_BINDER_REQUEST = 103,

        /// <summary>
        /// 系统错误
        /// </summary>
        [Description("系统异常")]
        EXCEPTION = 300,

        /// <summary>
        /// 连接超时
        /// </summary>
        [Description("连接超时")]
        TIMEOUT = 400,

        /// <summary>
        /// 未知错误
        /// </summary>
        [Description("未知错误")]
        OTHER = 999
    }
}