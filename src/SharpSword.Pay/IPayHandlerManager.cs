/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2016 2:29:27 PM
 * ****************************************************************/

namespace SharpSword.Pay
{
    /// <summary>
    /// 第三方支付处理器入口
    /// </summary>
    public interface IPayHandlerManager
    {
        /// <summary>
        /// 发起支付请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="payRequestHandler"></param>
        PayRequestResult GeneratePayRequest<T>(T payRequestHandler) where T : IPayRequestHandler;

        /// <summary>
        /// 处理第三方平台回送的支付结果 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="payedCallBackHandler"></param>
        void Pay<T>(T payedCallBackHandler) where T : IPayedCallBackHandler;
    }
}