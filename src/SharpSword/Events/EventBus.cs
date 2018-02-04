/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/3/2016 4:58:00 PM
 * ****************************************************************/
using SharpSword.Events.Factories;
using SharpSword.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SharpSword.Events
{
    /// <summary>
    /// 默认的事件总线实现
    /// </summary>
    public class EventBus : IEventBus
    {
        #region Public properties

        /// <summary>
        /// 
        /// </summary>
        public static EventBus Default
        {
            get { return DefaultInstance; }
        }

        /// <summary>
        /// 
        /// </summary>
        private static readonly EventBus DefaultInstance = new EventBus();

        /// <summary>
        /// 
        /// </summary>
        public ILogger Logger { get; set; }

        #endregion

        #region Private fields

        /// <summary>
        /// 所有已经注册的事件处理工厂
        /// Key: 事件类型
        /// Value: 处理事件的工厂集合（一个事件可能有多个订阅处理类）
        /// </summary>
        private readonly Dictionary<Type, List<IEventHandlerFactory>> _handlerFactories;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        private EventBus()
        {
            this._handlerFactories = new Dictionary<Type, List<IEventHandlerFactory>>();
            this.Logger = NullLogger.Instance;
        }

        #endregion

        #region Public methods

        #region Register

        /// <inheritdoc/>
        public IDisposable Register<TEventData>(Action<TEventData> action) where TEventData : IEventData
        {
            return this.Register(typeof(TEventData), new ActionEventHandler<TEventData>(action));
        }

        /// <inheritdoc/>
        public IDisposable Register<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData
        {
            return this.Register(typeof(TEventData), handler);
        }

        /// <inheritdoc/>
        public IDisposable Register<TEventData, THandler>() where TEventData : IEventData where THandler : IEventHandler<TEventData>, new()
        {
            return this.Register(typeof(TEventData), new TransientEventHandlerFactory<THandler>());
        }

        /// <inheritdoc/>
        public IDisposable Register(Type eventType, IEventHandler handler)
        {
            return Register(eventType, new SingleInstanceHandlerFactory(handler));
        }

        /// <inheritdoc/>
        public IDisposable Register<TEventData>(IEventHandlerFactory handlerFactory) where TEventData : IEventData
        {
            return this.Register(typeof(TEventData), handlerFactory);
        }

        /// <inheritdoc/>
        public IDisposable Register(Type eventType, IEventHandlerFactory handlerFactory)
        {
            lock (_handlerFactories)
            {
                this.GetOrCreateHandlerFactories(eventType).Add(handlerFactory);
                return new FactoryUnregistrar(this, eventType, handlerFactory);
            }
        }

        #endregion

        #region Unregister

        /// <inheritdoc/>
        public void Unregister<TEventData>(Action<TEventData> action) where TEventData : IEventData
        {
            lock (_handlerFactories)
            {
                this.GetOrCreateHandlerFactories(typeof(TEventData))
                    .RemoveAll(
                        factory =>
                        {
                            if (factory is SingleInstanceHandlerFactory)
                            {
                                var singleInstanceFactoru = factory as SingleInstanceHandlerFactory;
                                if (singleInstanceFactoru.HandlerInstance is ActionEventHandler<TEventData>)
                                {
                                    var actionHandler =
                                        singleInstanceFactoru.HandlerInstance as ActionEventHandler<TEventData>;
                                    if (actionHandler.Action == action)
                                    {
                                        return true;
                                    }
                                }
                            }

                            return false;
                        });
            }
        }

        /// <inheritdoc/>
        public void Unregister<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData
        {
            this.Unregister(typeof(TEventData), handler);
        }

        /// <inheritdoc/>
        public void Unregister(Type eventType, IEventHandler handler)
        {
            lock (_handlerFactories)
            {
                this.GetOrCreateHandlerFactories(eventType)
                    .RemoveAll(
                        factory =>
                        factory is SingleInstanceHandlerFactory && ((SingleInstanceHandlerFactory)factory).HandlerInstance == handler
                    );
            }
        }

        /// <inheritdoc/>
        public void Unregister<TEventData>(IEventHandlerFactory factory) where TEventData : IEventData
        {
            this.Unregister(typeof(TEventData), factory);
        }

        /// <inheritdoc/>
        public void Unregister(Type eventType, IEventHandlerFactory factory)
        {
            lock (_handlerFactories)
            {
                this.GetOrCreateHandlerFactories(eventType).Remove(factory);
            }
        }

        /// <inheritdoc/>
        public void UnregisterAll<TEventData>() where TEventData : IEventData
        {
            this.UnregisterAll(typeof(TEventData));
        }

        /// <inheritdoc/>
        public void UnregisterAll(Type eventType)
        {
            lock (_handlerFactories)
            {
                this.GetOrCreateHandlerFactories(eventType).Clear();
            }
        }

        #endregion

        #region Trigger

        /// <inheritdoc/>
        public void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            this.Trigger((object)null, eventData);
        }

        /// <inheritdoc/>
        public void Trigger<TEventData>(object eventSource, TEventData eventData) where TEventData : IEventData
        {
            this.Trigger(typeof(TEventData), eventSource, eventData);
        }

        /// <inheritdoc/>
        public void Trigger(Type eventType, IEventData eventData)
        {
            this.Trigger(eventType, null, eventData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="eventSource"></param>
        /// <param name="eventData"></param>
        /// <exception cref="SharpSwordCoreException">事件处理程序未注册</exception>
        public void Trigger(Type eventType, object eventSource, IEventData eventData)
        {
            eventData.EventSource = eventSource;

            //根据事件类型检索对应的处理类
            foreach (var factoryToTrigger in GetHandlerFactories(eventType))
            {
                var eventHandler = factoryToTrigger.GetHandler();
                if (eventHandler.IsNull())
                {
                    throw new SharpSwordCoreException("Registered event handler for event type " + eventType.Name +
                                        " does not implement IEventHandler<" + eventType.Name + "> interface!");
                }

                var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);

                try
                {
                    handlerType
                        .GetMethod("HandleEvent", BindingFlags.Public | BindingFlags.Instance, null, new[] { eventType },
                            null)
                        .Invoke(eventHandler, new object[] { eventData });
                }
                finally
                {
                    factoryToTrigger.ReleaseHandler(eventHandler);
                }
            }

            //Implements generic argument inheritance. See IEventDataWithInheritableGenericArgument
            if (eventType.IsGenericType && eventType.GetGenericArguments().Length == 1 &&
                typeof(IEventDataWithInheritableGenericArgument).IsAssignableFrom(eventType))
            {
                var genericArg = eventType.GetGenericArguments()[0];
                var baseArg = genericArg.BaseType;
                if (!baseArg.IsNull())
                {
                    var baseEventType = eventType.GetGenericTypeDefinition().MakeGenericType(genericArg.BaseType);
                    var constructorArgs = ((IEventDataWithInheritableGenericArgument)eventData).GetConstructorArgs();
                    var baseEventData = (IEventData)Activator.CreateInstance(baseEventType, constructorArgs);
                    baseEventData.EventTime = eventData.EventTime;
                    this.Trigger(baseEventType, eventData.EventSource, baseEventData);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        private IEnumerable<IEventHandlerFactory> GetHandlerFactories(Type eventType)
        {
            var handlerFactoryList = new List<IEventHandlerFactory>();

            lock (_handlerFactories)
            {
                foreach (var handlerFactory in _handlerFactories.Where(hf => ShouldTriggerEventForHandler(eventType, hf.Key)))
                {
                    handlerFactoryList.AddRange(handlerFactory.Value);
                }
            }

            return handlerFactoryList.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handlerType"></param>
        /// <returns></returns>
        private static bool ShouldTriggerEventForHandler(Type eventType, Type handlerType)
        {
            if (handlerType == eventType)
            {
                return true;
            }

            //是否是继承的子类(可以在同一事件处理器里处理)
            if (handlerType.IsAssignableFrom(eventType))
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            return this.TriggerAsync((object)null, eventData);
        }

        /// <inheritdoc/>
        public Task TriggerAsync<TEventData>(object eventSource, TEventData eventData)
            where TEventData : IEventData
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    this.Trigger(eventSource, eventData);
                }
                catch (Exception ex)
                {
                    this.Logger.Warning(ex.ToString(), ex);
                }
            });
        }

        /// <inheritdoc/>
        public Task TriggerAsync(Type eventType, IEventData eventData)
        {
            return this.TriggerAsync(eventType, null, eventData);
        }

        /// <inheritdoc/>
        public Task TriggerAsync(Type eventType, object eventSource, IEventData eventData)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    this.Trigger(eventType, eventSource, eventData);
                }
                catch (Exception ex)
                {
                    this.Logger.Warning(ex.ToString(), ex);
                }
            });
        }

        #endregion

        #endregion

        #region Private methods

        private List<IEventHandlerFactory> GetOrCreateHandlerFactories(Type eventType)
        {
            List<IEventHandlerFactory> handlers;
            if (!_handlerFactories.TryGetValue(eventType, out handlers))
            {
                _handlerFactories[eventType] = handlers = new List<IEventHandlerFactory>();
            }

            return handlers;
        }

        #endregion
    }
}
