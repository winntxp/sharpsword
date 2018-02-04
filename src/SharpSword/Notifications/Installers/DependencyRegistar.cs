/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;

namespace SharpSword.Notifications.Installers
{
    /// <summary>
    /// 
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
            //注册一个空的配置，正式环境里需要重新注册，覆盖掉空实现
            containerBuilder.Register(c => new NotificationConfiguration())
                            .As<INotificationConfiguration>()
                            .SingleInstance();

            //注册一个空实现，防止出错
            containerBuilder.RegisterType<NullRealTimeNotifier>()
                            .As<IRealTimeNotifier>()
                            .InstancePerLifetimeScope();
        }
    }
}