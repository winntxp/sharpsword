/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/17/2016 2:53:42 PM
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharpSword.Commands
{
    /// <summary>
    /// 命令处理器创建器
    /// </summary>
    public class CommandHandlerDescriptorBuilder
    {
        /// <summary>
        /// 根据指定的命令处理器，找出其合法的命令行（方法）
        /// </summary>
        /// <param name="commandHandlerType"></param>
        /// <returns></returns>
        public CommandHandlerDescriptor Build(Type commandHandlerType)
        {
            return new CommandHandlerDescriptor { Commands = CollectMethods(commandHandlerType) };
        }

        /// <summary>
        /// 查找指定类型里的所有合法命令
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private IEnumerable<CommandDescriptor> CollectMethods(Type type)
        {
            var methods = type
                            .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                            .Where(m => !m.IsSpecialName);

            foreach (var methodInfo in methods)
            {
                yield return BuildMethod(methodInfo);
            }
        }

        /// <summary>
        /// 创建一个命令描述对象
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        private CommandDescriptor BuildMethod(MethodInfo methodInfo)
        {
            return new CommandDescriptor(
                name: GetCommandName(methodInfo),
                methodInfo: methodInfo,
                helpText: GetCommandHelpText(methodInfo)
            );
        }

        /// <summary>
        /// 获取命令行帮助信息
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        private string GetCommandHelpText(MethodInfo methodInfo)
        {
            var attributes = methodInfo.GetCustomAttributes(typeof(CommandHelpAttribute), false/*inherit*/);
            if (!attributes.IsNull() && attributes.Any())
            {
                return attributes.Cast<CommandHelpAttribute>().Single().HelpText;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取命令行名称，如果没有自定义命令行名称，使用方法名称
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        private string GetCommandName(MethodInfo methodInfo)
        {
            var attributes = methodInfo.GetCustomAttributes(typeof(CommandNameAttribute), false/*inherit*/);
            if (!attributes.IsNull() && attributes.Any())
            {
                return attributes.Cast<CommandNameAttribute>().Single().Command;
            }

            //当方法名称有_的时候，我们将命令名称替换为空，比如：Get_AllActions ，命令名称为：Get AllActions
            return methodInfo.Name.Replace('_', ' ');
        }
    }

}
