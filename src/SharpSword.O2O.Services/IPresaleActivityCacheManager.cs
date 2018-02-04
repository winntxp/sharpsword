/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/5/2017 12:42:43 PM
 * ****************************************************************/

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 获取缓存管理器
    /// </summary>
    public interface IPresaleActivityCacheManager
    {
        /// <summary>
        /// 清理获取商品缓存
        /// </summary>
        /// <param name="presaleActivityId">活动编号</param>
        /// <param name="productId">商品编号</param>
        void RemovePresaleProduct(long presaleActivityId, int productId);

        /// <summary>
        /// 清理活动商品销售缓存
        /// </summary>
        /// <param name="presaleActivityId">活动编号</param>
        /// <param name="productId">商品编号</param>
        void RemovePresaleProductSaleQuantity(long presaleActivityId, int productId);

        /// <summary>
        /// 清理活动商品用户限购信息
        /// </summary>
        /// <param name="presaleActivityId">活动编号</param>
        /// <param name="productId">商品编号</param>
        void RemovePresaleProductUserBuy(long presaleActivityId, int productId);

    }
}
