/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/5/2017 5:39:58 PM
 * ****************************************************************/
using System.Data;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 
    /// </summary>
    public static class IAreaOrderDbConnectionFactoryExtensions
    {
        /// <summary>
        /// 区域维度订单拆库连接对象获取，具体实现里需要根据区域ID获取区域拆分因子
        /// </summary>
        /// <param name="areaOrderDbConnectionFactory"></param>
        /// <param name="areaId">区域ID</param>
        /// <returns></returns>
        public static IDbConnection CreateByAreaId(this IAreaOrderDbConnectionFactory areaOrderDbConnectionFactory, long areaId)
        {
            return areaOrderDbConnectionFactory.Create(OrderSplitFactorServices.Instance.GetAreaFactor(areaId));
        }

        /// <summary>
        /// 区域维度订单拆库连接对象获取，具体实现里需要根据订单编号获取区域拆分因子
        /// </summary>
        /// <param name="areaOrderDbConnectionFactory"></param>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        public static IDbConnection CreateByOrderId(this IAreaOrderDbConnectionFactory areaOrderDbConnectionFactory, string orderId)
        {
            return areaOrderDbConnectionFactory.Create(OrderSplitFactorServices.Instance.GetAreaFactor(orderId));
        }
    }
}
