/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2016 2:29:27 PM
 * ****************************************************************/
using Autofac;

namespace SharpSword.Pay
{
    public class DependencyRegistar : DependencyRegistarBase
    {
        public override int Priority => 0;

        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            containerBuilder.RegisterType<PayHandlerManager>()
                            .As<IPayHandlerManager>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();
        }
    }
}
