/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/26 11:59:37
 * ****************************************************************/
using Autofac;

namespace SharpSword.Caching.NullCacheManager
{
    /// <summary>
    /// 空实现，不使用缓存
    /// </summary>
    public class DependencyRegistar : IDependencyRegistar
    {
        /// <summary>
        /// 注册缓存实现
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="typeFinder"></param>
        /// <param name="globalConfiguration">系统框架配置信息</param>
        public void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            containerBuilder.RegisterType<NullCacheManager>()
                            .As<ICacheManager>()
                            .Named<ICacheManager>("NullCacheManager")
                            .InstancePerLifetimeScope();
        }

        /// <summary>
        /// 优先级注册为最大，方便调试
        /// </summary>
        public int Priority => int.MaxValue;
    }
}
