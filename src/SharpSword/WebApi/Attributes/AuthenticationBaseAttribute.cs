/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/1/25 8:59:16
 * ****************************************************************/
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 特定接口授权基类；需要实现接口自定义签名，授权校验的，请继承此类，重写IsValid方法即可；
    /// 然后将自己实现的特性类，附加到接口类上面即可）
    /// * *****************************************************************************************
    /// 为什么要定义此授权过滤器抽象基类，因为接口框架已经定义了一个IAuthentication授权接口，原因如下：
    /// 1.IAuthentication授权接口是统一的授权检测，比如参数正确性，加密是否正确等，即全局的授权判断
    /// 2.但是有些接口需要一个独自的授权判断，因此定义了一个授权基类来实现不同接口实现不同授权判断的情况
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class AuthenticationBaseAttribute : Attribute, IAuthentication
    {
        /// <summary>
        /// 优先级，越高越先执行判断;默认0
        /// </summary>
        public virtual int Order { get; set; }

        /// <summary>
        /// 默认实现全部返回true，继承类需要重写此方法，用于实际的业务逻辑授权判断
        /// </summary>
        /// <param name="requestContext">当前请求上下文</param>
        /// <returns>true/false</returns>
        public virtual AuthenticationResult Valid(RequestContext requestContext)
        {
            return AuthenticationResult.Success;
        }

    }
}
