/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/11/2017 3:39:28 PM
 * ****************************************************************/
using System;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 订单自动完成管理器，由于是分库分表，我们需要将完成收货同一进行管理
    /// </summary>
    public interface IOrderFinishedManager
    {
        /// <summary>
        /// 将支付完成的订单，压入到完成管理器
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="finishedTime">确认完成时间</param>
        void Add(string orderId, DateTime finishedTime);

        /// <summary>
        /// 从完成管理器里将订单删除
        /// </summary>
        /// <param name="orderId"></param>
        void Remove(string orderId);

        /// <summary>
        /// 完成订单操作(具体实现内部需要实现自动触发，即：外部调用只要提供业务逻辑操作委托即可，有需要自动完成订单实现会自动调用)
        /// </summary>
        /// <param name="action"></param>
        void Job(Action<string, long> action);

    }
}
