/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/9/2017 8:41:45 AM
 * ****************************************************************/
using SharpSword.Caching.Redis.StackExchange;
using SharpSword.O2O.Services.Domain;
using SharpSword.Timing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 我们自动完成确认管理器使用RDIS实现，如果后期防止RDIS重启丢失，我们可以重写此类，将数据持久化到MongoDB或者关系型数据中
    /// </summary>
    public class RedisOrderFinishedManager : OrderFinishedManagerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IRedisConnectionWrapper _redisConnectionWrapper;
        private readonly GlobalConfig _globalConfig;

        /// <summary>
        /// 
        /// </summary>
        private const string EXPIREDMEMBER = "O2O.Order.WaitFinished";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="redisConnectionWrapper"></param>
        /// <param name="globalConfig"></param>
        public RedisOrderFinishedManager(IRedisConnectionWrapper redisConnectionWrapper, GlobalConfig globalConfig)
        {
            this._redisConnectionWrapper = redisConnectionWrapper;
            this._globalConfig = globalConfig;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="finishedTime"></param>
        protected override void Add(string orderId, DateTime finishedTime)
        {
            this._redisConnectionWrapper.Database()
                                        .SortedSetAdd(EXPIREDMEMBER,
                                                      orderId,
                                                      Clock.Now.AddMinutes(this._globalConfig.OrderFinishedTime).Ticks);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        protected override void Remove(string orderId)
        {
            this._redisConnectionWrapper.Database().SortedSetRemove(EXPIREDMEMBER, orderId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<FinishedOrderInfo> GetFinishedOrders()
        {
            try
            {
                //先获取所有小于当前过期时间的订单(每次弹出1000个订单，方式大量订单过期读取数据占用大量内存)
                var finishedOrders = this._redisConnectionWrapper.Database()
                                                                 .SortedSetRangeByScoreWithScores(EXPIREDMEMBER, -1,
                                                                                                  Clock.Now.Ticks,
                                                                                                  skip: 0,
                                                                                                  take: 100);

                return finishedOrders.Select(x => new FinishedOrderInfo() { OrderId = x.Element, Ticks = (long)x.Score });
            }
            catch (Exception ex)
            {
                //记录下日志
                this.Logger.Error(ex);

                //触发报警
                this.WarningTrigger.Warning(this, ex.Message, ex);

                //返回空集合
                return new List<FinishedOrderInfo>();
            }

        }
    }
}
