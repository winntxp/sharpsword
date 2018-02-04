﻿/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;

namespace SharpSword.Domain.Uow
{
    internal class DependencyRegistar : DependencyRegistarBase
    {
        public override int Priority { get { return 0; } }

        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            containerBuilder.RegisterType<CallContextCurrentUnitOfWorkProvider>()
                            .As<ICurrentUnitOfWorkProvider>()
                            .InstancePerLifetimeScope();

            containerBuilder.RegisterType<UnitOfWorkManager>()
                            .As<IUnitOfWorkManager>()
                            .InstancePerLifetimeScope();
        }
    }
}
