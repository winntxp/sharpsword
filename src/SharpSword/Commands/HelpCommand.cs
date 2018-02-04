/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/17/2016 2:45:09 PM
 * ****************************************************************/
using SharpSword.Timing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharpSword.Commands
{
    /// <summary>
    /// 系统命令行帮助命令
    /// </summary>
    public class HelpCommand : CommandHandlerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ICommandManager _commandManager;
        private readonly IIocResolver _iocResolver;
        private readonly HttpContextBase _httpContext;
        private readonly Lazy<IMachineNameProvider> _machineNameProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandManager"></param>
        /// <param name="iocResolver"></param>
        /// <param name="httpContext"></param>
        /// <param name="machineNameProvider"></param>
        public HelpCommand(ICommandManager commandManager,
                           IIocResolver iocResolver,
                           HttpContextBase httpContext,
                           Lazy<IMachineNameProvider> machineNameProvider)
        {
            this._commandManager = commandManager;
            this._iocResolver = iocResolver;
            this._httpContext = httpContext;
            this._machineNameProvider = machineNameProvider;
        }

        /// <summary>
        /// 获取所有命令
        /// </summary>
        [CommandName("help commands")]
        [CommandHelp("help commands \r\n\t 显示所有命令行信息")]
        public void AllCommands()
        {
            Context.Output.WriteLine("List of available commands:");
            Context.Output.WriteLine("---------------------------");

            var descriptors = _commandManager.GetCommandDescriptors().OrderBy(d => d.Name);
            foreach (var descriptor in descriptors)
            {
                Context.Output.WriteLine(this.GetHelpText(descriptor));
                Context.Output.WriteLine();
            }
        }

        /// <summary>
        /// 获取指定命令帮助信息
        /// </summary>
        /// <param name="commandNameStrings"></param>
        [CommandName("help")]
        [CommandHelp("help [command] \r\n\t 显示命令行帮助")]
        public void SingleCommand(string[] commandNameStrings)
        {
            string command = string.Join(" ", commandNameStrings);
            var descriptors = _commandManager.GetCommandDescriptors()
                                             .Where(t => t.Name.StartsWith(command, StringComparison.OrdinalIgnoreCase))
                                             .OrderBy(d => d.Name);
            if (!descriptors.Any())
            {
                Context.Output.WriteLine("命令 {0} 不存在", command);
            }
            else
            {
                foreach (var descriptor in descriptors)
                {
                    Context.Output.WriteLine(GetHelpText(descriptor));
                    Context.Output.WriteLine();
                }
            }
        }

        /// <summary>
        /// 获取所有注册的缓存策略
        /// </summary>
        /// <returns></returns>
        [CommandHelp("help caches \r\n\t 显示所有注册的缓存策略")]
        [CommandName("help caches")]
        public void CacheReged()
        {
            var cacheManagers = this._iocResolver.Resolve<IEnumerable<ICacheManager>>();
            Context.Output.WriteLine("List of available caches:");
            Context.Output.WriteLine("Type--------Assembly--------Version--------");
            foreach (var cacheManager in cacheManagers)
            {
                var t = cacheManager.GetType();
                this.Context.Output.WriteLine("{0} \t {1} \t {2}".With(t.FullName, t.Assembly.GetName().Name, t.Assembly.GetName().Version));
            }
        }

        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <returns></returns>
        [CommandHelp("help time \r\n\t 获取当前服务器时间")]
        [CommandName("help time")]
        public void GetTime()
        {
            Context.Output.WriteLine("server time : \t {0}".With(Clock.Now.ToString("yyyy/MM/dd HH:mm:ss.ffffff")));
        }

        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <returns></returns>
        [CommandHelp("help server \r\n\t 获取服务器环境信息")]
        [CommandName("help server")]
        public void GetServerInfo()
        {
            Context.Output.WriteLine("MachineName:{0}".With(this._machineNameProvider.Value.GetMachineName()));
            Context.Output.WriteLine("--------------------------------------------------");
            var serverVariables = this._httpContext.Request.ServerVariables;
            for (int i = 0; i < serverVariables.Count; i++)
            {
                Context.Output.WriteLine("{0} \t:\t {1}".With(serverVariables.Keys[i], serverVariables[i]));
            }
        }

        /// <summary>
        /// 获取帮助信息
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        private string GetHelpText(CommandDescriptor descriptor)
        {
            if (string.IsNullOrEmpty(descriptor.HelpText))
            {
                return "{0}.{1}: 没有帮助信息".With(descriptor.MethodInfo.DeclaringType.FullName, descriptor.MethodInfo.Name);
            }
            return descriptor.HelpText;
        }
    }
}
