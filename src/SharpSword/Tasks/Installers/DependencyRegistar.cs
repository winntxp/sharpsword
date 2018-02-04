/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;
using SharpSword.Tasks.Impl;

namespace SharpSword.Tasks.Installers
{
    internal class DependencyRegistar : DependencyRegistarBase
    {
        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            //分布式作业任务协调器
            containerBuilder.RegisterType<DefaultTaskSchedulerDistributedLocker>()
                            .AsImplementedInterfaces()
                            .InstancePerLifetimeScope();
        }
    }
}