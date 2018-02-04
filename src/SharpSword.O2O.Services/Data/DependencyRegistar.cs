using Autofac;
using SharpSword.Data;
/******************************************************************
* SharpSword zhangliang@sharpsword.com.cn 2015/11/20 18:49:15
* *******************************************************/

namespace SharpSword.Host.Data
{
    /// <summary>
    /// 框架会自动检测到这里的注册类,自动完成注册
    /// </summary>
    public class DependencyRegistar : IDependencyRegistar
    {
        /// <summary>
        /// 系统框架默认的会被覆盖;
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="typeFinder">类型查找器</param>
        /// <param name="globalConfiguration"></param>
        public void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            containerBuilder.RegisterType<TestSession>()
                            .AsImplementedInterfaces()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            containerBuilder.Register<O2ODbContext>(c => new O2ODbContext("MyDbContext"))
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            containerBuilder.Register(c => c.Resolve<IDbContextFactory>().Create(() => "MyDbContext"))
                            .As<IDbContext>()
                            .InstancePerLifetimeScope();
        }

        /// <summary>
        /// 数字越大越后注册
        /// </summary>
        public int Priority => int.MaxValue;
    }
}
