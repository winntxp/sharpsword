/* *******************************************************
 * SharpSword zhangliang4629@163.com 11/17/2016 2:31:08 PM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.Commands
{
    /// <summary>
    /// 命令行管理器(用于接收用户的控制台输入)
    /// </summary>
    public interface ICommandManager : IDependency
    {
        /// <summary>
        /// 执行命令行
        /// </summary>
        /// <param name="parameters">从控制台输入的命令行信息</param>
        void Execute(CommandParameters parameters);

        /// <summary>
        /// 获取所有命令行描述集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<CommandDescriptor> GetCommandDescriptors();
    }
}
