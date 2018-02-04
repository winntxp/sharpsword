/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/4/2016 4:10:50 PM
 * ****************************************************************/

namespace SharpSword
{
    /// <summary>
    /// 当前登录信息扩展
    /// </summary>
    public static class SessionExtensions
    {
        /// <summary>
        /// 获取当前登录用户编号
        /// </summary>
        /// <param name="session">ISession对象</param>
        /// <returns>返回转型成功的用户编号</returns>
        public static T GetUserId<T>(this ISession session)
        {
            if (session.UserId.IsNullOrEmpty())
            {
                throw new SharpSwordCoreException("Session.UserId 不能为null");
            }
            return session.UserId.As<T>();
        }

        /// <summary>
        /// 获取当前session上下文附带的数据
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key">数据键</param>
        /// <returns></returns>
        public static T GetData<T>(this ISession session, string key)
        {
            return (T)session.Properties.GetValueOrDefault(key);
        }
    }
}
