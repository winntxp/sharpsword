/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2016 2:29:27 PM
 * ****************************************************************/
using System;
using System.Web;

namespace SharpSword.Pay
{
    /// <summary>
    /// 处理支付结果，注意：如果处理接口里有错误，请直接抛出异常，支付管理器会自动进行错误捕捉并处理
    /// </summary>
    public interface IPayedCallBackHandler
    {
        /// <summary>
        /// 支付完成(这里主要进行具体的业务操作，比如修改本地单据为已经支付状态)
        /// </summary>
        Action<PayCallBackContext, Trade> PayStart { get; set; }

        /// <summary>
        /// 支付失败（在支付或者更新的时候有任何错误，此方法都会被触发）
        /// </summary>
        Action<PayCallBackContext, Exception> PayError { get; set; }

        /// <summary>
        /// 返回给第三方支付平台本地是否处理成功消息
        /// </summary>
        Action<PayCallBackContext, string> PayFeedBack { get; set; }

        /// <summary>
        /// 处理第三方支付平台支付成功回调，方法里实现具体业务逻辑
        /// </summary>
        /// <param name="httpContext"></param>
        void Execute(HttpContextBase httpContext);
    }
}
