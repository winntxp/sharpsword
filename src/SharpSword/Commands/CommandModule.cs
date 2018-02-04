/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/17/2016 2:52:23 PM
 * ****************************************************************/
using Autofac;
using Autofac.Core;
using System.Linq;

namespace SharpSword.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CommandModule : Module
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentRegistry"></param>
        /// <param name="registration"></param>
        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            if (!registration.Services.Contains(new TypedService(typeof(ICommandHandler))))
            {
                return;
            }
            var commandHandlerDescriptor = new CommandHandlerDescriptorBuilder().Build(registration.Activator.LimitType);
            registration.Metadata.Add(typeof(CommandHandlerDescriptor).FullName, commandHandlerDescriptor);
        }

    }
}
