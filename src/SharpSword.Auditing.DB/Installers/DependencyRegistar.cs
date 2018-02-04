/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/20 18:49:15
 * ****************************************************************/
using Autofac;
using SharpSword.WebApi;

namespace SharpSword.Auditing.SqlServer
{
    /// <summary>
    /// API框架会自动检测到这里的注册类,自动完成注册
    /// </summary>
    public class DependencyRegistar : DependencyRegistarBase
    {
        /// <summary>
        /// 系统框架默认的会被覆盖;
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="typeFinder">类型查找器</param>
        /// <param name="globalConfiguration">系统框架配置信息</param>
        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            //data context
            containerBuilder.Register(c => new AuditingStoreContext(c.Resolve<AuditingStoreConfig>().ConnectionStringName))
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            containerBuilder.RegisterType<SqlAuditingStore>()
                            .As<IAuditingStore>()
                            .InstancePerLifetimeScope();
        }

        /// <summary>
        /// 数字越大越后注册
        /// </summary>
        public override int Priority => 0;
    }
}
