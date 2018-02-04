/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/30/2017 10:50:39 AM
 * ****************************************************************/

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 事件发布器，所有触发的事件需要进行发布出去（比如：创建订单，关闭订单，支付订单，完成订单，修改门店线路等等）
    /// </summary>
    public interface IEventPublisher
    {
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <param name="eventData">事件</param>
        bool Publish<T>(T eventData) where T : IEvent;
    }
}
