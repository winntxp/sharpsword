/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/30/2017 10:50:39 AM
 * ****************************************************************/

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 用户库分库策略
    /// </summary>
    public interface IUserDbFinder : IDbFinder
    {
        /// <summary>
        /// 获取用户库分库策略连接字符串
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        string GetDbConnectionString(long userId);
    }
}
