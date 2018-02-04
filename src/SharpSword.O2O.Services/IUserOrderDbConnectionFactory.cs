/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/5/2017 12:43:13 PM
 * ****************************************************************/
using System.Data;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 此接口是为方便编程定义的接口，用户根据订单编号获取用户订单分库策略
    /// </summary>
    public interface IUserOrderDbConnectionFactory
    {
        /// <summary>
        /// 根据订单编号创建数据库连接
        /// </summary>
        /// <param name="userIdSplitFactor">用户编号拆分库因子（用户ID后2位）</param>
        /// <returns></returns>
        IDbConnection Create(int userIdSplitFactor);
    }
}
