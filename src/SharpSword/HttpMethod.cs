/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/3/2015 12:56:17 PM
 * ****************************************************************/
using System;
using System.ComponentModel;

namespace SharpSword
{
    /// <summary>
    /// 请求方式
    /// </summary>
    [Flags]
    public enum HttpMethod
    {
        /// <summary>
        /// Http-POST方式提交
        /// </summary>
        [Description("POST")]
        POST = 1,

        /// <summary>
        /// Http-Get方式提交
        /// </summary>
        [Description("GET")]
        GET = 1 << 1

    }
}
