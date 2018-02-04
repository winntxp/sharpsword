/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/8/2017 3:27:07 PM
 * ****************************************************************/
using System;

namespace SharpSword.O2O.Services.Events
{
    /// <summary>
    /// 订单完成成功
    /// </summary>
    [Serializable]
    public class OrderShiped :  IOrderEvent, IEvent
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 发货员
        /// </summary>
        public int ConsigneUserId { get; set; }

        /// <summary>
        /// 发货员名称
        /// </summary>
        public string ConsigneUserName { get; set; }
    }
}
