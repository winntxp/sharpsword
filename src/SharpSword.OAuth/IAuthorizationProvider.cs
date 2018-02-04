/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using System.Web;
using System.Threading.Tasks;

namespace SharpSword.OAuth
{
    /// <summary>
    /// 授权提供程序接口。
    /// </summary>
    public interface IAuthorizationProvider
    {
        /// <summary>
        /// 生成授权Url。
        /// </summary>
        /// <param name="application">平台应用</param>
        /// <param name="state">状态参数。</param>
        /// <param name="view">授权页面的类型，如淘宝将授权页面分为web、tmall和wap三种类型。</param>
        string GenerateUrl(App application, string state = "", string view = "");

        /// <summary>
        /// 接受回调请求后向平台换取Token。
        /// </summary>
        /// <param name="application">平台应用</param>
        /// <param name="callbackRequest">回调请求。</param>
        AuthorizationResult GetToken(App application, HttpRequestBase callbackRequest);

        /// <summary>
        /// 异步向平台换取Token。
        /// </summary>
        /// <param name="application">平台应用</param>
        /// <param name="callbackRequest">回调请求。</param>
        Task<AuthorizationResult> GetTokenAsync(App application, HttpRequestBase callbackRequest);
    }

}