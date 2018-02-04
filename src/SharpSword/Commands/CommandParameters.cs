/* ****************************************************************
 * SharpSword zhangliang4629@163.com 11/17/2016 2:26:27 PM
 * ****************************************************************/
using System.Collections.Generic;
using System.IO;

namespace SharpSword.Commands
{
    /// <summary>
    /// 命令行参数
    /// </summary>
    public class CommandParameters
    {
        /// <summary>
        /// 命令行名称或者方法参数，如：help help commands
        /// </summary>
        public IEnumerable<string> Arguments { get; set; }

        /// <summary>
        /// 命令行可选参数（属性），如： /Name:sharpsword /Version:1.0.0.0
        /// </summary>
        public IDictionary<string, string> Switches { get; set; }

        /// <summary>
        /// 输入流
        /// </summary>
        public TextReader Input { get; set; }

        /// <summary>
        /// 输出流
        /// </summary>
        public TextWriter Output { get; set; }
    }
}
