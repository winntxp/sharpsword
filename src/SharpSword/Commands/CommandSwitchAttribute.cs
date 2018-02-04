/* *******************************************************
 * SharpSword zhangliang4629@163.com 11/17/2016 2:36:19 PM
 * ****************************************************************/
using System;

namespace SharpSword.Commands
{
    /// <summary>
    /// 此特性用来标识，命令行类属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CommandSwitchAttribute : Attribute
    {
    }
}
