/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/27/2016 5:29:51 PM
 * ****************************************************************/
using System.Web;

namespace SharpSword.O2O.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class UserSession : SessionBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly HttpContextBase _httpContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        public UserSession(HttpContextBase httpContext)
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
    }

    /// <summary>
    /// 
    /// </summary>
    public class MVCSession : UserSession
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
