/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/23/2016 2:46:28 PM
 * *******************************************************/
using System;

namespace SharpSword.Configuration
{
    /// <summary>
    /// 比如：框架内部的设置，我们可以从外部获取，也可以使用默认值，我们为了防止初始化的时候，系统未配置参数，
    /// 我们设置此标注，表示从外部获取配置失败，我们就返回系统默认值
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class FailReturnDefaultAttribute : Attribute { }
}
