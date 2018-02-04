/* *******************************************************
 * SharpSword zhangliang4629@163.com 11/17/2016 2:36:50 PM
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.Commands
{
    /// <summary>
    /// 用来标注命令行必须包含哪些参数属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandSwitchesAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly string _switches;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="switches">命令必须含有的属性参数，多个属性参数使用,分开</param>
        public CommandSwitchesAttribute(string switches)
        {
            this._switches = switches;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="switches"></param>
        public CommandSwitchesAttribute(params string[] switches)
        {
            if (!switches.IsNull())
            {
                this._switches = switches.JoinToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<string> Switches
        {
            get
            {
                return (_switches ?? "").Trim().Split(',').Select(s => s.Trim());
            }
        }
    }
}
