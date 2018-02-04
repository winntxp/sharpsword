/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using System;
using Autofac;
using Autofac.Core;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace SharpSword.Logging.Log4Net
{
    /// <summary>
    /// 继承自AutoFac.Module，系统框架会自动进行搜索注册
    /// </summary>
    public class LoggingModule : Autofac.Module
    {
        /// <summary>
        /// 用于缓存类型是否有ILogger属性集合
        /// </summary>
        private static Dictionary<Type, PropertyInfo[]> _propertysCached = new Dictionary<Type, PropertyInfo[]>();

        private static object _locker = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleBuilder"></param>
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            //系统默认使用Log4Net日志记录器
            moduleBuilder.RegisterType<Log4NetLoggerFactory>()
                .As<ILoggerFactory>()
                .InstancePerLifetimeScope();

            //在需要对MVC的Filter进行属性注入的时候使用或者在global.asax里进行注册(无法关联当前使用日志类)
            moduleBuilder.Register(cx => cx.Resolve<ILoggerFactory>().CreateLogger(this.GetType())).As<ILogger>();

            base.Load(moduleBuilder);
        }

        /// <summary>
        /// logger在注册的时候，需要记录日志关联的类，这里重新设置
        /// </summary>
        /// <param name="componentRegistry"></param>
        /// <param name="registration"></param>
        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry,
            IComponentRegistration registration)
        {
            //当前实例类型
            var instanceType = registration.Activator.LimitType;

            //构造函数（先准备参数对象）
            bool needsLogger = instanceType.GetConstructors().Select(p => p.GetParameters())
                .Any(constructor => constructor.Any(item => item.ParameterType == typeof(ILogger)));
            if (needsLogger)
            {
                registration.Preparing += (sender, e) =>
                {
                    e.Parameters = e.Parameters.Concat(new[]
                    {
                        new ResolvedParameter(
                            (p, i) => p.ParameterType == typeof(ILogger),
                            (p, i) => e.Context.Resolve<ILoggerFactory>().CreateLogger(p.Member.DeclaringType))
                    });
                };
            }

            //检索需要注入的对象，是否包含ILogger属性
            var properties = instanceType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(ILogger) && p.CanWrite && p.GetIndexParameters().Length == 0)
                .ToArray();
            if (properties.Any())
            {
                //日志的属性注入，不使用AutoFac自动，这里手动先反射检索出对象的属性，然后进行赋值操作
                registration.Activated += (sender, e) =>
                {
                    //可能存在多个ILogger属性，直接注入日志对象
                    foreach (var propToSet in properties)
                    {
                        if (e.Instance.GetType() == instanceType)
                            propToSet.SetValue(e.Instance,
                                e.Context.Resolve<ILoggerFactory>().CreateLogger(instanceType),
                                null);
                    }
                };
            }
        }
    }
}
