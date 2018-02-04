/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/7/2016 10:19:50 AM
 * ****************************************************************/
using System;

namespace SharpSword.Auditing
{
    /// <summary>
    /// 用于标识一个类方法是否需要进行审计存储
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuditedAttribute : Attribute { }
}
