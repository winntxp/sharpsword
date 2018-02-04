/* *******************************************************
 * SharpSword zhangliang4629@163.com 10/12/2016 2:04:01 PM
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Castle.DynamicProxy;

namespace SharpSword.Services
{
    /// <summary>
    /// 对IDomainService接口实现类进行拦截，实现一个方法一个事务封装
    /// </summary>
    public class RegistrationSource : IRegistrationSource
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly DefaultProxyBuilder _proxyBuilder;
        private readonly ITypeFinder _typeFinder;

        /// <summary>
        /// 
        /// </summary>
        public RegistrationSource(ITypeFinder typeFinder)
        {
            _proxyBuilder = new DefaultProxyBuilder();
            this._typeFinder = typeFinder;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsAdapterForIndividualComponents
        {
            get { return true; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="registrationAccessor"></param>
        /// <returns></returns>
        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service,
            Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {

            var serviceWithType = service as IServiceWithType;
            if (serviceWithType == null)
                yield break;

            var serviceType = serviceWithType.ServiceType;

            //直接使用当前实现类注入
            if (!typeof(IDomainService).IsAssignableFrom(serviceType) || typeof(IDomainService) == serviceType)
                yield break;

            //代理类
            Type proxyType = null;

            //实现类
            Type targetType = null;

            //直接使用类型作为入参
            if (serviceType.IsClass)
            {
                //输出的类型为：继承实现类，重写虚拟方法
                proxyType = _proxyBuilder.CreateClassProxyTypeWithTarget(serviceType, new Type[0],
                     ProxyGenerationOptions.Default);
                targetType = serviceType;
            }

            //使用接口作为入参
            if (serviceType.IsInterface)
            {
                //实现指定接口，传入实现类实例
                proxyType = _proxyBuilder.CreateInterfaceProxyTypeWithTargetInterface(serviceType, new Type[0],
                    ProxyGenerationOptions.Default);
                targetType = this._typeFinder.FindClassesOfType(serviceType).FirstOrDefault();
            }

            //代理类失败
            if (proxyType.IsNull() || targetType.IsNull())
            {
                throw new ApiException("未找到{0}实现类".With(serviceType.FullName));
            }

            yield return RegistrationBuilder.ForDelegate((c, p) =>
            {
                //目标类
                var target = ServicesContainer.Current.ResolverUnregistered(targetType);

                //var target = c.Resolve(targetType);

                //获取类型的构造函数集合(优先使用最多参数的构造函数)
                var constructor =
                    proxyType.GetConstructors().OrderByDescending(x => x.GetParameters().Length).FirstOrDefault();
                if (constructor.IsNull())
                {
                    return null;
                }

                //创建构造函数参数
                var parameterInstances = new List<object>();
                foreach (var parameter in constructor.GetParameters())
                {
                    object parameterInstance = null;

                    if (parameter.ParameterType == serviceType)
                    {
                        parameterInstance = target;
                    }
                    else if (parameter.ParameterType == typeof(IInterceptor[]))
                    {
                        parameterInstance = new IInterceptor[]
                            {new TransactionInterceptor() {Logger = c.Resolve<ILogger>()}};
                    }
                    else
                    {
                        parameterInstance = c.Resolve(parameter.ParameterType);
                    }

                    if (parameterInstance.IsNull())
                    {
                        throw new ApiException("依赖{0}未注册，请先注册服务".With(parameter.ParameterType.FullName));
                    }

                    parameterInstances.Add(parameterInstance);
                }

                //直接反射创建出对象
                return Activator.CreateInstance(proxyType, parameterInstances.ToArray());
            })
            .InstancePerLifetimeScope()
            .PropertiesAutowired()
            .As(service)
            .CreateRegistration();
        }
    }
}
