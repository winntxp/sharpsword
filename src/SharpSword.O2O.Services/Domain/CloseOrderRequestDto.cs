/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 1:04:35 PM
 * ****************************************************************/

namespace SharpSword.O2O.Services.Domain
{
    /// <summary>
    /// 取消订单参数
    /// </summary>
    public class CloseOrderRequestDto : OrderRequestDtoBase
    {
        /// <summary>
        /// 取消订单原因
        /// </summary>
        public string CloseReason { get; set; }
    }
}