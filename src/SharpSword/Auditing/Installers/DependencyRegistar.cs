/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;

namespace SharpSword.Auditing.Installers
{
    /// <summary>
    /// 注册系统框架默认的审计实现
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
            containerBuilder.RegisterType<NullAuditInfoProvider>()
                            .As<IAuditInfoProvider>()
                            .SingleInstance();

            containerBuilder.Register(c => NullAuditingStore.Instance)
                            .As<IAuditingStore>();
        }
    }
}