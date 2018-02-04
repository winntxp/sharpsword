/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/24/2017 1:08:41 PM
 * ****************************************************************/

namespace SharpSword.O2O.Services.Domain
{
    /// <summary>
    /// 订单明细
    /// </summary>
    public class OrderCreateDetail
    {
        /// <summary>
        /// 预售活动ID
        /// </summary>
        public long PresaleActivityId { get; set; }

        /// <summary>
        /// 商品编号
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// 商品名称 确定需要上送？
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 商品SKU编码
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// 购买数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 属性值 确定需要上送？
        /// </summary>
        public string MutValues { get; set; }

        /// <summary>
        /// 商品主图 ? 确定需要上送？
        /// </summary>
        public string ProductMasterImage { get; set; }
    }
}
