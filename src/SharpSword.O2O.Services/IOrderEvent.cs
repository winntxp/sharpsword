/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/8/2017 3:32:14 PM
 * ****************************************************************/

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 订单事件定义接口
    /// </summary>
    public interface IOrderEvent
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        string OrderId { get; set; }
    }
}
