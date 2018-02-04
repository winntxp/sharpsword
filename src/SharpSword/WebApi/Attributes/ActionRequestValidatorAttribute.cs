/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/1/16 8:51:11
 * ****************************************************************/
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 用于校验接口定义的特性，此接口会在获取到接口实例后，进行校验
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public abstract class ActionRequestValidatorAttribute : Attribute
    {
        /// <summary>
        /// 获取到校验结果
        /// </summary>
        /// <returns></returns>
        public abstract ActionRequestValidatorResult ValidForRequest(RequestContext requestContext);
    }
}
