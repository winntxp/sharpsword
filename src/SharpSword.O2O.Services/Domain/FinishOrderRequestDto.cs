/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 11:31:23 AM
 * ****************************************************************/
using System;

namespace SharpSword.O2O.Services.Domain
{
    /// <summary>
    /// 订单完成参数
    /// </summary>
    public class FinishOrderRequestDto : OrderRequestDtoBase
    {
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime FinishedTime { get; set; }
    }
}