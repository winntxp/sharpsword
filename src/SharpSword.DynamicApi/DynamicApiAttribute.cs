/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/7/2016 8:53:41 AM
 * ****************************************************************/
using System;

namespace SharpSword.DynamicApi
{
    /// <summary>
    /// 标识一个service方法是否可以在运行时动态生成API接口
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class DynamicApiAttribute : Attribute { }
}
