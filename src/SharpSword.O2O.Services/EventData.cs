/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/21/2017 2:54:10 PM
 * ****************************************************************/
using SharpSword.Timing;
using System;

namespace SharpSword.O2O.Services.Events
{
    /// <summary>
    /// 事件传输对象
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    [Serializable]
    public class EventData<TEvent>
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// 事件编号(构造函数里已经设置了默认值)
        /// </summary>
        public string EventId { get; set; }

        /// <summary>
        /// 事件触发时间(构造函数里已经设置了默认值)
        /// </summary>
        public DateTime EventTime { get; set; }

        /// <summary>
        /// 事件数据信息
        /// </summary>
        public TEvent Body { get; set; }

        /// <summary>
        /// 默认设置下 事件触发时间和事件ID编号
        /// </summary>
        public EventData()
        {
            this.EventTime = Clock.Now;
            this.EventId = Guid.NewGuid().ToString("N");
        }
    }
}
