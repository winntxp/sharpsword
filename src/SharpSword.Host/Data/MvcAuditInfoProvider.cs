/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/7/2016 5:10:20 PM
 * ****************************************************************/
using SharpSword.Auditing;
using System.Web;

namespace SharpSword.Host.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class MvcAuditInfoProvider : IAuditInfoProvider
    {
        /// <summary>
        /// 
        /// </summary>
        private HttpContextBase _httpContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        public MvcAuditInfoProvider(HttpContextBase httpContext)
        {
            this._httpContext = httpContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditInfo"></param>
        public void Fill(AuditInfo auditInfo)
        {
            auditInfo.ClientName = "安卓客户端";
            auditInfo.ClientIpAddress = this._httpContext.Request.GetClientIp();
            auditInfo.BrowserInfo = "未知浏览器";
        }
    }
}
