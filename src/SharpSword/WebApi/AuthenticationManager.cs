/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/25 11:41:20
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 权限校验管理器
    /// </summary>
    internal class AuthenticationManager
    {
        /// <summary>
        /// 
        /// </summary>
        private static AuthenticationManager _instance = new AuthenticationManager();

        /// <summary>
        /// 
        /// </summary>
        public static AuthenticationManager Instance => _instance;

        /// <summary>
        /// 
        /// </summary>
        private AuthenticationManager() { }

        /// <summary>
        /// 针对允许匿名和非匿名进行判断，如果接口允许匿名访问，接口将不走IAuthentication接口流程，直接返回校验成功；
        /// </summary>
        /// <param name="requestContext">当前请求上下文</param>
        /// <param name="actionDescriptor">接口描述对象</param>
        /// <returns>身份信息验证是否通过，通过返回：true，失败返回:false</returns>
        public AuthenticationResult Valid(RequestContext requestContext, IActionDescriptor actionDescriptor)
        {
            requestContext.CheckNullThrowArgumentNullException(nameof(requestContext));
            actionDescriptor.CheckNullThrowArgumentNullException(nameof(actionDescriptor));

            //获取接口所有的授权器，包括特性和全局注册的
            var authentications = actionDescriptor.Authentications;

            //循环授权器，看验证是否通过
            foreach (var authentication in authentications)
            {
                //允许匿名，不进行全局授权校验，但是要进行接口自定义的授权校验，全局接口，是直接实现：IAuthentication 的类
                if (actionDescriptor.AllowAnonymous && !(authentication is AuthenticationBaseAttribute))
                {
                    continue;
                }

                //授权校验
                var validResult = authentication.Valid(requestContext);

                //授权校验失败，直接返回，不进行后续的校验
                if (!validResult.IsValid)
                {
                    return validResult;
                }
            }

            //直接返回校验成功
            return AuthenticationResult.Success;
        }
    }
}
