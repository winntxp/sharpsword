/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 5:16:33 PM
 * ****************************************************************/
using SharpSword.O2O.Data.Entities;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 订单号生成器 ；考虑到以后分表分开策略，我们将生成订单ID单独作为一个服务提取出来
    /// </summary>
    public interface IOrderIdGenerator
    {
        /// <summary>
        /// 创建订单编号
        /// {校验码}[1]+{用户ID}[2]+{区域码}[2]+{流水号}[7] =13
        /// </summary>
        /// <param name="order">订单信息</param>
        /// <returns></returns>
        string Create(Order order);

        /// <summary>
        /// 校验订单编号是否合法
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        bool Check(string orderId);
    }
}
