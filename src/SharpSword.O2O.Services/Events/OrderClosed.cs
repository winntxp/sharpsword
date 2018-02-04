/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/8/2017 3:27:07 PM
 * ****************************************************************/
using System;

namespace SharpSword.O2O.Services.Events
{
    /// <summary>
    /// 订单关闭成功
    /// </summary>
    [Serializable]
    public class OrderClosed : IOrderEvent, IEvent
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 关闭用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UerName { get; set; }
    }
}
