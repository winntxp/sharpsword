/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;
using SharpSword.DtoValidator.Impl;

namespace SharpSword.DtoValidator.Installers
{
    internal class DependencyRegistar : DependencyRegistarBase
    {
        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            //RequestDto验证管理类
            containerBuilder.RegisterType<DefaultDtoValidatorManager>()
                            .As<IDtoValidatorManager>()
                            .InstancePerLifetimeScope();

            //RequestDto视图默认实现验证类
            containerBuilder.RegisterType<DefaultDtoValidator>()
                            .As<IDtoValidator>()
                            .SingleInstance();
        }
    }
}