/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;
using SharpSword.Configuration.JsonConfig;
using SharpSword.Configuration.WebConfig;

namespace SharpSword.Configuration
{
    /// <summary>
    /// 注册系统框架默认的配置参数管理器和工厂等
    /// </summary>
    internal class DependencyRegistar : DependencyRegistarBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="typeFinder"></param>
        /// <param name="globalConfiguration"></param>
        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            //系统框架默认注册json和web.config2种配置参数方式
            containerBuilder.RegisterType<WebConfigSettingFactory>()
                            .As<ISettingFactory>()
                            .InstancePerLifetimeScope()
                            .PropertiesAutowired();

            containerBuilder.RegisterType<JsonConfigSettingFactory>()
                            .As<ISettingFactory>()
                            .InstancePerLifetimeScope()
                            .PropertiesAutowired();

            containerBuilder.RegisterType<DefaultSettingFactoryBuilder>()
                            .As<ISettingFactoryBuilder>()
                            .InstancePerLifetimeScope()
                            .PropertiesAutowired();

            containerBuilder.RegisterType<DefaultConfigurationReader>()
                            .As<IConfigurationReader>()
                            .InstancePerLifetimeScope()
                            .PropertiesAutowired();

            //运行时我们来自动注入参数对象
            containerBuilder.RegisterSource(new SettingsSource());
        }
    }
}
