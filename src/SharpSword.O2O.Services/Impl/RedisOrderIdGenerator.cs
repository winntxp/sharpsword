/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 5:11:14 PM
 * ****************************************************************/
using SharpSword.Caching.Redis.StackExchange;
using SharpSword.O2O.Data.Entities;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 默认使用REDIS订单ID管理器
    /// </summary>
    public class RedisOrderIdGenerator : OrderIdGeneratorBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IRedisConnectionWrapper _redisConnectionWrapper;

        /// <summary>
        /// 
        /// </summary>
        private const string ORDERIDKEY = "O2O.Order.SequenceId";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="redisConnectionWrapper"></param>
        public RedisOrderIdGenerator(IRedisConnectionWrapper redisConnectionWrapper)
        {
            this._redisConnectionWrapper = redisConnectionWrapper;
        }

        /// <summary>
        /// 获取流水码，我们定义成虚方法，方便我们重写此方法，比如后续需要使用数据库等
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        protected override string GetSequenceId(Order order)
        {
            //生成流水码
            string sequenceId = this._redisConnectionWrapper.Database()
                                                            .StringIncrement(ORDERIDKEY)
                                                            .ToString("0000000");

            return sequenceId;
        }
    }
}
