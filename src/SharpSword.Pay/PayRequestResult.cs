/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2016 2:29:27 PM
 * ****************************************************************/
using System;
using System.Collections.Generic;

namespace SharpSword.Pay
{
    /// <summary>
    /// 支付请求初始化对象
    /// </summary>
    [Serializable]
    public class PayRequestResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="getwayUrl">第三方平台支付网关</param>
        /// <param name="httpMethod">提交方式</param>
        /// <param name="arguments">提交的支付参数</param>
        public PayRequestResult(string getwayUrl, HttpMethod httpMethod, IDictionary<string, string> arguments = null)
        {
            this.GetwayUrl = getwayUrl;
            this.HttpMethod = httpMethod;
            this.Arguments = arguments ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 第三方平台支付网关
        /// </summary>
        public string GetwayUrl { get; private set; }

        /// <summary>
        /// 提交方式
        /// </summary>
        public HttpMethod HttpMethod { get; private set; }

        /// <summary>
        /// 提交的支付参数
        /// </summary>
        public IDictionary<string, string> Arguments { get; private set; }
    }
}
