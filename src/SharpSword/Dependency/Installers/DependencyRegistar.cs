/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;
using Autofac.Integration.Mvc;
using SharpSword.Fakes;
using System.Reflection;
using System.Web;

namespace SharpSword.Dependency.Installers
{
    internal class DependencyRegistar : DependencyRegistarBase
    {
        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            //注册下当前http请求上下文抽象类
            containerBuilder.Register(c =>
                //我们注册下模拟类，防止非基于HttpRequest请求出现错误
                HttpContext.Current != null ? (new HttpContextWrapper(HttpContext.Current) as HttpContextBase)
                                            : (new FakeHttpContext("~/") as HttpContextBase))
                            .As<HttpContextBase>()
                            .InstancePerLifetimeScope();

            containerBuilder.Register(c => c.Resolve<HttpContextBase>().Request)
                            .As<HttpRequestBase>()
                            .InstancePerLifetimeScope();

            containerBuilder.Register(c => c.Resolve<HttpContextBase>().Response)
                            .As<HttpResponseBase>()
                            .InstancePerLifetimeScope();

            containerBuilder.Register(c => c.Resolve<HttpContextBase>().Server)
                            .As<HttpServerUtilityBase>()
                            .InstancePerLifetimeScope();

            containerBuilder.Register(c => c.Resolve<HttpContextBase>().Session)
                            .As<HttpSessionStateBase>()
                            .InstancePerLifetimeScope();

            //注册IOC容器
            containerBuilder.Register(c => ServicesContainer.Current)
                            .As<IIocResolver>();

            //注册下本程序集里的控制器
            containerBuilder.RegisterControllers(Assembly.GetExecutingAssembly())
                            .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }
    }
}