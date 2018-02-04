/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/12/2017 10:32:40 AM
 * ****************************************************************/

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 订单处理结果
    /// </summary>
    public enum SaveOrderResultStatus
    {
        /// <summary>
        /// 处理成功（订单已经保存到后端存储介质）
        /// </summary>
        OK,

        /// <summary>
        /// 缺少库存（当前订单里的商品库存不足以扣除）
        /// </summary>
        LOWSTOCKS,

        /// <summary>
        /// 处理出现异常（在持久化订单到存储介质的时候，出现异常，比如后端存储介质已经停止工作等等）
        /// </summary>
        ERROR
    }
}
