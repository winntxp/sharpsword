/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/17/2016 2:23:46 PM
 * ****************************************************************/
using System.Reflection;

namespace SharpSword.Commands
{
    /// <summary>
    /// 命令行描述信息
    /// </summary>
    public class CommandDescriptor
    {
        /// <summary>
        /// 命令行描述
        /// </summary>
        /// <param name="name">命令行名称</param>
        /// <param name="methodInfo">处理方法信息</param>
        /// <param name="helpText">命令行帮助说明</param>
        public CommandDescriptor(string name, MethodInfo methodInfo, string helpText)
        {
            this.Name = name;
            this.MethodInfo = methodInfo;
            this.HelpText = helpText;
        }

        /// <summary>
        /// 命令行名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 处理方法信息
        /// </summary>
        public MethodInfo MethodInfo { get; set; }

        /// <summary>
        /// 命令行帮助说明
        /// </summary>
        public string HelpText { get; set; }
    }
}
