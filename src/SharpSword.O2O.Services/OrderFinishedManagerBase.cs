/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/15/2017 10:14:06 AM
 * ****************************************************************/
using SharpSword.O2O.Services.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 订单自动完成管理器抽象基类
    /// </summary>
    public abstract class OrderFinishedManagerBase : IOrderFinishedManager
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
        public OrderFinishedManagerBase()
        {
            this.Logger = GenericNullLogger<OrderFinishedManagerBase>.Instance;
            this.WarningTrigger = NullSystemWarningTrigger.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="finishedTime"></param>
        void IOrderFinishedManager.Add(string orderId, DateTime finishedTime)
        {
            this.Add(orderId, finishedTime);
        }

        /// <summary>
        /// 我们将实现交给具体实现去实现
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="finishedTime"></param>
        protected abstract void Add(string orderId, DateTime finishedTime);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        void IOrderFinishedManager.Remove(string orderId)
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
        void IOrderFinishedManager.Job(Action<string, long> action)
        {
            while (true)
            {
                //获取过期订单
                var finishedOrders = this.GetFinishedOrders();

                //没有过期订单，我们就暂停N秒钟
                if (finishedOrders.Count() <= 0)
                {
                    Thread.Sleep(SLEEP);
                    continue;
                }

                //循环处理
                finishedOrders.AsParallel().ForAll(item =>
                {
                    try
                    {
                        //执行操作
                        action(item.OrderId, item.Ticks);

                        //只要不抛出异常，我们就认为是处理成功了，将订单从过期管理器里删除
                        ((IOrderFinishedManager)this).Remove(item.OrderId);
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
        /// 获取已经过期的订单信息（当订单量大的时候，可以每次取出100调记录等
        /// 当然这要看具体的管理器实现，如果是NOSQL实现，可以一次性多取一点，如果是DB，可以取少点）
        /// </summary>
        /// <param name="action"></param>
        protected abstract IEnumerable<FinishedOrderInfo> GetFinishedOrders();

    }
}
