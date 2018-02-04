using Autofac;
using SharpSword;
using SharpSword.WebApi;
/******************************************************************
* SharpSword zhangliang@sharpsword.com.cn 2015/11/25 11:48:48
* *****************************************************************/

namespace ServiceCenter.Api.Core.Security
{
    /// <summary>
    /// API框架会自动检测到这里的注册类,自动完成注册
    /// </summary>
    public class DependencyRegistar : IDependencyRegistar
    {
        /// <summary>
        /// 系统框架默认的会被覆盖;
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="typeFinder">类型查找器</param>
        /// <param name="globalConfiguration">系统框架配置信息</param>
        public void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            containerBuilder.RegisterType<ApiSecurity>().As<IApiSecurity>().InstancePerLifetimeScope();
        }

        /// <summary>
        /// 数字越大越后注册
        /// </summary>
        public int Priority
        {
            get { return 1; }
        }
    }
}
