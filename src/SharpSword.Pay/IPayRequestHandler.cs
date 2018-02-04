/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2016 2:29:27 PM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.Pay
{
    /// <summary>
    /// 直接请求对象封装
    /// </summary>
    public interface IPayRequestHandler
    {
        /// <summary>
        /// 支付提交方式 GET/POST
        /// </summary>
        HttpMethod GetHttpMethod();

        /// <summary>
        /// 获取支付网关
        /// </summary>
        /// <returns></returns>
        string GetGetwayUrl();

        /// <summary>
        /// 创建支付参数
        /// </summary>
        /// <returns></returns>
        IDictionary<string, string> GetArguments();
    }
}
