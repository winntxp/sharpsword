﻿/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;

namespace SharpSword.Net.SMS
{
    /// <summary>
    /// 
    /// </summary>
    public class DependencyRegistar : DependencyRegistarBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="typeFinder"></param>
        /// <param name="globalConfiguration"></param>
        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            containerBuilder.RegisterType<NullSMSSender>()
                            .As<ISMSSender>()
                            .SingleInstance()
                            .PropertiesAutowired();
        }
    }
}
