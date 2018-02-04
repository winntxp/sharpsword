/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/12/2016 1:11:45 PM
 * ****************************************************************/
using System;

namespace SharpSword.DynamicApi
{
    /// <summary>
    /// 将一个合法的动态API接口，定义成非动态API接口（不映射）
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class NotDynamicApiAttribute : Attribute { }
}
