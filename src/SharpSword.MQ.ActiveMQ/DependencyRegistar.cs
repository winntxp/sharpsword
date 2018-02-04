/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;

namespace SharpSword.MQ.ActiveMQ
{
    public class DependencyRegistar : DependencyRegistarBase
    {
        /// <summary>
        /// 
        /// </summary>
        public override int Priority { get { return 0; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="typeFinder"></param>
        /// <param name="globalConfiguration"></param>
        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            containerBuilder.RegisterType<ActiveMQManager>()
                            .As<IMessagePublisher>()
                            .SingleInstance();

            containerBuilder.RegisterType<ActiveMQManager>()
                            .As<IMessageConsumer>()
                            .SingleInstance();
        }
    }
}
