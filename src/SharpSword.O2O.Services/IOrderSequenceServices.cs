/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 5:11:14 PM
 * ****************************************************************/

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 排队信息（当前排名位置，当前订单排名处理结果）
    /// </summary>
    public interface IOrderSequenceServices
    {
        /// <summary>
        /// 获取排队总人数，用于限流
        /// </summary>
        /// <returns></returns>
        long GetCount();

        /// <summary>
        /// 入队，并返回排队排名
        /// </summary>
        /// <param name="token"></param>
        /// <returns>返回排队编号</returns>
        long In(string token);

        /// <summary>
        /// 排队成功。订单处理完成，出队
        /// </summary>
        /// <param name="token">票据号</param>
        /// <param name="orderProgress">处理完成的状态</param>
        void Out(string token, OrderProgress orderProgress);

        /// <summary>
        /// 获取订单处理进度状态
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        OrderProgress GetOrderProgress(string token);
    }
}
