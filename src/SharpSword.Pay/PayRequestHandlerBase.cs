/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2016 2:29:27 PM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.Pay
{
    /// <summary>
    /// 发起支付请求，构造出POST或者GET需要的支付网关和参数信息
    /// </summary>
    /// <typeparam name="TConfig">实现IPayConfig的支付配置类</typeparam>
    public abstract class PayRequestHandlerBase<TConfig> : IPayRequestHandler where TConfig : IPayConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config">实现IPayConfig的支付配置类</param>
        public PayRequestHandlerBase(TConfig config)
        {
            config.CheckNullThrowArgumentNullException(nameof(config));
            this.PayConfig = config;
        }

        /// <summary>
        /// 支付配置文件
        /// </summary>
        protected TConfig PayConfig { get; private set; }

        /// <summary>
        /// 返回空的支付参数集合
        /// </summary>
        /// <returns></returns>
        public virtual IDictionary<string, string> GetArguments()
        {
            return new Dictionary<string, string>();
        }

        /// <summary>
        /// 默认提交支付方式为POST
        /// </summary>
        /// <returns></returns>
        public virtual HttpMethod GetHttpMethod()
        {
            return HttpMethod.POST;
        }

        /// <summary>
        /// 支付网关，即：第三方平台的支付接口地址
        /// </summary>
        public abstract string GetGetwayUrl();
    }
}
