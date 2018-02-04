/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/8/2017 5:32:06 PM
 * ****************************************************************/
using System;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 订单过期管理器，由于是分库分表，我们需要集中进行订单过期进行管理
    /// </summary>
    public interface IOrderExpiredManager
    {
        /// <summary>
        /// 将订单过期信息压入到订单过期管理器
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="expiredTime">过期时间</param>
        void Add(string orderId, DateTime expiredTime);

        /// <summary>
        /// 将订单从过期管理器里删除（比如：支付成功，手工取消，自动取消）
        /// </summary>
        /// <param name="orderId"></param>
        void Remove(string orderId);

        /// <summary>
        /// 进行过期处理，处理方式在具体实现里去实现，比如：轮训或者采取多线程等等
        /// </summary>
        /// <param name="action">
        /// 我们将处理委托给外部去实现，入参为订单ID，参数2：为过期时间戳
        /// 注意：只要不抛出异常，我们就或认为处理成功，订单将会从过期管理器里清楚
        /// (具体实现内部需要实现自动触发，即：外部调用只要提供业务逻辑操作委托即可，有需要自动调用)
        /// </param>
        void Job(Action<string, long> action);
    }
}
