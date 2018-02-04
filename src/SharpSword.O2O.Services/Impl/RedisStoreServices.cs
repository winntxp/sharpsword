/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 11:33:46 AM
 * ****************************************************************/
using Dapper;
using SharpSword.Caching.Redis.StackExchange;
using SharpSword.Data;
using SharpSword.O2O.Services.Domain;
using System;
using System.Linq;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 基于REDIS的门店信息服务
    /// </summary>
    public class RedisStoreServices : IStoreServices
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly Lazy<ICacheManager> _cacheManager;
        private readonly IGlobalDbConnectionFactory _globalDbConnectionFactory;
        private readonly GlobalConfig _globalConfig;
        private readonly IRedisConnectionWrapper _redisConnectionWrapper;

        /// <summary>
        /// 
        /// </summary>
        private const string SHOPKEY = "O2O.Store:{0}";

        /// <summary>
        /// 日志记录器
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 系统报警器
        /// </summary>
        public ISystemWarningTrigger WarningTrigger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="redisConnectionWrapper"></param>
        /// <param name="globalDbConnectionFactory"></param>
        /// <param name="globalConfig"></param>
        public RedisStoreServices(IRedisConnectionWrapper redisConnectionWrapper,
                                  IGlobalDbConnectionFactory globalDbConnectionFactory,
                                  GlobalConfig globalConfig)
        {
            this._globalDbConnectionFactory = globalDbConnectionFactory;
            this._globalConfig = globalConfig;
            this._redisConnectionWrapper = redisConnectionWrapper;
            this._cacheManager = new Lazy<ICacheManager>(() => new RedisCacheManager(redisConnectionWrapper));
            this.Logger = GenericNullLogger<RedisStoreServices>.Instance;
            this.WarningTrigger = NullSystemWarningTrigger.Instance;
        }

        /// <summary>
        /// 我们先从缓存取到门店信息，如果缓存不存在，我们直接从数据库取
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public virtual StoreProfileDto GetStore(long storeId)
        {
            try
            {
                return this._cacheManager.Value.Get(SHOPKEY.With(storeId), this._globalConfig.StoreCacheTime, () =>
                 {
                     string sql = @"SELECT  sp.SupplierId as StoreId,
                                            sp.SupplierNo as StoreNo, 
                                            sysa.AreaFullName, 
                                            sp.DetailAddress, 
                                            sp.StoreName , 
                                            sp.SupplierState as Status,
                                            sp.IsDeleted,
                                            sp.LineSort,
                                            dl.LineID, 
                                            dl.LineName,
                                            oa.OperationAreaID as AreaID, 
                                            oa.OperationAreaName,
                                            dc.DistributionClerkID, 
                                            dc.DistributionClerkName
                                FROM StoreProfile sp
                                INNER JOIN DistributionLine dl ON sp.LineID = dl.LineID
                                INNER JOIN OperationArea oa ON oa.OperationAreaID=dl.OperationAreaID
                                INNER JOIN DistributionClerk dc ON dc.OperationAreaID=dl.OperationAreaID
                                LEFT JOIN SysArea sysa ON sysa.AreaID=sp.RegionId
                                WHERE sp.IsDeleted=0 AND sp.SupplierID=@StoreId";

                     return this._globalDbConnectionFactory.Create()
                                                           .Query<StoreProfileDto>(sql, new { StoreId = storeId }).FirstOrDefault();
                 });
            }
            catch (Exception ex)
            {
                //记录下日志
                this.Logger.Error(ex);

                //报警
                this.WarningTrigger.Warning(this, ex.Message, ex);

                //返回空
                return null;
            }
        }
    }
}
