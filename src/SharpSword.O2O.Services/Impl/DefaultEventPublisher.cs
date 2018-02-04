/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/9/2017 2:17:38 PM
 * ****************************************************************/
using SharpSword.O2O.Services.Events;
using System.Linq;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 默认实现我们将触发消息发送到消息队列（当然我们可以用具体不同实现来实现消息传递）
    /// </summary>
    public class DefaultEventPublisher : IEventPublisher
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IMessageManager _messageManager;
        private readonly GlobalConfig _globalConfig;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageManager"></param>
        /// <param name="globalConfig"></param>
        public DefaultEventPublisher(IMessageManager messageManager, GlobalConfig globalConfig)
        {
            this._messageManager = messageManager;
            this._globalConfig = globalConfig;
        }

        /// <summary>
        /// 为了防止调用者出错，我们实现下显示接口声明
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventData">订单事件</param>
        bool IEventPublisher.Publish<T>(T eventData)
        {
            return this.Publish(eventData);
        }

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event">事件对象</param>
        protected virtual bool Publish<T>(T @event) where T : IEvent
        {
            //构造消息体
            EventData<T> eventData = new EventData<T>();

            //事件名称
            eventData.EventName = typeof(T).Name;

            //事件数据
            eventData.Body = @event;

            //同步队列数量
            int syncQueuesNumber = this._globalConfig.SyncQueuesNumber;

            //我们使用ID进行路由(先取到HashCode在取模)
            int k = System.Math.Abs(eventData.EventId.GetHashCode()) % syncQueuesNumber;

            //我们将订单每个数字相加（防止自动增位发生数据溢出）
            if (@event is IOrderEvent)
            {
                k = ((IOrderEvent)@event).OrderId.Select(x => x.ToString().As<int>()).Sum() % syncQueuesNumber;
            }

            //提交到消息队列，通知其他维度数据源进行数据同步
            return this._messageManager.Publish<string>(eventData.Serialize2Josn(), "order.sync.{0}".With(k));
        }
    }
}
