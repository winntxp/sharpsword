/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/5/2017 12:43:42 PM
 * ****************************************************************/
using System.Data;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 此接口是为方便编程定义的接口，用户ID获取用户订单分库策略
    /// </summary>
    public interface IUserDbConnectionFactory
    {
        /// <summary>
        /// 根据用户编号创建数据库连接
        /// </summary>
        /// <param name="userId">会员编号</param>
        /// <returns></returns>
        IDbConnection Create(long userId);
    }
}
