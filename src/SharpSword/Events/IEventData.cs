/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/3/2016 4:26:02 PM
 * ****************************************************************/
using System;

namespace SharpSword.Events
{
    /// <summary>
    /// 所有事件需要继承的接口
    /// </summary>
    public interface IEventData
    {
        /// <summary>
        /// 事件ID
        /// </summary>
        string EventId { get; set; }

        /// <summary>
        /// 事件创建的时间
        /// </summary>
        DateTime EventTime { get; set; }

        /// <summary>
        /// 触发此事件的对象
        /// </summary>
        object EventSource { get; set; }
    }
}
