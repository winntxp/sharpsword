/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/5/2017 5:44:54 PM
 * ****************************************************************/
using System.Data;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 用户维度拆库数据库连接对象扩展
    /// </summary>
    public static class IUserOrderDbConnectionFactoryExtensions
    {
        /// <summary>
        /// 用户维度拆分订单库数据库连接字符获取，具体实现里需要根据用户ID获取用户拆分因子
        /// </summary>
        /// <param name="userOrderDbConnectionFactory"></param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public static IDbConnection CreateByUserId(this IUserOrderDbConnectionFactory userOrderDbConnectionFactory, long userId)
        {
            return userOrderDbConnectionFactory.Create(OrderSplitFactorServices.Instance.GetUserFactor(userId));
        }

        /// <summary>
        /// 用户维度拆分订单库数据库连接字符获取，具体实现里需要根据订单编号获取用户拆分因子
        /// </summary>
        /// <param name="userOrderDbConnectionFactory"></param>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        public static IDbConnection CreateByOrderId(this IUserOrderDbConnectionFactory userOrderDbConnectionFactory, string orderId)
        {
            return userOrderDbConnectionFactory.Create(OrderSplitFactorServices.Instance.GetUserFactor(orderId));
        }
    }
}
