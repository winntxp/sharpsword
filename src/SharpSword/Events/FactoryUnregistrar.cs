/* *******************************************************
 * SharpSword zhangliang4629@163.com 10/3/2016 5:01:15 PM
 * ****************************************************************/
using System;

namespace SharpSword.Events
{
    /// <summary>
    /// 
    /// </summary>
    internal class FactoryUnregistrar : IDisposable
    {
        private readonly IEventBus _eventBus;
        private readonly Type _eventType;
        private readonly IEventHandlerFactory _factory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventBus"></param>
        /// <param name="eventType"></param>
        /// <param name="factory"></param>
        public FactoryUnregistrar(IEventBus eventBus, Type eventType, IEventHandlerFactory factory)
        {
            _eventBus = eventBus;
            _eventType = eventType;
            _factory = factory;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _eventBus.Unregister(_eventType, _factory);
        }
    }
}
