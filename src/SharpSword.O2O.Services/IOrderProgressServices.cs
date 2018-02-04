/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 5:24:54 PM
 * ****************************************************************/
using SharpSword.O2O.Services.Domain;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 订单处理进度跟踪服务
    /// </summary>
    public interface IOrderProgressServices
    {
        /// <summary>
        /// 保存订单
        /// </summary>
        /// <param name="order"></param>
        SaveOrderResult SaveOrder(OrderDto order);
    }
}
