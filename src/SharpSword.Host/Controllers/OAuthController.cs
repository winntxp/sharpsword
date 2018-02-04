/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/14/2016 1:45:18 PM
 * ****************************************************************/
using SharpSword.OAuth;

namespace SharpSword.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class OAuthController : MvcControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IOAuthManager _oauthManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oauthManager"></param>
        public OAuthController(IOAuthManager oauthManager)
        {
            this._oauthManager = oauthManager;
        }

        /// <summary>
        /// 
        /// </summary>     
        public void QQ()
        {
            //不建议这样获取应用信息(除非确保一个平台里只有一个应用被注册)
            var url = this._oauthManager.GetApp("qq").GenerateAuthorizationUrl("xxxxxx");
            this.HttpContext.Response.Redirect(url);
        }

        /// <summary>
        /// 
        /// </summary>
        public void CallBack()
        {
            var token = this._oauthManager.GetApp("qq", "206426").GetToken(this.HttpContext.Request);
            this.HttpContext.Response.Write(token);
        }
    }
}
