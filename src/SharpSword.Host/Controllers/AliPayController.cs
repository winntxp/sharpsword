/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/14/2016 1:45:18 PM
 * ****************************************************************/
using SharpSword.Pay;
using SharpSword.Pay.AliPay;

namespace SharpSword.Host.Controllers
{
    public interface IOrderService
    {
        void Update(PayCallBackContext context, Trade trade);
    }

    /// <summary>
    /// 
    /// </summary>
    public class OrderService : Domain.Services.SharpSwordServicesBase, IOrderService
    {
        public void Update(PayCallBackContext context, Trade trade)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AliPayController : MvcControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IPayHandlerManager _payHandlerManager;
        private readonly IOrderService _orderService;
        private readonly AlipayConfig _alipayConfig;

        /// <summary>
        /// 
        /// </summary>
        public AliPayController(IPayHandlerManager payHandlerManager, IOrderService orderService, AlipayConfig alipayConfig)
        {
            this._payHandlerManager = payHandlerManager;
            this._orderService = orderService;
            this._alipayConfig = alipayConfig;
        }

        /// <summary>
        /// 
        /// </summary>     
        public void Pay()
        {
            var r0 = this._payHandlerManager.GeneratePayRequest(new AlipayRequestHandler(this._alipayConfig)
            {
                Body = ""
            });

            //POST转向或者get转向
        }

        /// <summary>
        /// 
        /// </summary>
        public void Notify()
        {
            this._payHandlerManager.Pay(new AlipayCallBackHandler(this._alipayConfig)
            {
                //开始支付
                PayStart = (paycontext, trade) =>
                {
                    //具体的业务处理中，我们一般会定义一个服务类，将其中的方法定义成符合此委托的方法，这样我们再不同发支付业务场景中，可以通用一套支付业务逻辑
                    this._orderService.Update(paycontext, trade);

                    //反馈给第三方支付平台(如果未定义PayResponse则需要自己在这里处理返回信息)
                    //paycontext.HttpContext.Response.Write("OK");
                },

                //支付操作中出现任何异常，都会在这里处理
                PayError = (payContext, exception) =>
                {
                    //这里我们定义处理错误消息的委托，当然一般我们需要定义一个处理类，然后将此类的一个方法定义成委托，防止重复代码出现
                    this.Logger.Error(exception);

                    //反馈给第三方支付平台(如果未定义PayResponse则需要自己在这里处理返回信息
                    //payContext.HttpContext.Response.Write("ERR");
                },

                //反馈信息给第三方支付平台
                PayFeedBack = (payContext, message) =>
                {
                    payContext.HttpContext.Response.Write(message);
                }
            });
        }
    }
}
