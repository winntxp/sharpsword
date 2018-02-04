/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;
using SharpSword.ViewEngine.Impl;

namespace SharpSword.ViewEngine.Installers
{
    internal class DependencyRegistar : DependencyRegistarBase
    {
        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            //注册默认的视图引擎
            containerBuilder.RegisterType<DefaultViewEngine>()
                            .As<IViewEngine>()
                            .InstancePerLifetimeScope();

            containerBuilder.RegisterType<DefaultViewEngineManager>()
                            .As<IViewEngineManager>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();
        }
    }
}