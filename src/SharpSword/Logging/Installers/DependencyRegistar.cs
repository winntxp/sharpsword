/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;

namespace SharpSword.Logging.Installers
{
    internal class DependencyRegistar : DependencyRegistarBase
    {
        public override int Priority { get { return int.MinValue; } }

        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            containerBuilder.RegisterGeneric(typeof(GenericNullLogger<>))
                            .As(typeof(ILogger<>))
                            .SingleInstance();

            containerBuilder.Register(c => NullLogger.Instance)
                            .As(typeof(ILogger));
        }
    }
}