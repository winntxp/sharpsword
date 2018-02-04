/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 12:18:52 PM
 * ****************************************************************/
using SharpSword.O2O.Data.Entities;
using SharpSword.O2O.Services.Domain;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 订单相关接口；订单声明周期：WAIT_PAY，WAIT_SEND_GOODS，TRADE_CLOSE，WAIT_BUYER_CONFIRM_GOODS，TRADE_FINISHED
    /// </summary>
    public interface IOrderServices
    {
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="request"></param>
        void CloseOrder(CloseOrderRequestDto request);

        /// <summary>
        /// 订单支付
        /// </summary>
        /// <param name="request"></param>
        void PayOrder(PayOrderRequestDto request);

        /// <summary>
        /// 订单已经发货;
        /// </summary>
        /// <param name="request"></param>
        void ShipOrder(ShipOrderRequestDto request);

        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="request"></param>
        void FinishOrder(FinishOrderRequestDto request);

        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        OrderDto GetOrder(string orderId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Order GetOrderSimple(string orderId);
    }
}
