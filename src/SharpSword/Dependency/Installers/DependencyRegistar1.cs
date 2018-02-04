/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.Core.Internal;
using System;
using System.Linq;
using System.Reflection;

namespace SharpSword.Dependency.Installers
{
    internal class DependencyRegistar1 : DependencyRegistarBase
    {
        public override int Priority => this.DefaultPriority + 9999;

        /// <summary>
        /// 注册特定的类型到容器
        /// </summary>
        /// <param name="containerBuilder">注册容器</param>
        /// <param name="typeFinder">类型查找器</param>
        /// <param name="globalConfiguration">系统框架配置参数</param>
        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            //注册系统拦截器
            foreach (var interceptorType in InterceptedByAttribute.Default)
            {
                containerBuilder.RegisterType(interceptorType)
                                .PropertiesAutowired()
                                .InstancePerLifetimeScope();
            }

            //使用类代理（继承实现类，重写虚方法，构造函数将会在被继承类上增加切点类数组，类似：ctor(Interceptor[],....... )）
            //TODO:但是代理类有个问题，就是生产代理类后，构造函数参数名称被忽略掉了，这个问题在我们需要进行自定义参数注入的时候是个问题(2个类型一致，但是实现不一致，注入会有问题)
            //http://stackoverflow.com/questions/34342483/registering-a-type-with-both-enableclassinterceptors-and-withparameter
            //全局单列
            typeFinder.FindClassesOfType<ISingletonDependency>().ForEach(type =>
            {
                if (type.EnableInterceptorProxy())
                {
                    if (type.EnableClassInterceptor())
                    {
                        containerBuilder.RegisterType(type)
                                        .AsSelf()
                                        .AsImplementedInterfaces()
                                        .PropertiesAutowired()
                                        .SingleInstance()
                                        .EnableClassInterceptors()
                                        .InterceptedBy(this.GetInterceptorsOrDefault(type));
                    }
                    if (type.EnableInterfaceInterceptor())
                    {
                        containerBuilder.RegisterType(type)
                                        .AsImplementedInterfaces()
                                        .PropertiesAutowired()
                                        .SingleInstance()
                                        .EnableInterfaceInterceptors()
                                        .InterceptedBy(this.GetInterceptorsOrDefault(type));
                    }
                }
                else
                {
                    containerBuilder.RegisterType(type)
                                    .AsSelf()
                                    .AsImplementedInterfaces()
                                    .PropertiesAutowired()
                                    .SingleInstance();
                }
            });

            //线程单例
            typeFinder.FindClassesOfType<IPerLifetimeDependency>().ForEach(type =>
            {
                if (type.EnableInterceptorProxy())
                {
                    if (type.EnableClassInterceptor())
                    {
                        containerBuilder.RegisterType(type)
                                        .AsSelf()
                                        .AsImplementedInterfaces()
                                        .PropertiesAutowired()
                                        .InstancePerLifetimeScope()
                                        .EnableClassInterceptors()
                                        .InterceptedBy(this.GetInterceptorsOrDefault(type));
                    }
                    if (type.EnableInterfaceInterceptor())
                    {
                        containerBuilder.RegisterType(type)
                                        .AsImplementedInterfaces()
                                        .PropertiesAutowired()
                                        .InstancePerLifetimeScope()
                                        .EnableInterfaceInterceptors()
                                        .InterceptedBy(this.GetInterceptorsOrDefault(type));
                    }
                }
                else
                {
                    containerBuilder.RegisterType(type)
                                    .AsSelf()
                                    .AsImplementedInterfaces()
                                    .PropertiesAutowired()
                                    .InstancePerLifetimeScope();
                }
            });

            //瞬态
            typeFinder.FindClassesOfType<ITransientDependency>().ForEach(type =>
            {
                if (!type.EnableInterceptorProxy())
                {
                    containerBuilder.RegisterType(type)
                                    .AsSelf()
                                    .AsImplementedInterfaces()
                                    .PropertiesAutowired()
                                    .InstancePerDependency();
                }

                if (type.EnableInterceptorProxy() && type.EnableClassInterceptor())
                {
                    containerBuilder.RegisterType(type)
                                    .AsSelf()
                                    .AsImplementedInterfaces()
                                    .PropertiesAutowired()
                                    .InstancePerDependency()
                                    .EnableClassInterceptors()
                                    .InterceptedBy(this.GetInterceptorsOrDefault(type));
                }

                if (type.EnableInterceptorProxy() && type.EnableInterfaceInterceptor())
                {
                    containerBuilder.RegisterType(type)
                                    .AsImplementedInterfaces()
                                    .PropertiesAutowired()
                                    .InstancePerDependency()
                                    .EnableInterfaceInterceptors()
                                    .InterceptedBy(this.GetInterceptorsOrDefault(type));
                }
            });
        }

        /// <summary>
        /// 获取拦截器，如果未定义拦截器，系统将返回默认注册的拦截器
        /// </summary>
        /// <param name="type">被代理的类的类型</param>
        /// <returns></returns>
        private Type[] GetInterceptorsOrDefault(Type type)
        {
            //我们也许重写InterceptedByAttribute特性来实现自定义拦截器
            var interceptedByAttribute = type.GetCustomAttributes().FirstOrDefault(x => x is InterceptedByAttribute);
            if (interceptedByAttribute.IsNull())
            {
                return InterceptedByAttribute.Default;
            }
            //值获取第一个
            return ((InterceptedByAttribute)interceptedByAttribute).InterceptorServiceTypes;
        }
    }
}