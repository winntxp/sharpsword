/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/30/2015 9:09:00 AM
 * ****************************************************************/

namespace SharpSword.RealTime
{
    /// <summary>
    /// 
    /// </summary>
    public static class OnlineClientManagerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="onlineClientManager"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool IsOnline(IOnlineClientManager onlineClientManager, string userId)
        {
            return !onlineClientManager.GetByUserId(userId).IsNull();
        }
    }
}