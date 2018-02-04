/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/5/2017 5:46:46 PM
 * ****************************************************************/

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 
    /// </summary>
    public static class IUserOrderDbFinderExtensions
    {
        /// <summary>
        /// 根据用户ID获取用户订单分库连接字符串
        /// </summary>
        /// <param name="userOrderDbFinder"></param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public static string GetDbConnectionStringByUserId(this IUserOrderDbFinder userOrderDbFinder, long userId)
        {
            return userOrderDbFinder.GetDbConnectionString(OrderSplitFactorServices.Instance.GetUserFactor(userId));
        }

        /// <summary>
        /// 根据订单编号获取用户订单分库连接字符串
        /// </summary>
        /// <param name="userOrderDbFinder"></param>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        public static string GetDbConnectionStringByOrderId(this IUserOrderDbFinder userOrderDbFinder, string orderId)
        {
            return userOrderDbFinder.GetDbConnectionString(OrderSplitFactorServices.Instance.GetUserFactor(orderId));
        }
    }
}
