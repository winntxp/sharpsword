/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/4/2016 11:36:02 AM
 * ****************************************************************/
using System;

namespace SharpSword.Events.Factories
{
    /// <summary>
    /// 使用IOC方式获取事件处理器
    /// </summary>
    public class IocHandlerFactory : IEventHandlerFactory
    {
        /// <summary>
        /// 事件处理器类型
        /// </summary>
        public Type HandlerType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handlerType"></param>
        public IocHandlerFactory(Type handlerType)
        {
            this.HandlerType = handlerType;
        }

        /// <summary>
        /// 从IOC容器里反转出事件处理器
        /// </summary>
        /// <returns></returns>
        public IEventHandler GetHandler()
        {
            return (IEventHandler)ServicesContainer.Current.Resolve(HandlerType);
        }

        /// <summary>
        /// 释放资源事件
        /// </summary>
        /// <param name="handler"></param>
        public void ReleaseHandler(IEventHandler handler)
        {
            if (handler is IDisposable)
            {
                ((IDisposable)handler).Dispose();
            }
        }
    }
}
