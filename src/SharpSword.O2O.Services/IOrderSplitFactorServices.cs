/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/30/2017 10:50:39 AM
 * ****************************************************************/

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 拆分因子提取器，订单生成规则：{校验码}[1]+{用户ID}[2]+{区域码}[2]+{流水号}[7] =13
    /// </summary>
    public interface IOrderSplitFactorServices
    {
        /// <summary>
        /// 根据订单编号，获取到订单编号上的用户因子（2位）
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        int GetUserFactor(string orderId);

        /// <summary>
        /// 根据用户ID获取到拆分因子(后2位)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        int GetUserFactor(long userId);

        /// <summary>
        /// 根据订单编号获取到区域因子（2位）
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        int GetAreaFactor(string orderId);

        /// <summary>
        /// 根据区域ID提取拆分因子（后2位）
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        int GetAreaFactor(long areaId);
    }
}
