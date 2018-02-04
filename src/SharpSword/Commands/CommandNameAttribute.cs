/* *******************************************************
 * SharpSword zhangliang4629@163.com 11/17/2016 2:35:19 PM
 * ****************************************************************/
using System;

namespace SharpSword.Commands
{
    /// <summary>
    /// 用于标识一个方法对应的命令行名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandNameAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly string _commandAlias;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandAlias">命令行名称</param>
        public CommandNameAttribute(string commandAlias)
        {
            _commandAlias = commandAlias;
        }

        /// <summary>
        /// 命令行名称
        /// </summary>
        public string Command
        {
            get { return _commandAlias; }
        }
    }
}
