/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2016 2:29:27 PM
 * ****************************************************************/
using System;
using System.Web;

namespace SharpSword.Pay
{
    /// <summary>
    /// 此实现类生命周期需要注册成，在一个作用域里每次请求单例
    /// </summary>
    public class PayHandlerManager : IPayHandlerManager
    {
        /// <summary>
        /// 
        /// </summary>
        private HttpContextBase _httpContext;

        /// <summary>
        /// 日志记录器
        /// </summary>
        public ILogger<PayHandlerManager> Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext">http工作上下文</param>
        public PayHandlerManager(HttpContextBase httpContext)
        {
            this.Logger = GenericNullLogger<PayHandlerManager>.Instance;
            this._httpContext = httpContext;
        }

        /// <summary>
        /// 获取付款请求信息，返回付款请求对象供调用方使用
        /// </summary>
        /// <param name="payHandlerName">PayHandler</param>
        /// <returns></returns>
        public PayRequestResult GeneratePayRequest<T>(T payRequestHandler) where T : IPayRequestHandler
        {
            return new PayRequestResult(payRequestHandler.GetGetwayUrl(),
                payRequestHandler.GetHttpMethod(), payRequestHandler.GetArguments());
        }

        /// <summary>
        /// 付款回调业务处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="payedCallBackHandler"></param>
        public void Pay<T>(T payedCallBackHandler) where T : IPayedCallBackHandler
        {
            try
            {
                payedCallBackHandler.Execute(this._httpContext);
            }
            catch (Exception exc)
            {
                //我们记录下所有支付错误信息
                this.Logger.Error(exc);

                //出现错误，我们直接抛出下异常
                throw exc;
            }
        }
    }
}
