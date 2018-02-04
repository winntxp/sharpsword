/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/17/2016 2:32:35 PM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.Commands
{
    /// <summary>
    /// 命令处理类描述对象
    /// </summary>
    public class CommandHandlerDescriptor
    {
        /// <summary>
        /// 命令处理类里，所有合法的命令行集合
        /// </summary>
        public IEnumerable<CommandDescriptor> Commands { get; set; }
    }
}
