/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/
using System;
using System.Threading.Tasks;
using System.Web;

namespace SharpSword.OAuth
{
    /// <summary>
    /// 授权认证提供者抽象基类
    /// </summary>
    public abstract class AuthorizationProvider : IAuthorizationProvider
    {
        /// <summary>
        /// 生成请求授权URL地址
        /// </summary>
        /// <param name="application"></param>
        /// <param name="state"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        public abstract string GenerateUrl(App application, string state = "", string view = "");

        /// <summary>
        /// 根据异步获取token方法，转换成同步方法
        /// </summary>
        /// <param name="application"></param>
        /// <param name="callbackRequest"></param>
        /// <returns></returns>
        public virtual AuthorizationResult GetToken(App application, HttpRequestBase callbackRequest)
        {
            try
            {
                AuthorizationResult result = new AuthorizationResult();
                var tokenAysnResult = GetTokenAsync(application, callbackRequest);
                tokenAysnResult.Wait();
                return tokenAysnResult.Result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 异步获取token
        /// </summary>
        /// <param name="application"></param>
        /// <param name="callbackRequest"></param>
        /// <returns></returns>
        public abstract Task<AuthorizationResult> GetTokenAsync(App application, HttpRequestBase callbackRequest);
    }
}
