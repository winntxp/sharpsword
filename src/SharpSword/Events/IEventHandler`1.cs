/* *******************************************************
 * SharpSword zhangliang4629@163.com 10/4/2016 1:37:00 PM
 * ****************************************************************/

namespace SharpSword.Events
{
    /// <summary>
    /// 所有事件处理需要继承的处理接口
    /// </summary>
    /// <typeparam name="TEventData">待处理的事件类型</typeparam>
    public interface IEventHandler<in TEventData> : IEventHandler where TEventData : IEventData
    {
        /// <summary>
        /// 处理对应的事件
        /// </summary>
        /// <param name="eventData">事件</param>
        void HandleEvent(TEventData eventData);
    }
}
