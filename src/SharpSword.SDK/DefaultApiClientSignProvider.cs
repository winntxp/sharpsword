/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/21 17:05:55
 * ****************************************************************/
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.SDK
{
    /// <summary>
    /// 默认的数据签名服务，采取MD5摘要签名
    /// </summary>
    internal class DefaultApiClientSignProvider : IApiClientSignProvider
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IApiClientConfiguration _apiClientConfiguration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiClientConfiguration"></param>
        public DefaultApiClientSignProvider(IApiClientConfiguration apiClientConfiguration)
        {
            this._apiClientConfiguration = apiClientConfiguration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestParams"></param>
        /// <returns></returns>
        public string Sign(IDictionary<string, string> requestParams)
        {
            var signString = "{0}{1}{0}".With(this._apiClientConfiguration.AppSecret,
                        string.Join("", (from item in requestParams select item.Value).ToList()));
            return Utils.MD5(signString).ToUpper();
        }
    }
}
