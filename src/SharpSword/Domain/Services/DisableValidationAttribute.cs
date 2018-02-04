/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/9/2016 4:53:20 PM
 * ****************************************************************/
using System;

namespace SharpSword.Domain.Services
{
    /// <summary>
    /// 对方法不进行验证参数的数据校验（系统默认所有被代理的类都会进行参数数据校验，如果不需要校验，请在方法上加此特性）
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DisableValidationAttribute : Attribute { }
}
