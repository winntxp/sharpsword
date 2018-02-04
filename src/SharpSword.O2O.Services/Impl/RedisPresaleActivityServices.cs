/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 12:17:39 PM
 * ****************************************************************/
using Dapper;
using SharpSword.Caching.Redis.StackExchange;
using SharpSword.Data;
using SharpSword.O2O.Data.Entities;
using SharpSword.O2O.Services.Domain;
using System;
using System.Linq;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class RedisPresaleActivityServices : IPresaleActivityServices, IPresaleActivityCacheManager
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly Lazy<ICacheManager> _cacheManager;
        private readonly IRedisConnectionWrapper _redisConnectionWrapper;
        private readonly IGlobalDbConnectionFactory _globalDbConnectionFactory;
        private readonly GlobalConfig _globalConfig;

        /// <summary>
        /// 活动信息
        /// </summary>
        private const string PRESALEKEY = "O2O.Presale:{0}";

        /// <summary>
        /// 活动商品
        /// </summary>
        private const string PRESALEPRODUCTKEY = "O2O.Presale.Product:{0}.{1}";

        /// <summary>
        /// 活动商品销售量
        /// </summary>
        private const string PRESALEPRODUCTSALEQTYKEY = "O2O.Presale.Product.SaleQty:{0}.{1}";

        /// <summary>
        /// 用户限购数量集合表(销量)
        /// </summary>
        private const string PRESALEPRODUCTUSERBUYKEY = "O2O.Presale.Product.UserBuy:{0}.{1}";

        /// <summary>
        /// 商品总库存限购
        /// </summary>
        private const string PRESALEPRODUCTPRESALEQTYKEY = "O2O.Presale.Product.PresaleQty:{0}.{1}";

        /// <summary>
        /// 商品用户限购
        /// </summary>
        private const string PRESALEPRODUCTUSERLIMITQTYKEY = "O2O.Presale.Product.UserLimitQty:{0}.{1}";

        /// <summary>
        /// 日志记录器
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 异常报警触发器
        /// </summary>
        public ISystemWarningTrigger WarningTrigger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="redisConnectionWrapper"></param>
        /// <param name="globalDbConnectionFactory"></param>
        /// <param name="globalConfig"></param>
        public RedisPresaleActivityServices(IRedisConnectionWrapper redisConnectionWrapper,
                                            IGlobalDbConnectionFactory globalDbConnectionFactory,
                                            GlobalConfig globalConfig)
        {
            this._redisConnectionWrapper = redisConnectionWrapper;
            this._globalDbConnectionFactory = globalDbConnectionFactory;
            this._globalConfig = globalConfig;
            this._cacheManager = new Lazy<ICacheManager>(() => new RedisCacheManager(redisConnectionWrapper));
            this.Logger = GenericNullLogger<RedisPresaleActivityServices>.Instance;
            this.WarningTrigger = NullSystemWarningTrigger.Instance;
        }

        /// <summary>
        /// 获取活动信息
        /// </summary>
        /// <param name="presaleActivityId"></param>
        /// <returns></returns>
        public virtual PresaleActivity GetPresaleActivity(long presaleActivityId)
        {
            return this._cacheManager.Value.Get(PRESALEKEY.With(presaleActivityId), this._globalConfig.PresaleProductCacheTime, () =>
            {
                string sql = @"SELECT * FROM  PresaleActivity WHERE PresaleActivityID=@PresaleActivityId";
                return this._globalDbConnectionFactory.Create().Query<PresaleActivity>(sql, new
                {
                    PresaleActivityId = presaleActivityId
                }).FirstOrDefault();
            });
        }

        /// <summary>
        /// 获取活动商品信息
        /// TODO:是否需要加入在活动开始前30分钟自动将活动商品信息加入到缓存（提前准备）？还是现在怎样进行惰性加载？
        /// </summary>
        /// <param name="presaleActivityId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public virtual PresaleActivityProductDto GetPresaleProduct(long presaleActivityId, int productId)
        {
            return this._cacheManager.Value.Get(PRESALEPRODUCTKEY.With(presaleActivityId, productId), this._globalConfig.PresaleProductCacheTime, () => {
                 string sql = @"SELECT pa.PresaleActivityID, 
                                       pa.DeliveryTime, 
                                       pa.ExpiryDateStart,
                                       pa.ExpiryDateEnd,
                                       pa.IsDeleted,
                                       pa.IsAudit,
                                       pjip.ProductID, 
                                       pjip.ProductName, 
                                       pjip.VendorID, 
                                       pjip.DirectMining,
                                       pjip.PackingNumber, 
                                       pjip.SaleQuantity, 
                                       pjip.PresaleQuantity, 
                                       pjip.UserLimitNumber ,
                                       pjip.MarketPrice, 
                                       pjip.PresalePrice, 
                                       pjip.SupplyPrice, 
                                       pjip.Commission, 
                                       pjip.CompanyCommission,
                                       p.SKU, 
                                       p.MutValues,
                                       v.VendorName, 
                                       v.[Address] as VendorAddress,
                                       v.Telephone as VendorTelephone
                                FROM PresaleActivity pa 
                                INNER JOIN PresaleJoinInProduct pjip ON pa.PresaleActivityID=pjip.PresaleActivityID
                                INNER JOIN Products p ON p.ProductId=pjip.ProductID
                                LEFT JOIN Vendor v ON v.VendorID = pjip.VendorID
                                WHERE pa.PresaleActivityID=@PresaleActivityId AND pjip.ProductID=@ProductId";

                 return this._globalDbConnectionFactory.Create().Query<PresaleActivityProductDto>(sql, new
                 {
                     PresaleActivityId = presaleActivityId,
                     ProductId = productId
                 }).FirstOrDefault();
             });
        }

        /// <summary>
        /// 获取活动商品总库存限制，如果不限购返回0
        /// </summary>
        /// <param name="presaleActivityId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public long GetPresaleProductPresaleQty(long presaleActivityId, int productId)
        {
            return this._cacheManager.Value.Get(PRESALEPRODUCTPRESALEQTYKEY.With(presaleActivityId, productId), this._globalConfig.PresaleProductCacheTime, () =>
            {
                string sql = @"SELECT PresaleQuantity ISNULL(MAX(PresaleJoinInProduct),0) WHERE PresaleActivityID=@PresaleActivityId AND ProductID=@ProductId";
                return this._globalDbConnectionFactory.Create().Query<long>(sql, new
                {
                    PresaleActivityId = presaleActivityId,
                    ProductId = productId
                }).FirstOrDefault();
            });
        }

        /// <summary>
        /// 获取商品用户购买现在，如果不限购返回0
        /// </summary>
        /// <param name="presaleActivityId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public long GetPresaleProductUserLimitQty(long presaleActivityId, int productId)
        {
            return this._cacheManager.Value.Get(PRESALEPRODUCTUSERLIMITQTYKEY.With(presaleActivityId, productId), this._globalConfig.PresaleProductCacheTime, () =>
            {
                string sql = @"SELECT PresaleQuantity, ISNULL(MAX(UserLimitNumber),0) WHERE PresaleActivityID=@PresaleActivityId AND ProductID=@ProductId";
                return this._globalDbConnectionFactory.Create().Query<long>(sql, new
                {
                    PresaleActivityId = presaleActivityId,
                    ProductId = productId
                }).FirstOrDefault();
            });
        }

        /// <summary>
        /// 获取商品的销售量（缓存里取，惰性加载）
        /// </summary>
        /// <param name="presaleActivityId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public virtual long GetPresaleProductSaleQuantity(long presaleActivityId, int productId)
        {
            string key = PRESALEPRODUCTSALEQTYKEY.With(presaleActivityId, productId);

            if (this._cacheManager.Value.IsSet(key))
            {
                return this._cacheManager.Value.Get<long>(key);
            }
            else
            {
                string sql = @"SELECT ISNULL(SUM(SaleQuantity),0) AS SaleQuantity FROM  PresaleJoinInProduct 
                                                                                        WHERE PresaleActivityID=@PresaleActivityID 
                                                                                        AND   ProductID=@ProductID";

                var quantity = this._globalDbConnectionFactory.Create().Query<long>(sql, new
                {
                    PresaleActivityID = presaleActivityId,
                    ProductID = productId
                }).FirstOrDefault();

                //缓存重建
                this.AddPresaleProductSaleQuantity(presaleActivityId, productId, quantity);

                return quantity;
            }
        }

        /// <summary>
        /// 增加销售量
        /// </summary>
        /// <param name="presaleActivityId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        public virtual void AddPresaleProductSaleQuantity(long presaleActivityId, int productId, decimal quantity)
        {
            try
            {
                //TODO:如果缓存挂掉？
                var db = this._redisConnectionWrapper.Database();
                db.StringIncrement(PRESALEPRODUCTSALEQTYKEY.With(presaleActivityId, productId),
                                                                        (long)quantity);
            }
            catch (Exception ex)
            {
                //记录日志
                this.Logger.Error(ex);

                //报警
                this.WarningTrigger.Warning(this, ex.Message, ex);
            }
        }

        /// <summary>
        /// 减掉销量
        /// </summary>
        /// <param name="presaleActivityId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        public void SubPresaleProductSaleQuantity(long presaleActivityId, int productId, decimal quantity)
        {
            try
            {
                var db = this._redisConnectionWrapper.Database();
                db.StringDecrement(PRESALEPRODUCTSALEQTYKEY.With(presaleActivityId, productId),
                                                                      (long)quantity);
            }
            catch (Exception ex)
            {
                //记录日志
                this.Logger.Error(ex);

                //报警
                this.WarningTrigger.Warning(this,ex.Message);
            }
        }

        /// <summary>
        /// 获取用户限购缓存KEY
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="shipTo"></param>
        /// <returns></returns>
        private string UserBuyedCacheKey(long userId, string shipTo)
        {
            if (!shipTo.IsNullOrEmpty())
            {
                return "{0}.{1}".With(userId, MD5.Encrypt(shipTo));
            }
            else
            {
                return "{0}".With(userId);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="presaleActivityId"></param>
        /// <param name="productId"></param>
        /// <param name="userId"></param>
        /// <param name="shipTo"></param>
        /// <returns></returns>
        public virtual long GetPresaleProductUserBuyQuantity(long presaleActivityId, int productId, long userId, string shipTo = null)
        {
            string key = PRESALEPRODUCTUSERBUYKEY.With(presaleActivityId, productId);
            string userkey = this.UserBuyedCacheKey(userId, shipTo);
            if (this._redisConnectionWrapper.Database().HashExists(key, userkey))
            {
                return (long)_redisConnectionWrapper.Database().HashGet(key, userkey);
            }
            else
            {
                string sql = @"SELECT ISNULL(Sum(SaleQuantity),0) AS SaleQuantity FROM  PresaleProductSaleQuantity 
                                                                                   WHERE PresaleActivityID=@PresaleActivityId 
                                                                                   AND ProductID=@ProductId 
                                                                                   AND UserID=@UserId";
                if (!shipTo.IsNullOrEmpty())
                {
                    sql = sql + " AND ShipTo=@ShipTo ";
                }

                var quantity = this._globalDbConnectionFactory.Create().Query<long>(sql, new
                {
                    PresaleActivityId = presaleActivityId,
                    ProductId = productId,
                    UserId = userId,
                    ShipTo = shipTo
                }).FirstOrDefault();

                //缓存重建
                this.AddPresaleProductUserBuyQuantity(presaleActivityId, productId, userId, quantity, shipTo);

                return quantity;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="presaleActivityId"></param>
        /// <param name="productId"></param>
        /// <param name="userId"></param>
        /// <param name="quantity"></param>
        /// <param name="shipTo"></param>
        public virtual void AddPresaleProductUserBuyQuantity(long presaleActivityId, int productId, decimal quantity, long userId, string shipTo = null)
        {
            try
            {
                //TODO:如果缓存服务器挂掉？
                var db = this._redisConnectionWrapper.Database();
                db.HashIncrement(PRESALEPRODUCTUSERBUYKEY.With(presaleActivityId, productId),
                                                                   this.UserBuyedCacheKey(userId, shipTo),
                                                                   (double)quantity);
            }
            catch (Exception ex)
            {
                //记录日志
                this.Logger.Error(ex);

                //报警
                this.WarningTrigger.Warning(this, ex.Message, ex);
            }
        }

        /// <summary>
        /// 减掉用户购买数量
        /// </summary>
        /// <param name="presaleActivityId"></param>
        /// <param name="productId"></param>
        /// <param name="userId"></param>
        /// <param name="quantity"></param>
        /// <param name="shipTo"></param>
        public void SubPresaleProductUserBuyQuantity(long presaleActivityId, int productId, decimal quantity, long userId, string shipTo = null)
        {
            try
            {
                var db = this._redisConnectionWrapper.Database();
                db.HashDecrement(PRESALEPRODUCTUSERBUYKEY.With(presaleActivityId, productId),
                                                                  this.UserBuyedCacheKey(userId, shipTo),
                                                                  (double)quantity);
            }
            catch (Exception ex)
            {
                //记录日志
                this.Logger.Error(ex);

                //报警
                this.WarningTrigger.Warning(this, ex.Message, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="presaleActivityId"></param>
        /// <param name="productId"></param>
        public void RemovePresaleProduct(long presaleActivityId, int productId)
        {
            this._redisConnectionWrapper.Database().KeyDelete(PRESALEPRODUCTKEY.With(presaleActivityId, productId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="presaleActivityId"></param>
        /// <param name="productId"></param>
        public void RemovePresaleProductSaleQuantity(long presaleActivityId, int productId)
        {
            this._redisConnectionWrapper.Database().KeyDelete(PRESALEPRODUCTSALEQTYKEY.With(presaleActivityId, productId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="presaleActivityId"></param>
        /// <param name="productId"></param>
        public void RemovePresaleProductUserBuy(long presaleActivityId, int productId)
        {
            this._redisConnectionWrapper.Database().KeyDelete(PRESALEPRODUCTUSERBUYKEY.With(presaleActivityId, productId));
        }
    }
}
