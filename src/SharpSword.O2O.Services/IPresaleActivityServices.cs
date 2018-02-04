/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 12:18:52 PM
 * ****************************************************************/
using SharpSword.O2O.Data.Entities;
using SharpSword.O2O.Services.Domain;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 预售相关商品，非常重要的一个接口，全部内存操作，因此需要考虑缓存失效，缓存如何进行重构
    /// </summary>
    public interface IPresaleActivityServices
    {
        /// <summary>
        /// 获取活动信息（不对信息过滤）
        /// </summary>
        /// <param name="presaleActivityId">活动ID</param>
        /// <returns></returns>
        PresaleActivity GetPresaleActivity(long presaleActivityId);

        /// <summary>
        /// 获取指定的活动商品信息，此接口是经过整理后下单系统需要的所有数据即可s
        /// </summary>
        /// <param name="presaleActivityId">活动ID</param>
        /// <param name="productId">活动商品ID</param>
        /// <returns></returns>
        PresaleActivityProductDto GetPresaleProduct(long presaleActivityId, int productId);

        /// <summary>
        /// 获取商品预售数量，如果不限制则返回0
        /// </summary>
        /// <param name="presaleActivityId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        long GetPresaleProductPresaleQty(long presaleActivityId, int productId);

        /// <summary>
        /// 获取商品用户限购数量（每人最多只能购买多少份），不限制则返回0
        /// </summary>
        /// <param name="presaleActivityId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        long GetPresaleProductUserLimitQty(long presaleActivityId, int productId);

        /// <summary>
        /// 获取活动商品销售数量（需实现先从缓存读取，不存在直接读取数据库并且存入到缓存）
        /// </summary>
        /// <param name="presaleActivityId">活动ID</param>
        /// <param name="productId">活动商品ID</param>
        /// <returns></returns>
        long GetPresaleProductSaleQuantity(long presaleActivityId, int productId);

        /// <summary>
        /// 增加商品销售量（需实现写入到缓存）
        /// </summary>
        /// <param name="presaleActivityId">活动ID</param>
        /// <param name="productId">活动商品ID</param>
        /// <param name="quantity">数量</param>
        void AddPresaleProductSaleQuantity(long presaleActivityId, int productId, decimal quantity);

        /// <summary>
        /// 减掉已经卖掉的销售量（需事先缓存写入）
        /// </summary>
        /// <param name="presaleActivityId">活动ID</param>
        /// <param name="productId">活动商品ID</param>
        /// <param name="quantity">数量</param>
        void SubPresaleProductSaleQuantity(long presaleActivityId, int productId, decimal quantity);

        /// <summary>
        /// 获取商品用户已经购买的数量（需实现先从缓存读取，不存在直接读取数据库并且存入到缓存）
        /// </summary>
        /// <param name="presaleActivityId"></param>
        /// <param name="productId">活动商品ID</param>
        /// <param name="userId"></param>
        /// <param name="shipTo">代客下单客户名称，如果为null，则代表不是代客下单订单</param>
        /// <returns></returns>
        long GetPresaleProductUserBuyQuantity(long presaleActivityId, int productId, long userId, string shipTo = null);

        /// <summary>
        /// 增加用户购买记录（需实现写入到缓存）
        /// </summary>
        /// <param name="presaleActivityId">活动ID</param>
        /// <param name="productId">活动商品ID</param>
        /// <param name="userId">购买用户</param>
        /// <param name="quantity">购买数量</param>
        /// <param name="shipTo">代客下单客户名称，如果为null，则代表不是代客下单订单</param>
        void AddPresaleProductUserBuyQuantity(long presaleActivityId, int productId, decimal quantity, long userId, string shipTo = null);

        /// <summary>
        /// 减掉已经购买的数量（需实现缓存操作）
        /// </summary>
        /// <param name="presaleActivityId">活动ID</param>
        /// <param name="productId">活动商品ID</param>
        /// <param name="userId">购买用户</param>
        /// <param name="quantity">数量</param>
        /// <param name="shipTo">代客下单客户名称，如果为null，则代表不是代客下单订单</param>
        void SubPresaleProductUserBuyQuantity(long presaleActivityId, int productId, decimal quantity, long userId, string shipTo = null);
    }
}
