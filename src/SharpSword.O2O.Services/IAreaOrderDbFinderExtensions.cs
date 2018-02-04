/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/5/2017 5:42:11 PM
 * ****************************************************************/

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 
    /// </summary>
    public static class IAreaOrderDbFinderExtensions
    {
        /// <summary>
        /// 区域维度拆库连接字符串获取，实现里需要实现根据区域ID取出区域拆分因子
        /// </summary>
        /// <param name="areaOrderDbFinder"></param>
        /// <param name="areaId">区域ID</param>
        /// <returns></returns>
        public static string GetDbConnectionStringByAreaId(this IAreaOrderDbFinder areaOrderDbFinder, long areaId)
        {
            return areaOrderDbFinder.GetDbConnectionString(OrderSplitFactorServices.Instance.GetAreaFactor(areaId));
        }

        /// <summary>
        /// 区域维度拆库连接字符串获取，实现里需要实现根据订单ID取出区域维度拆分因子
        /// </summary>
        /// <param name="areaOrderDbFinder"></param>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        public static string GetDbConnectionStringByOrderId(this IAreaOrderDbFinder areaOrderDbFinder, string orderId)
        {
            return areaOrderDbFinder.GetDbConnectionString(OrderSplitFactorServices.Instance.GetAreaFactor(orderId));
        }
    }
}
