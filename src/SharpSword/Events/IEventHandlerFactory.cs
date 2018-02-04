/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/3/2016 4:48:31 PM
 * ****************************************************************/

namespace SharpSword.Events
{
    /// <summary>
    /// 事件处理器创建工厂接口
    /// </summary>
    public interface IEventHandlerFactory
    {
        /// <summary>
        /// 获取事件处理器
        /// </summary>
        /// <returns>获取事件处理对象</returns>
        IEventHandler GetHandler();

        /// <summary>
        /// 释放事件处理器
        /// </summary>
        /// <param name="handler">释放事件处理对象</param>
        void ReleaseHandler(IEventHandler handler);
    }
}
