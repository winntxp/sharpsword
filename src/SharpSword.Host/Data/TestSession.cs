/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/27/2016 5:29:51 PM
 * ****************************************************************/
using System.Web;
using SharpSword.Api.SDK;

namespace SharpSword.Host.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class TestSession : SessionBase, IApiClientUserPrivoder
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly HttpContextBase _httpContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        public TestSession(HttpContextBase httpContext)
        {
            this._httpContext = httpContext;
        }

        /// <summary>
        /// 
        /// </summary>
        public override string UserId
        {
            get { return "999"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string UserName
        {
            get { return "system"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Dispose()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ApiUser Get()
        {
            return new ApiUser(this.UserId, this.UserName);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MVCSession : TestSession
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        public MVCSession(HttpContextBase httpContext) : base(httpContext) { }

        /// <summary>
        /// 
        /// </summary>
        public string WID { get { return "200"; } }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class ISessionExt
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static MVCSession Get(this ISession session)
        {
            return session as MVCSession;
        }
    }
}
