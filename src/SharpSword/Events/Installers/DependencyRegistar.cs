/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;
using SharpSword.Events.Entitys;
using SharpSword.Events.Factories;

namespace SharpSword.Events.Installers
{
    /// <summary>
    /// 系统自动注册所有的事件和时间处理绑定，系统框架执行的第一次自动注册，无需手工处理
    /// </summary>
    internal class DependencyRegistar : DependencyRegistarBase
    {
        /// <summary>
        /// 注册特定的类型到容器
        /// </summary>
        /// <param name="containerBuilder">注册容器</param>
        /// <param name="typeFinder">类型查找器</param>
        /// <param name="globalConfiguration">系统框架配置参数</param>
        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            //注册EventBus事件总线
            containerBuilder.Register(c => EventBus.Default)
                            .As<IEventBus>()
                            .PropertiesAutowired();

            //注册实体创建，删除，更新事件
            containerBuilder.RegisterType<EntityEventHelper>()
                            .AsImplementedInterfaces()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            //注册所有的处理事件
            var eventHandlers = typeFinder.FindClassesOfType<IEventHandler>();
            //是有实现的事件处理类
            foreach (var eventHandler in eventHandlers)
            {
                //事件处理类不能是泛型类 排除掉系统默认的ActionEventHandler处理类
                if (eventHandler.IsGenericType) { continue; }

                //必须继承IEventHandler
                if (!typeof(IEventHandler).IsAssignableFrom(eventHandler)) { return; }

                //获取事件处理类的所有接口
                var interfaces = eventHandler.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    //接口必须继承IEventHandler接口
                    if (!typeof(IEventHandler).IsAssignableFrom(@interface)) { continue; }

                    //必须要继承接口IEventHandler<TEventData>
                    var genericArgs = @interface.GetGenericArguments();

                    //参数必须为一个才能注册
                    if (genericArgs.Length != 1) { continue; }

                    //根据泛型接口获取到定义的事件参数合事件处理器创建类关联起来，批量注册使用IocHandlerFactory来获取事件处理器
                    EventBus.Default.Register(genericArgs[0], new IocHandlerFactory(eventHandler));

                    //注册事件到IOC容器
                    containerBuilder.RegisterType(eventHandler).As(@interface).AsSelf().PropertiesAutowired();
                }
            }
        }
    }
}