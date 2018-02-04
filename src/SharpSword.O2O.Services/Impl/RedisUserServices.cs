/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 12:18:52 PM
 * ****************************************************************/
using Dapper;
using SharpSword.Caching.Redis.StackExchange;
using SharpSword.Data;
using SharpSword.O2O.Data.Entities;
using System;
using System.Linq;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class RedisUserServices : IUserServices
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly Lazy<ICacheManager> _cacheManager;
        private readonly IUserDbConnectionFactory _userDbConnectionFactory;
        private readonly IUserDbTableFinder _userDbTableFinder;
        private readonly IRedisConnectionWrapper _redisConnectionWrapper;

        /// <summary>
        /// 
        /// </summary>
        private const string USERKEY = "O2O.User:{0}";

        /// <summary>
        /// 日志记录器
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 异常报警器
        /// </summary>
        public ISystemWarningTrigger WarningTrigger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="redisConnectionWrapper"></param>
        /// <param name="userDbConnectionFactory"></param>
        /// <param name="userDbTableFinder"></param>
        public RedisUserServices(IRedisConnectionWrapper redisConnectionWrapper,
                                 IUserDbConnectionFactory userDbConnectionFactory,
                                 IUserDbTableFinder userDbTableFinder)
        {
            this._userDbConnectionFactory = userDbConnectionFactory;
            this._userDbTableFinder = userDbTableFinder;
            this._redisConnectionWrapper = redisConnectionWrapper;
            this._cacheManager = new Lazy<ICacheManager>(() => new RedisCacheManager(redisConnectionWrapper));
            this.Logger = GenericNullLogger<RedisUserServices>.Instance;
            this.WarningTrigger = NullSystemWarningTrigger.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual AspnetUser GetUser(long userId)
        {
            return this._cacheManager.Value.Get(USERKEY.With(userId), 30, () =>
            {
                string sql = @"SELECT * FROM  AspnetUsers WHERE UserId=@UserId";
                return this._userDbConnectionFactory.Create(userId).Query<AspnetUser>(sql, new
                {
                    UserId = userId
                }).FirstOrDefault();
            });
        }
    }
}
