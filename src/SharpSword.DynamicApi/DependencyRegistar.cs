/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/8/2016 12:25:58 PM
 * ****************************************************************/
using Autofac;

namespace SharpSword.DynamicApi
{
    /// <summary>
    /// IOC注册
    /// </summary>
    public class DependencyRegistar : DependencyRegistarBase
    {
        /// <summary>
        /// 我们将此插件服务定义在系统服务注册之后注册
        /// </summary>
        public override int Priority
        {
            get
            {
                return this.DefaultPriority + 99;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="typeFinder"></param>
        /// <param name="globalConfiguration"></param>
        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            //注册下默认的动态接口API查找器
            containerBuilder.RegisterType<DefaultDynamicApiSelector>()
                            .AsImplementedInterfaces()
                            .InstancePerLifetimeScope();

            //注册所有的动态API类
            var dynamicApiServices = typeFinder.FindClassesOfType<IDynamicApiService>();
            foreach (var servicesType in dynamicApiServices)
            {
                //注意注册类型，动态API我们不注册接口，直接注册自身类型
                containerBuilder.RegisterType(servicesType)
                                .PropertiesAutowired()
                                .InstancePerLifetimeScope();
            }
        }
    }
}
