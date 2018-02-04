/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/3/2016 4:46:28 PM
 * ****************************************************************/
using System;

namespace SharpSword.Events.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEventData"></typeparam>
    internal class ActionEventHandler<TEventData> : IEventHandler<TEventData> where TEventData : IEventData
    {
        /// <summary>
        /// 处理事件委托
        /// </summary>
        public Action<TEventData> Action { get; private set; }

        /// <summary>
        /// 处理事件委托
        /// </summary>
        /// <param name="handler"></param>
        public ActionEventHandler(Action<TEventData> handler)
        {
            this.Action = handler;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(TEventData eventData)
        {
            this.Action(eventData);
        }
    }
}
