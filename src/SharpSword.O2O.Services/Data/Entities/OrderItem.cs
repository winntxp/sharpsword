/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 08/23/2017 15:31:02
 * ****************************************************************/
using SharpSword.Domain.Entitys;

namespace SharpSword.O2O.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderItem : Entity
    {
        ///<summary>
        /// 编号
        ///</summary>
        public int Id { get; set; }

        ///<summary>
        /// 订单编号
        ///</summary>
        public string OrderId { get; set; }

        ///<summary>
        /// 商品编号
        ///</summary>
        public int ProductId { get; set; }

        ///<summary>
        /// 商品SKU
        ///</summary>
        public string Sku { get; set; }

        ///<summary>
        /// 商品价格ID(商品价格表中的主键ID；ProductsPrice.ProductsPriceID)
        ///</summary>
        public int ProductPriceId { get; set; }

        ///<summary>
        /// 预售数量
        ///</summary>
        public decimal? PresaleQuantity { get; set; }

        ///<summary>
        /// 购买数量
        ///</summary>
        public decimal Quantity { get; set; }

        ///<summary>
        /// 发货数量
        ///</summary>
        public decimal ShipmentQuantity { get; set; }

        ///<summary>
        /// 原价
        ///</summary>
        public decimal ItemListPrice { get; set; }

        ///<summary>
        /// 调整后价格(支付价格)
        ///</summary>
        public decimal ItemAdjustedPrice { get; set; }

        ///<summary>
        /// 每份提成
        ///</summary>
        public decimal? Commission { get; set; }

        ///<summary>
        /// 描述
        ///</summary>
        public string ItemDescription { get; set; }

        ///<summary>
        /// 商品图片
        ///</summary>
        public string ThumbnailsUrl { get; set; }

        ///<summary>
        /// 重量
        ///</summary>
        public decimal? Weight { get; set; }

        ///<summary>
        /// 商品规格内容
        ///</summary>
        public string SkuContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? PackingNumber { get; set; }

        ///<summary>
        /// 区域ID
        ///</summary>
        public int? OperationAreaId { get; set; }

        ///<summary>
        /// 是否缺货(1、缺货；0/null、不缺货)
        ///</summary>
        public int? IsStockOut { get; set; }

        ///<summary>
        /// 是否已提货（1、已提货，0、待提货）
        ///</summary>
        public int? IsGetProduct { get; set; }

        ///<summary>
        /// 第三方供应商ID
        ///</summary>
        public int? VendorId { get; set; }

        ///<summary>
        /// 供应商名称
        ///</summary>
        public string VendorName { get; set; }

        ///<summary>
        /// 供应商地址
        ///</summary>
        public string VendorAddress { get; set; }

        ///<summary>
        /// 供应商联络电话
        ///</summary>
        public string VendorTelephone { get; set; }

        ///<summary>
        /// 预售活动ID号
        ///</summary>
        public long? PresaleActivityId { get; set; }

        ///<summary>
        /// 是否直采（1、直采）
        ///</summary>
        public int? DirectMining { get; set; }

        ///<summary>
        /// 供货价
        ///</summary>
        public decimal? SupplyPrice { get; set; }

        ///<summary>
        /// 每份公司提成
        ///</summary>
        public decimal? CompanyCommission { get; set; }

        ///<summary>
        /// 提货时间
        ///</summary>
        public System.DateTime? DeliveryTime { get; set; }

        ///<summary>
        /// 商品名称
        ///</summary>
        public string ProductName { get; set; }

        ///<summary>
        /// 线路ID
        ///</summary>
        public int? LineId { get; set; }

        ///<summary>
        /// 线路名称
        ///</summary>
        public string LineName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OrderItem()
        {
            ProductPriceId = 0;
        }
    }

}
