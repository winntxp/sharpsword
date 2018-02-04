/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/6/2017 8:55:10 AM
 * ****************************************************************/

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderSplitFactorServices : IOrderSplitFactorServices
    {
        /// <summary>
        /// 
        /// </summary>
        private static IOrderSplitFactorServices _instance = new OrderSplitFactorServices();

        /// <summary>
        /// 
        /// </summary>
        public static IOrderSplitFactorServices Instance => _instance;

        /// <summary>
        /// 
        /// </summary>
        private OrderSplitFactorServices() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        public int GetAreaFactor(long areaId)
        {
            if (areaId.ToString().Length >= 2)
            {
                return areaId.ToString().Substring(areaId.ToString().Length - 2, 2).As<int>();
            }
            return (int)areaId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public int GetAreaFactor(string orderId)
        {
            return orderId.Substring(3, 2).As<int>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetUserFactor(long userId)
        {
            if (userId.ToString().Length >= 2)
            {
                return userId.ToString().Substring(userId.ToString().Length - 2, 2).As<int>();
            }
            return (int)userId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public int GetUserFactor(string orderId)
        {
            return orderId.Substring(1, 2).As<int>();
        }
    }
}
