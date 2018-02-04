/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;

namespace SharpSword.RealTime.Installers
{
    internal class DependencyRegistar : DependencyRegistarBase
    {
        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            //注册在线客户端管理器，需要注册成全局单列(因为需要进行全局管理)
            containerBuilder.RegisterType<OnlineClientManager>()
                            .As<IOnlineClientManager>()
                            .SingleInstance();
        }
    }
}