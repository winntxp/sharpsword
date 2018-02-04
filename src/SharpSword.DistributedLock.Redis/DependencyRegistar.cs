/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;

namespace SharpSword.DistributedLock.Redis
{
    public class DependencyRegistar : DependencyRegistarBase
    {
        /// <summary>
        /// 优先级我们设置稍高点
        /// </summary>
        public override int Priority { get { return 1; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="typeFinder"></param>
        /// <param name="globalConfiguration"></param>
        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            containerBuilder.Register(c => new DistributedLockerManager(((IDistributedLockConfig)c.Resolve<DistributedLockConfig>()).RedisServers))
                            .As<IDistributedLockerManager>()
                            .SingleInstance();
        }
    }
}
