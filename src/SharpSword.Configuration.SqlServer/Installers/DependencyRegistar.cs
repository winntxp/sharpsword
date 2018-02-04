/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/20 18:49:15
 * ****************************************************************/
using Autofac;
using SharpSword.WebApi;

namespace SharpSword.Configuration.SqlServer
{
    public class DependencyRegistar : IDependencyRegistar
    {
        public int Priority { get { return 0; } }

        public void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            //data context
            containerBuilder.Register(c => new ConfigurationStoreContext(c.Resolve<ConfigurationConfig>().ConnectionStringName))
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            containerBuilder.RegisterType<DbConfigSettingFactory>()
                            .As<ISettingFactory>()
                            .InstancePerLifetimeScope()
                            .PropertiesAutowired();
        }
    }
}
