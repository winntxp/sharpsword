/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;
using Autofac.Integration.Mvc;
using SharpSword.Api.SDK;
using SharpSword.Auditing;
using SharpSword.Host.Apis;
using SharpSword.Pay.AliPay;
using System.Reflection;

namespace SharpSword.Host
{
    /// <summary>
    /// 注册系统默认实现的接口服务类
    /// </summary>
    public class DependencyRegistar : IDependencyRegistar
    {
        /// <summary>
        /// 优先级最低，方便外部程序重写框架里的实现，覆盖掉系统默认的实现
        /// </summary>
        public int Priority => int.MaxValue;

        /// <summary>
        /// 注册特定的类型到容器
        /// </summary>
        /// <param name="containerBuilder">注册容器</param>
        /// <param name="typeFinder">类型查找器</param>
        /// <param name="globalConfiguration">系统框架配置信息</param>
        public void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            //all controller
            containerBuilder.RegisterControllers(Assembly.GetExecutingAssembly())
                            .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            //我们采取代码的方式注册参数
            globalConfiguration.SetConfig(new AlipayConfig() { Key = "ccc", NotifyUrl = "" });

            //注册SDK客户端
            containerBuilder.RegisterType<DefaultApiClient>()
                            .As<IApiClient>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();
            containerBuilder.Register(c => new ApiClientConfiguration("http://www.sharpsword.com/Api"))
                            .As<IApiClientConfiguration>()
                            .SingleInstance();

            //验证实现分发器
            containerBuilder.RegisterType<DispatcherAuditingStore>()
                            .As<IAuditingStore>()
                            .InstancePerLifetimeScope()
                            .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }
    }
}