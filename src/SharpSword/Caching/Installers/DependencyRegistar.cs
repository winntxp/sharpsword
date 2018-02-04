/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;
using SharpSword.Caching.Impl;

namespace SharpSword.Caching.Installers
{
    /// <summary>
    /// 注册系统框架默认的缓存实现
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
            //接口缓存器，默认使用当前进程内存作为缓存器
            containerBuilder.RegisterType<PerRequestCacheManager>()
                            .As<ICacheManager>()
                            .PropertiesAutowired()
                            .Named<ICacheManager>("cache_per_request")
                            .InstancePerLifetimeScope()
                            .WithMetadata("name", "PreRequest");

            //默认内存缓存实现
            containerBuilder.RegisterType<MemoryCacheManager>()
                            .As<ICacheManager>()
                            .PropertiesAutowired()
                            .Named<ICacheManager>("cache_static")
                            .SingleInstance()
                            .WithMetadata("name", "Memory");
        }
    }
}