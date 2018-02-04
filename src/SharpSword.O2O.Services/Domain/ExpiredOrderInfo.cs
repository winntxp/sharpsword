/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/15/2017 10:05:51 AM
 * ****************************************************************/

namespace SharpSword.O2O.Services.Domain
{
    /// <summary>
    /// 已经过期的订单信息
    /// </summary>
    public class ExpiredOrderInfo
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
