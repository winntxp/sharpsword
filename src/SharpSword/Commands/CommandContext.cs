/* *******************************************************
 * SharpSword zhangliang4629@163.com 11/17/2016 2:23:08 PM
 * ****************************************************************/
using System.Collections.Generic;
using System.IO;

namespace SharpSword.Commands
{
    /// <summary>
    /// 命令行处理上下文信息
    /// </summary>
    public class CommandContext
    {
        /// <summary>
        /// 输入
        /// </summary>
        public TextReader Input { get; set; }

        /// <summary>
        /// 输出
        /// </summary>
        public TextWriter Output { get; set; }

        /// <summary>
        /// 命令名称
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// 命令行方法入参
        /// </summary>
        public IEnumerable<string> Arguments { get; set; }

        /// <summary>
        /// 命令行所需要使用的属性集合
        /// </summary>
        public IDictionary<string, string> Switches { get; set; }

        /// <summary>
        /// 命令行描述对象
        /// </summary>
        public CommandDescriptor CommandDescriptor { get; set; }
    }
}
