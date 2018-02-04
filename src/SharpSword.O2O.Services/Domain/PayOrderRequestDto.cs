/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 11:31:23 AM
 * ****************************************************************/
using System;

namespace SharpSword.O2O.Services.Domain
{
    /// <summary>
    /// 订单支付参数
    /// </summary>
    public class PayOrderRequestDto : OrderRequestDtoBase
    {
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime PayedTime { get; set; }
    }
}