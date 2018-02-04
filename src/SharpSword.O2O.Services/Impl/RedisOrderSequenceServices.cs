/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 5:11:14 PM
 * ****************************************************************/
using SharpSword.Caching.Redis.StackExchange;
using System;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 排队实现
    /// </summary>
    public class RedisOrderSequenceServices : OrderSequenceServicesBase, ITransientDependency
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly Lazy<ICacheManager> _cacheManager;
        private readonly IRedisConnectionWrapper _redisConnectionWrapper;

        /// <summary>
        /// 订单排队集合（有序集合）KEY
        /// </summary>
        private const string SEQUENCEKEYMEMBER = "O2O.Order.Rank";

        /// <summary>
        /// 排单排队集合，TOKEN键
        /// </summary>
        private const string SEQUENCEKEY = "{0}";

        /// <summary>
        /// 订单排队处理状态缓存键
        /// </summary>
        private const string STATUSKEY = "O2O.Order.Status:{0}";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="redisConnectionWrapper"></param>
        public RedisOrderSequenceServices(IRedisConnectionWrapper redisConnectionWrapper) : base()
        {
            this._redisConnectionWrapper = redisConnectionWrapper;
            this._cacheManager = new Lazy<ICacheManager>(() => new RedisCacheManager(redisConnectionWrapper));
            this.Logger = GenericNullLogger<RedisOrderSequenceServices>.Instance;
            this.WarningTrigger = NullSystemWarningTrigger.Instance;
        }

        /// <summary>
        /// 获取排队总人数
        /// </summary>
        /// <returns></returns>
        protected override long GetCount()
        {
            return this._redisConnectionWrapper.Database()
                                               .SortedSetLength(SEQUENCEKEYMEMBER);
        }

        /// <summary>
        /// 防止分布式系统里实际出现不同步情况，我们直接使用redis服务器时间
        /// </summary>
        /// <returns></returns>
        private DateTime GetTime()
        {
            foreach (var ep in _redisConnectionWrapper.GetEndpoints())
            {
                var server = _redisConnectionWrapper.Server(ep);
                return server.Time().AddHours(8);
            }
            return DateTime.Now;
        }

        /// <summary>
        /// 获取到购买资格，入队，便于后续用户定时查看他当前排名
        /// </summary>
        /// <param name="token"></param>
        protected override long In(string token)
        {
            var db = this._redisConnectionWrapper.Database();

            //排队
            var result = db.SortedSetAdd(SEQUENCEKEYMEMBER, SEQUENCEKEY.With(token), this.GetTime().Ticks);

            //获取排队信息
            if (!result)
            {
                return 0;
            }

            //获取排名
            var rank = db.SortedSetRank(SEQUENCEKEYMEMBER, SEQUENCEKEY.With(token));

            //返回排名
            return rank.HasValue ? rank.Value : 0;
        }

        /// <summary>
        /// 处理完成，出队
        /// </summary>
        /// <param name="token"></param>
        /// <param name="orderProgress"></param>
        protected override void Out(string token, OrderProgress orderProgress)
        {
            var db = this._redisConnectionWrapper.Database();

            //反馈处理结果
            db.StringSet(STATUSKEY.With(token), orderProgress.Serialize2Josn(), new TimeSpan(0, 15, 0));

            //删除排队
            db.SortedSetRemove(SEQUENCEKEYMEMBER, SEQUENCEKEY.With(token));

        }

        /// <summary>
        /// 我们先查询订单状态十分有缓存，如果不存在，我们直接查询排队信息（如果查询出来消息已经被处理了，那么调用此方法会直接删除执行结构状态信息）
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        protected override OrderProgress GetOrderProgress(string token)
        {
            var db = this._redisConnectionWrapper.Database();

            //检测是否已经处理完毕
            var orderProgress = this._cacheManager.Value.Get<OrderProgress>(STATUSKEY.With(token));
            if (!orderProgress.IsNull())
            {
                //删除排队键
                db.SortedSetRemove(SEQUENCEKEYMEMBER, SEQUENCEKEY.With(token));

                //存在，我们直接删除掉缓存处理状态信息
                db.KeyDelete(STATUSKEY.With(token));

                //返回处理状态
                return orderProgress;
            }

            //检测是否在排队
            var rank = db.SortedSetRank(SEQUENCEKEYMEMBER, SEQUENCEKEY.With(token));
            if (rank.HasValue && rank.Value >= 0)
            {
                return new OrderProgress()
                {
                    Rank = rank.Value,
                    Token = token,
                    Status = OrderProgressStatus.Queuing,
                    Description = "排队中"
                };
            }

            //2个地方我们都找不到，我就返回未知状态(一般来说，此情况不会发生，只有在缓存过期或者缓存被重启)
            return new OrderProgress()
            {
                Rank = -1,
                Token = token,
                Status = OrderProgressStatus.Unkonw,
                Description = "未知状态"
            };
        }
    }
}