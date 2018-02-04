/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/25 11:41:20
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 全局默认的身份校验器，默认使用MD5签名
    /// </summary>
    public class DefaultAuthentication : IAuthentication
    {
        /// <summary>
        /// 默认返回true，全部通过，任何APPKEY都可以访问接口
        /// </summary>
        /// <param name="requestContext">当前请求上下文</param>
        /// <returns>身份信息验证是否通过，通过返回：true，失败返回:false</returns>
        public virtual AuthenticationResult Valid(RequestContext requestContext)
        {
            return AuthenticationResult.Success;
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order => int.MinValue;
    }
}
