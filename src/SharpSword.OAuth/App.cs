/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/
using System.Threading.Tasks;
using System.Web;
using System;

namespace SharpSword.OAuth
{
    /// <summary>
    /// 描述平台中的应用，存储该应用的基本接口信息。
    /// 应用是平台用来管理接口调用权限的机制。业务系统方要访问平台接口必须先申请一个应用，经平台方审核通过后才具有调用相应接口的权限。
    /// </summary>
    [Serializable]
    public class App
    {
        /// <summary>
        /// 获取或设置应用在其所属平台的唯一标识。
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// 获取或设置应用的密钥。
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// 获取或设置应用的授权回调Url。
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// 获取应用所属的平台。
        /// </summary>
        public Platform Platform { get; private set; }

        /// <summary>
        /// 授权提供程序实例
        /// </summary>
        public IAuthorizationProvider Provider { get; private set; }

        /// <summary>
        /// 创建Application实例。
        /// </summary>
        /// <param name="platform">应用所属的平台。</param>
        /// <param name="provider">授权提供程序实例。</param>
        public App(Platform platform, IAuthorizationProvider provider) : base()
        {
            this.Platform = platform;
            this.Provider = provider;
        }

        /// <summary>
        /// 生成授权Url。
        /// </summary>
        /// <param name="state">状态参数。</param>
        /// <param name="view">授权页面的类型，如淘宝将授权页面分为web、tmall和wap三种类型。</param>
        public string GenerateAuthorizationUrl(string state, string view = "")
        {
            return this.Provider.GenerateUrl(this, state, view);
        }

        /// <summary>
        /// 接受回调请求后向平台换取Token。
        /// </summary>
        /// <param name="callbackRequest">回调请求。</param>
        public AuthorizationResult GetToken(HttpRequestBase callbackRequest)
        {
            return this.Provider.GetToken(this, callbackRequest);
        }

        /// <summary>
        /// 异步向平台换取Token。
        /// </summary>
        /// <param name="callbackRequest">回调请求。</param>
        public async Task<AuthorizationResult> GetTokenAsync(HttpRequestBase callbackRequest)
        {
            return await this.Provider.GetTokenAsync(this, callbackRequest);
        }

    }

}