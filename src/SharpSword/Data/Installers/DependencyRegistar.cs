﻿/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;

namespace SharpSword.Data.Installers
{
    internal class DependencyRegistar : DependencyRegistarBase
    {  
        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            containerBuilder.Register(c => NullSqlTraceManager.Instance)
                            .As<ISqlTraceManager>();
        }
    }
}