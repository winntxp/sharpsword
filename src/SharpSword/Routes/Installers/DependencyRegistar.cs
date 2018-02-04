/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;
using System.Linq;

namespace SharpSword.Routes.Installers
{
    internal class DependencyRegistar : DependencyRegistarBase
    {
        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            containerBuilder.RegisterType<RoutePublisher>()
                            .As<IRoutePublisher>()
                            .PropertiesAutowired()
                            .SingleInstance();

            //注册所有的路由注册器
            typeFinder.FindClassesOfType<IRouteProvider>().Where(type => !type.IsAbstract).ToList().ForEach(type =>
            {
                containerBuilder.RegisterType(type)
                                .As<IRouteProvider>()
                                .PropertiesAutowired()
                                .SingleInstance();
            });
        }
    }
}