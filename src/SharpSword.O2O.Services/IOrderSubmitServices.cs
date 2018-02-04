/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 12:18:52 PM
 * ****************************************************************/
using SharpSword.O2O.Services.Domain;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 订单提交接口，我们单独定一个接口出来，区分开IOrderServices接口，放置注入服务过多影响执行效率
    /// </summary>
    public interface IOrderSubmitServices
    {
        /// <summary>
        /// 提交订单(先获取到票据，即：相当于先获取到资格)
        /// </summary>
        /// <param name="request">提交订单参数</param>
        SubmitOrderResult SubmitOrder(OrderCreateRequestDto request);
    }
}
