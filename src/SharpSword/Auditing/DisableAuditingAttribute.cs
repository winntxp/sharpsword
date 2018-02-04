/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/7/2016 10:29:04 AM
 * ****************************************************************/
using System;

namespace SharpSword.Auditing
{
    /// <summary>
    /// 标识一个方法禁用审计
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DisableAuditingAttribute : Attribute { }
}
