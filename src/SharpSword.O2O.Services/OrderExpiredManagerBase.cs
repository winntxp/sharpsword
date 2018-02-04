/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/15/2017 10:04:45 AM
 * ****************************************************************/
using SharpSword.O2O.Services.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 订单过期管理器抽象基类
    /// </summary>
    public abstract class OrderExpiredManagerBase : IOrderExpiredManager
    {
        /// <summary>
        /// 间隔时间，单位：毫秒
        /// </summary>
        private const int SLEEP = 5000;

        /// <summary>
        /// 日志记录器
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 报警器
        /// </summary>
        public ISystemWarningTrigger WarningTrigger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OrderExpiredManagerBase()
        {
            this.Logger = GenericNullLogger<OrderExpiredManagerBase>.Instance;
            this.WarningTrigger = NullSystemWarningTrigger.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="expiredTime"></param>
        void IOrderExpiredManager.Add(string orderId, DateTime expiredTime)
        {
            this.Add(orderId, expiredTime);
        }

        /// <summary>
        /// 我们将实现交给具体实现去实现
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="expiredTime"></param>
        protected abstract void Add(string orderId, DateTime expiredTime);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        void IOrderExpiredManager.Remove(string orderId)
        {
            this.Remove(orderId);
        }

        /// <summary>
        /// 我们将实现交给具体实现去实现
        /// </summary>
        /// <param name="orderId"></param>
        protected abstract void Remove(string orderId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        void IOrderExpiredManager.Job(Action<string, long> action)
        {
            while (true)
            {
                //先获取所有小于当前过期时间的订单(每次弹出1000个订单，方式大量订单过期读取数据占用大量内存)
                var expiredOrders = this.GetExpiredOrders();

                //没有过期订单，我们就暂停1秒钟
                if (expiredOrders.Count() <= 0)
                {
                    Thread.Sleep(SLEEP);
                    continue;
                }

                //并行处理下订单
                expiredOrders.AsParallel().ForAll(item =>
                {
                    try
                    {
                        //执行操作
                        action(item.OrderId, item.Ticks);

                        //只要不抛出异常，我们就认为是处理成功了，将订单从过期管理器里删除
                        ((IOrderExpiredManager)this).Remove(item.OrderId);
                    }
                    catch (Exception ex)
                    {
                        //记录下日志
                        this.Logger.Error(ex);
                        //报警
                        this.WarningTrigger.Warning(this, ex.Message, ex);
                    }
                });
            }
        }

        /// <summary>
        /// 获取已经过期的订单信息;（当订单量大的时候，可以每次取出100调记录等，
        /// 当然这要看具体的管理器实现，如果是NOSQL实现，可以一次性多取一点，如果是DB，可以取少点）
        /// </summary>
        /// <param name="action"></param>
        protected abstract IEnumerable<ExpiredOrderInfo> GetExpiredOrders();
    }
}
