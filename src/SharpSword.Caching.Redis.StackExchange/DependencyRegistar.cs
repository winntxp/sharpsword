using Autofac;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;
/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/26 11:59:37
 * ****************************************************************/

namespace SharpSword.Caching.Redis.StackExchange
{
    /// <summary>
    /// 注册新的Redis缓存服务器实现
    /// </summary>
    public class DependencyRegistar : DependencyRegistarBase
    {
        /// <summary>
        /// 注册缓存实现
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="typeFinder"></param>
        /// <param name="globalConfiguration">系统框架配置信息</param>
        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            containerBuilder.RegisterType<RedisCacheManager>()
                            .As<ICacheManager>()
                            .PropertiesAutowired()
                            .Named<ICacheManager>("redis")
                            .WithMetadata("name", "redis")
                            .InstancePerLifetimeScope();

            containerBuilder.RegisterType<NewtonsoftSerializer>()
                            .As<ISerializer>()
                            .SingleInstance();

            containerBuilder.RegisterType<StackExchangeRedisCacheClient>()
                            .As<ICacheClient>()
                            .SingleInstance();
        }

        /// <summary>
        /// 优先级
        /// </summary>
        public override int Priority => 3;
    }
}
