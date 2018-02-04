/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/15/2017 9:32:32 AM
 * ****************************************************************/
using System;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 订单排队服务抽象基类，我们抽象出此基类，来集中处理具体实现里的异常
    /// </summary>
    public abstract class OrderSequenceServicesBase : IOrderSequenceServices
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 报警器
        /// </summary>
        public ISystemWarningTrigger WarningTrigger { get; set; }

        /// <summary>
        /// 设置默认处理方式
        /// </summary>
        public OrderSequenceServicesBase()
        {
            this.Logger = GenericNullLogger<OrderSequenceServicesBase>.Instance;
            this.WarningTrigger = NullSystemWarningTrigger.Instance;
        }

        /// <summary>
        /// 获取排队的总人数
        /// </summary>
        /// <returns></returns>
        long IOrderSequenceServices.GetCount()
        {
            try
            {
                return this.GetCount();
            }
            catch (Exception ex)
            {
                //记录日志
                this.Logger.Error(ex);

                //报警
                this.WarningTrigger.Warning(this, "获取排队人数失败", ex);
            }

            //返回未知数量
            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract long GetCount();

        /// <summary>
        /// 开始排队
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        long IOrderSequenceServices.In(string token)
        {
            try
            {
                this.In(token);
            }
            catch (Exception ex)
            {
                //记录日志
                this.Logger.Error(ex);

                //报警
                this.WarningTrigger.Warning(this, "排队失败", ex);
            }

            //返回未知排名
            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        protected abstract long In(string token);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="orderProgress"></param>
        void IOrderSequenceServices.Out(string token, OrderProgress orderProgress)
        {
            try
            {
                this.Out(token, orderProgress);
            }
            catch (Exception ex)
            {
                //记录日志
                this.Logger.Error(ex);

                //报警
                this.WarningTrigger.Warning(this, "处理订单出现异常", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="orderProgress"></param>
        protected abstract void Out(string token, OrderProgress orderProgress);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        OrderProgress IOrderSequenceServices.GetOrderProgress(string token)
        {
            try
            {
                return this.GetOrderProgress(token);
            }
            catch (Exception ex)
            {
                //记录日志
                this.Logger.Error(ex);

                //报警
                this.WarningTrigger.Warning(this, "获取订单处理进度信息失败", ex);
            }

            return new OrderProgress()
            {
                Status = OrderProgressStatus.Unkonw,
                Rank = 0,
                Token = token,
                Description = "未知状态",
                OrderId = null
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        protected abstract OrderProgress GetOrderProgress(string token);
    }
}
