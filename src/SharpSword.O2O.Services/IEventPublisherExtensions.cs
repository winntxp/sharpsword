/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/30/2017 10:50:39 AM
 * ****************************************************************/
using SharpSword.O2O.Services.Events;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 订单事件发布器扩展
    /// </summary>
    public static class IEventPublisherExtensions
    {
        /// <summary>
        /// 创建订单成功事件发布
        /// </summary>
        /// <param name="orderEventPublisher"></param>
        /// <param name="orderEvent"></param>
        public static void OrderCreated(this IEventPublisher orderEventPublisher, OrderCreated orderEvent)
        {
            orderEventPublisher.Publish(orderEvent);
        }

        /// <summary>
        /// 取消订单事件
        /// </summary>
        /// <param name="orderEventPublisher"></param>
        /// <param name="orderEvent"></param>
        public static void OrderClosed(this IEventPublisher orderEventPublisher, OrderClosed orderEvent)
        {
            orderEventPublisher.Publish(orderEvent);
        }

        /// <summary>
        /// 支付成功事件
        /// </summary>
        /// <param name="orderEventPublisher"></param>
        /// <param name="orderEvent"></param>
        public static void OrderPayed(this IEventPublisher orderEventPublisher, OrderPayed orderEvent)
        {
            orderEventPublisher.Publish(orderEvent);
        }

        /// <summary>
        /// 发货成功事件
        /// </summary>
        /// <param name="orderEventPublisher"></param>
        /// <param name="orderEvent"></param>
        public static void OrderShiped(this IEventPublisher orderEventPublisher, OrderShiped orderEvent)
        {
            orderEventPublisher.Publish(orderEvent);
        }

        /// <summary>
        /// 完成订单事件
        /// </summary>
        /// <param name="orderEventPublisher"></param>
        /// <param name="orderEvent"></param>
        public static void OrderFinished(this IEventPublisher orderEventPublisher, OrderFinished orderEvent)
        {
            orderEventPublisher.Publish(orderEvent);
        }

        //....当有新的事件的时候，可以进行扩展
    }
}
