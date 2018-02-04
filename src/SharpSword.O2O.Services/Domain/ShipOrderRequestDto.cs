/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 11:31:23 AM
 * ****************************************************************/

namespace SharpSword.O2O.Services.Domain
{
    /// <summary>
    /// 订单发货参数
    /// </summary>
    public class ShipOrderRequestDto : OrderRequestDtoBase
    {
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