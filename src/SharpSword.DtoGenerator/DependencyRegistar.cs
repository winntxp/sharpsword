/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/8/2016 12:25:58 PM
 * ****************************************************************/
using Autofac;

namespace SharpSword.DtoGenerator
{
    /// <summary>
    /// IOC注册
    /// </summary>
    public class DependencyRegistar : DependencyRegistarBase
    {
        /// <summary>
        /// 这里注册顺序我们紧跟系统框架注册后面，防止数据访问上下文覆盖其他数据访问上下文
        /// </summary>
        public override int Priority => this.DefaultPriority + 1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="typeFinder"></param>
        /// <param name="globalConfiguration"></param>
        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            containerBuilder.Register(c => new DtoGeneratorDbContext(() => c.Resolve<DtoGeneratorConfig>().ConnectionStringName))
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            //重新注册控制器，注入新的数据访问上下文
            containerBuilder.RegisterType(typeof(Host.DtoGeneratorController))
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            //重新注册下接口
            containerBuilder.RegisterType(typeof(Actions.DtoGeneratorAction))
                            .PropertiesAutowired()
                            //.WithParameter(ResolvedParameter.ForNamed<IDbContext>(dbContextName))
                            .InstancePerLifetimeScope();
        }
    }
}
