/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 1:04:35 PM
 * ****************************************************************/
using System;

namespace SharpSword.O2O.Services.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class PresaleActivityProductDto
    {
        /// <summary>
        /// 活动ID
        /// </summary>
        public long PresaleActivityID { get; set; }

        /// <summary>
        /// 提货日期
        /// </summary>
        public DateTime DeliveryTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ExpiryDateStart { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ExpiryDateEnd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int IsAudit { get; set; }

        /// <summary>
        /// 商品编号
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 是否直采（1、是）
        /// </summary>
        public int DirectMining { get; set; }

        /// <summary>
        /// 商品SKU
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// 商品规格
        /// </summary>
        public string MutValues { get; set; }

        /// <summary>
        /// 包装数
        /// </summary>
        public decimal PackingNumber { get; set; }

        /// <summary>
        /// 已售数量
        /// </summary>
        public decimal SaleQuantity { get; set; }

        /// <summary>
        /// 预售数量
        /// </summary>
        public decimal? PresaleQuantity { get; set; }

        /// <summary>
        /// 用户限量
        /// </summary>
        public decimal? UserLimitNumber { get; set; }

        /// <summary>
        /// 市场价
        /// </summary>
        public decimal MarketPrice { get; set; }

        /// <summary>
        /// 预售价
        /// </summary>
        public decimal PresalePrice { get; set; }

        /// <summary>
        /// 供货价
        /// </summary>
        public decimal SupplyPrice { get; set; }

        /// <summary>
        /// 门店提成
        /// </summary>
        public decimal Commission { get; set; }

        /// <summary>
        /// 平台服务费
        /// </summary>
        public decimal CompanyCommission { get; set; }

        /// <summary>
        /// 供应商编号
        /// </summary>
        public int VendorID { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string VendorName { get; set; }

        /// <summary>
        /// 供应商地址
        /// </summary>
        public string VendorAddress { get; set; }

        /// <summary>
        /// 供应商联系方式
        /// </summary>
        public string VendorTelephone { get; set; }

    }
}
