/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/14 12:53:52
 * ****************************************************************/
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using SharpSword.TypeFinder.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//[assembly: PreApplicationStartMethod(typeof(ServiceCenter.Api.Core.ServicesContainer), "Init")]

namespace SharpSword
{
    /// <summary>
    /// see AutoFac link to:http://docs.autofac.org
    /// </summary>
    public class ServicesContainer : IIocResolver
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly object Locker = new object();

        /// <summary>
        /// 定义一个默认的生命周期作用域
        /// </summary>
        private const string HttpWebRequestScope = "AutofacWebRequest";

        /// <summary>
        /// 
        /// </summary>
        private static IContainer _container;
        private static ServicesContainer _servicesContainer;

        /// <summary>
        /// 系统框架服务注册类
        /// </summary>
        private ServicesContainer() { }

        /// <summary>
        /// 获取当前服务容器
        /// </summary>
        public static ServicesContainer Current
        {
            get
            {
                if (!_servicesContainer.IsNull()) return _servicesContainer;

                lock (Locker)
                {
                    if (_servicesContainer.IsNull())
                    {
                        _servicesContainer = new ServicesContainer();
                    }
                }

                return _servicesContainer;
            }
        }

        /// <summary>
        /// 对外公开的IOC容器访问接口
        /// </summary>
        public IContainer Container
        {
            get { return _container; }
        }

        /// <summary>
        /// IOC容器初始化，系统框架初始化等
        /// </summary>
        /// <returns></returns>
        public IContainer Initialize(GlobalConfiguration globalConfiguration)
        {
            //已经初始化了，直接返还
            if (!_container.IsNull())
            {
                return _container;
            }

            //初始化注册容器
            var builder = new ContainerBuilder();

            //查询器注册（默认使用WebAppTypeFinder,默认自动搜索BIN目录）
            var typeFinder = new WebAppTypeFinder { EnsureBinFolderAssembliesLoaded = true };
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();

            //注册系统框架配置参数
            builder.RegisterInstance(globalConfiguration).As<GlobalConfiguration>().SingleInstance();

            //注册module
            typeFinder.FindClassesOfType<IModule>()
                      .Where(type => !type.IsAbstract && type.IsPublic && typeof(Module).IsAssignableFrom(type))
                      .ToList().ForEach(type =>
                      {
                          builder.RegisterModule((IModule)Activator.CreateInstance(type));
                      });

            //注册手工实现IDependencyRegistar的接口进行初始化（如果可以，可以覆盖系统自动实现的注册方式）
            typeFinder.FindClassesOfType<IDependencyRegistar>()
                      .Select(type => (IDependencyRegistar)Activator.CreateInstance(type))
                      .OrderBy(o => o.Priority)
                      .ToList()
                      .ForEach(x => { x.Register(builder, typeFinder, globalConfiguration); });

            //build
            _container = builder.Build();

            //返回注册容器
            return _container;
        }

        /// <summary>
        /// 根据类型创建出对象实例
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="lifetimeScope">生命周期作用域</param>
        /// <returns></returns>
        public TService Resolve<TService>(ILifetimeScope lifetimeScope = null)
        {
            return (TService)Resolve(typeof(TService), lifetimeScope);
        }

        /// <summary>
        /// 根据类型创建出所有注册的实现类型
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="namedServices"></param>
        /// <param name="lifetimeScope">生命周期作用域</param>
        /// <returns></returns>
        public TService[] ResolveAll<TService>(string namedServices = "", ILifetimeScope lifetimeScope = null)
        {
            //返回生命域
            lifetimeScope = lifetimeScope.NullBackDefault(this.Scope);

            return string.IsNullOrWhiteSpace(namedServices)
                ? lifetimeScope.Resolve<IEnumerable<TService>>().ToArray()
                : lifetimeScope.ResolveNamed<IEnumerable<TService>>(namedServices).ToArray();
        }

        /// <summary>
        /// 根据类型创建对象
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="lifetimeScope">生命周期作用域</param>
        /// <returns></returns>
        public object Resolve(Type serviceType, ILifetimeScope lifetimeScope = null)
        {
            var obj = lifetimeScope.NullBackDefault(this.Scope).Resolve(serviceType);
            if (obj is IShouldInitialize)
            {
                ((IShouldInitialize)obj).Initialize();
            }
            return obj;
        }

        /// <summary>
        /// 尝试创建指定类型，不会抛出异常
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="instance">返回的实例</param>
        /// <param name="lifetimeScope">生命周期作用域，可以为null</param>
        /// <returns>创建是否成功true/false</returns>
        public bool TryResolve(Type serviceType, ILifetimeScope lifetimeScope, out object instance)
        {
            return lifetimeScope.NullBackDefault(this.Scope).TryResolve(serviceType, out instance);
        }

        /// <summary>
        /// 创建未注册的类型（类型没有在容器里注册过）；但是创建的类型有可能会引用容器里注册的类型
        /// </summary>
        /// <param name="type">待创建服务类型</param>
        /// <param name="lifetimeScope">生命周期作用域，可为null</param>
        /// <returns></returns>
        public object ResolveUnregistered(Type type, ILifetimeScope lifetimeScope = null)
        {

            lifetimeScope = lifetimeScope.NullBackDefault(this.Scope);

            //获取类型的构造函数集合(优先使用最多参数的构造函数)
            var constructors = type.GetConstructors().OrderByDescending(c => c.GetParameters().Length);

            //循环构造函数集合，创建对象
            foreach (var constructor in constructors)
            {
                try
                {
                    var parameters = constructor.GetParameters();
                    var parameterInstances = new List<object>();
                    foreach (var parameter in parameters)
                    {
                        var service = this.Resolve(parameter.ParameterType, lifetimeScope);
                        if (service.IsNull())
                        {
                            throw new SharpSwordCoreException("依赖{0}未注册，请先注册服务".With(parameter.ParameterType.FullName));
                        }
                        parameterInstances.Add(service);
                    }
                    return Activator.CreateInstance(type, parameterInstances.ToArray());
                }
#pragma warning disable CS0168 // The variable 'exc' is declared but never used
                catch (SharpSwordCoreException exc)
#pragma warning restore CS0168 // The variable 'exc' is declared but never used
                {
                    //
                }
            }

            throw new SharpSwordCoreException("未找到构造函数，创建对象失败");
        }

        /// <summary>
        /// 判断一个类型是否注册
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="lifetimeScope">生命周期作用域</param>
        /// <returns></returns>
        public bool IsRegistered(Type serviceType, ILifetimeScope lifetimeScope = null)
        {
            return lifetimeScope.NullBackDefault(this.Scope).IsRegistered(serviceType);
        }

        /// <summary>
        /// 如果反转不成功，则返回null
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="lifetimeScope">生命周期作用域</param>
        /// <returns></returns>
        public object ResolveOptional(Type serviceType, ILifetimeScope lifetimeScope = null)
        {
            return lifetimeScope.NullBackDefault(this.Scope).ResolveOptional(serviceType);
        }

        /// <summary>
        /// 获取当前请求生命周期
        /// </summary>
        /// <returns></returns>
        public virtual ILifetimeScope Scope()
        {
            try
            {
                //基于一个当前请求的生命周期（一个生命周期后，系统资源会释放）
                return !HttpContext.Current.IsNull() ? AutofacDependencyResolver.Current.RequestLifetimeScope
                                                     : this.Scope(HttpWebRequestScope);
            }
            catch (Exception)
            {
                return this.Scope(HttpWebRequestScope);
            }
        }

        /// <summary>
        /// 获取一个生命周期域
        /// </summary>
        /// <param name="tag">作用域名称</param>
        /// <returns></returns>
        public virtual ILifetimeScope Scope(object tag)
        {
            return this.Container.BeginLifetimeScope(tag);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="obj"></param>
        public void Release(object obj)
        {
            //throw new NotImplementedException();
        }
    }
}
