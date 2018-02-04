/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/15/2017 10:05:51 AM
 * ****************************************************************/

namespace SharpSword.O2O.Services.Domain
{
    /// <summary>
    /// 已经需要自动完成的订单信息
    /// </summary>
    public class FinishedOrderInfo
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 过期时间的Ticks值
        /// </summary>
        public long Ticks { get; set; }
    }
}
