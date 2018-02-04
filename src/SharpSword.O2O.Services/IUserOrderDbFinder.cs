/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/30/2017 10:50:39 AM
 * ****************************************************************/
using System.Linq;
using Dapper;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 用户订单库分库策略
    /// </summary>
    public interface IUserOrderDbFinder : IDbFinder
    {
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="userIdSplitFactor">用户拆分因子（用户编号后2位）</param>
        /// <returns></returns>
        string GetDbConnectionString(int userIdSplitFactor);
    }
}
