/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/8/2017 3:27:07 PM
 * ****************************************************************/
using System;

namespace SharpSword.O2O.Services.Events
{
    /// <summary>
    /// 订单支付成功
    /// </summary>
    [Serializable]
    public class OrderPayed : IOrderEvent, IEvent
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }

        //根据实际业务我们增加业务参数
    }
}
