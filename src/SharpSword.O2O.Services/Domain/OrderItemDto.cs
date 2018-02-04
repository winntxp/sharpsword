/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 11:31:23 AM
 * ****************************************************************/
using SharpSword.O2O.Data.Entities;

namespace SharpSword.O2O.Services.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderItemDto : OrderItem
    {
        /// <summary>
        /// 用户是否限购
        /// </summary>
        public decimal UserLimitNumber { get; set; }
    }
}
