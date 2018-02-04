/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/8/2017 3:27:07 PM
 * ****************************************************************/
using SharpSword.O2O.Services.Domain;
using System;

namespace SharpSword.O2O.Services.Events
{
    /// <summary>
    /// 订单创建成功
    /// </summary>
    [Serializable]
    public class OrderCreated : IOrderEvent, IEvent
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 订单信息
        /// </summary>
        public OrderDto Order { get; set; }
    }
}
