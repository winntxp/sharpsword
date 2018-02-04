/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2016 2:29:27 PM
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Web;

namespace SharpSword.Pay
{
    /// <summary>
    /// 支付完成回调上下文
    /// </summary>
    [Serializable]
    public class PayCallBackContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments">初始化的参数字典</param>
        /// <param name="httpContext">当前http请求上下文</param>
        internal PayCallBackContext(HttpContextBase httpContext, IDictionary<string, string> arguments = null)
        {
            this.Arguments = arguments ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            this.HttpContext = httpContext;
        }

        /// <summary>
        /// 从第三方支付平台POST回来的参数集合
        /// </summary>
        public IDictionary<string, string> Arguments { get; private set; }

        /// <summary>
        /// 当前http请求上下文
        /// </summary>
        public HttpContextBase HttpContext { get; private set; }

    }
}
