/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/25 11:39:31
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 身份校验返回对象;全局校验，在接口类上，如果定义了AllowAnonymousAttribute特性类，全局验证将不起作用
    /// </summary>
    public class AuthenticationResult
    {
        /// <summary>
        /// 我们将校验成功的对象设置成静态对象缓存起来
        /// </summary>
        private static AuthenticationResult _instance = new AuthenticationResult(true, "OK");

        /// <summary>
        /// 是否校验通过
        /// </summary>
        public bool IsValid { get; private set; }

        /// <summary>
        /// 错误或者成功返回的消息
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="isValid">是否校验通过</param>
        /// <param name="message">错误或者成功返回的消息</param>
        public AuthenticationResult(bool isValid, string message)
        {
            this.IsValid = isValid;
            this.Message = message;
        }

        /// <summary>
        /// 直接返回调用成功结果，方便我们开发使用
        /// </summary>
        public static AuthenticationResult Success
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// 验证失败返回验证对象结果
        /// </summary>
        /// <param name="message">错误或者成功返回的消息</param>
        /// <returns></returns>
        public static AuthenticationResult Fail(string message = "ERROR")
        {
            return new AuthenticationResult(false, message);
        }
    }

    /// <summary>
    /// 用于校验APPKEY身份，时间戳，上传数据签名，是否正确；具体实现交给外部去实现
    /// 注意：此接口校验器为全局，如果想要定义单独接口授权校验器，请实现：ActionAuthenticationBaseAttribute抽象类
    /// 注意此接口，注册为协作类型，即：多个注册实现授权器会按照优先级全部执行一次
    /// 为什么要设计成协作方式？
    /// 1。当我们很多时候，我们可能会出现扩展需求，在之前权限校验的基础上增加某些校验，这样如果不设计成协作接口，只能
    /// 继承原来的实现，然后重写，然后在注册。这样对于有时候扩展不是很方便
    /// </summary>
    public interface IAuthentication
    {
        /// <summary>
        /// 用于排序优先级，越高越先执行;但是全局实现的接口肯定优先于特性接口
        /// </summary>
        int Order { get; }

        /// <summary>
        /// 验证身份是否通过；校验的时候请使用原始的请求参数，即：RequestContext.RawRequestParams参数进行校验
        /// </summary>
        /// <param name="requestContext">请求参数</param>
        /// <returns>校验成功返回true,失败返回false</returns>
        AuthenticationResult Valid(RequestContext requestContext);
    }
}
