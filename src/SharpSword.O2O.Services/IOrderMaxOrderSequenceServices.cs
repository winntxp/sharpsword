/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/15/2017 2:13:54 PM
 * ****************************************************************/

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 获取订单最大ID流水编号
    /// </summary>
    public interface IOrderMaxOrderSequenceServices
    {
        /// <summary>
        /// 获取最大订单编号
        /// </summary>
        /// <returns></returns>
        string GetMaxOrderId();
    }
}
