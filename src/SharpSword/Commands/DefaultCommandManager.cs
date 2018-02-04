/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/17/2016 2:31:50 PM
 * ****************************************************************/
using Autofac.Features.Metadata;
using SharpSword.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.Commands
{
    /// <summary>
    /// 默认命令行处理器
    /// </summary>
    public class DefaultCommandManager : ICommandManager
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IEnumerable<Meta<Func<ICommandHandler>>> _handlers;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handlers"></param>
        public DefaultCommandManager(IEnumerable<Meta<Func<ICommandHandler>>> handlers)
        {
            this._handlers = handlers;
            this.Logger = GenericNullLogger<DefaultCommandManager>.Instance;
            this.L = NullLocalizer.Instance;
        }

        /// <summary>
        /// 日志记录器
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 本地化
        /// </summary>
        public Localizer L { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <exception cref="SharpSwordCoreException">找不到或者找到多个命令行会抛出异常</exception>
        public void Execute(CommandParameters parameters)
        {
            var matches = this.MatchCommands(parameters);

            //匹配到命令行，执行指定命令
            if (1 == matches.Count())
            {
                var commandMatch = matches.Single();
                commandMatch.CommandHandlerFactory().Execute(commandMatch.Context);
            }
            else
            {
                string errorMessage = "";
                var commandMatch = string.Join(" ", parameters.Arguments.ToArray());
                var commandList = string.Join(",", GetCommandDescriptors().Select(d => d.Name).ToArray());

                //找到了多个命令
                if (matches.Any())
                {
                    errorMessage = this.L("找到多个命令行，匹配参数： \"{0}\". 命令: {1}",
                                                           commandMatch, commandList);
                    throw new SharpSwordCoreException(errorMessage);
                }

                //未找到命令行
                errorMessage = this.L("未找到匹配的命令行，参数： \"{0}\". 命令: {1}",
                                             commandMatch, commandList);
                throw new SharpSwordCoreException(errorMessage);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CommandDescriptor> GetCommandDescriptors()
        {
            return _handlers.SelectMany(h => GetDescriptor(h.Metadata).Commands);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private IEnumerable<CommandMatch> MatchCommands(CommandParameters parameters)
        {
            foreach (var argCount in Enumerable.Range(1, parameters.Arguments.Count()).Reverse())
            {
                int count = argCount;
                var matches = _handlers.SelectMany(h => MatchCommands(parameters, count, GetDescriptor(h.Metadata), h.Value)).ToList();
                if (matches.Any())
                {
                    return matches;
                }
            }

            return Enumerable.Empty<CommandMatch>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="argCount"></param>
        /// <param name="descriptor"></param>
        /// <param name="handlerFactory"></param>
        /// <returns></returns>
        private static IEnumerable<CommandMatch> MatchCommands(CommandParameters parameters, int argCount, CommandHandlerDescriptor descriptor, Func<ICommandHandler> handlerFactory)
        {
            foreach (var commandDescriptor in descriptor.Commands)
            {
                var names = commandDescriptor.Name.Split(' ');
                if (!parameters.Arguments.Take(argCount).SequenceEqual(names, StringComparer.OrdinalIgnoreCase))
                {
                    continue;
                }

                yield return new CommandMatch
                {
                    Context = new CommandContext
                    {
                        Arguments = parameters.Arguments.Skip(names.Count()),
                        Command = string.Join(" ", names),
                        CommandDescriptor = commandDescriptor,
                        Input = parameters.Input,
                        Output = parameters.Output,
                        Switches = parameters.Switches,
                    },
                    CommandHandlerFactory = handlerFactory
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        private static CommandHandlerDescriptor GetDescriptor(IDictionary<string, object> metadata)
        {
            return ((CommandHandlerDescriptor)metadata[typeof(CommandHandlerDescriptor).FullName]);
        }

        /// <summary>
        /// 
        /// </summary>
        private class CommandMatch
        {
            /// <summary>
            /// 
            /// </summary>
            public CommandContext Context { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Func<ICommandHandler> CommandHandlerFactory { get; set; }
        }
    }
}
