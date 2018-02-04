/* ****************************************************************
 * SharpSword zhangliang4629@163.com 10/3/2016 4:26:48 PM
 * ****************************************************************/
using SharpSword.GuidGenerator;
using SharpSword.Timing;
using System;

namespace SharpSword.Events
{
    /// <summary>
    /// Implements <see cref="IEventData"/>事件基类
    /// 请继承此类是事件建议都标识成可序列化，
    /// 这样我们在需要进行事件传输夸分布式传输的时候可能会用到持久化
    /// </summary>
    [Serializable]
    public abstract class EventData : IEventData
    {
        /// <summary>
        /// 事件编号(构造函数里已经设置了默认值)
        /// </summary>
        public string EventId { get; set; }

        /// <summary>
        /// 事件触发时间(构造函数里已经设置了默认值)
        /// </summary>
        public DateTime EventTime { get; set; }

        /// <summary>
        /// 触发事件源
        /// </summary>
        public object EventSource { get; set; }

        /// <summary>
        /// 默认设置下 事件触发时间和事件ID编号
        /// </summary>
        protected EventData()
        {
            this.EventTime = Clock.Now;
            this.EventId = GuidGeneratorManager.Provider.Create().ToString("N");
        }
    }
}
