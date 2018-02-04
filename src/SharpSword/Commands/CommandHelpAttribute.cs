/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/17/2016 2:42:31 PM
 * ****************************************************************/
using System;

namespace SharpSword.Commands
{
    /// <summary>
    /// 命令行帮助说明
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandHelpAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="helpText">帮助说明</param>
        public CommandHelpAttribute(string helpText)
        {
            this.HelpText = helpText;
        }

        /// <summary>
        /// 帮助说明
        /// </summary>
        public string HelpText { get; private set; }
    }
}
