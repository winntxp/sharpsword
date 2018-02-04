/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 08/23/2017 15:31:02
 * ****************************************************************/

namespace SharpSword.O2O.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductsPrice
    {

        ///<summary>
        /// 商品价格ID
        ///</summary>
        public int ProductPriceId { get; set; } // ProductPriceID (Primary key)

        ///<summary>
        /// 商品ID(主键)SKUNumberService.ID取得
        ///</summary>
        public int? ProductId { get; set; } // ProductId

        ///<summary>
        /// 区域ID
        ///</summary>
        public int? OperationAreaId { get; set; } // OperationAreaID

        ///<summary>
        /// 供应商ID
        ///</summary>
        public int? VendorId { get; set; } // VendorID

        ///<summary>
        /// 预售数量
        ///</summary>
        public decimal? PresaleQuantity { get; set; } // PresaleQuantity

        ///<summary>
        /// 预售价格
        ///</summary>
        public decimal? PresalePrice { get; set; } // PresalePrice

        ///<summary>
        /// 市场价
        ///</summary>
        public decimal? MarketPrice { get; set; } // MarketPrice

        ///<summary>
        /// 每份提成
        ///</summary>
        public decimal? Commission { get; set; } // Commission

        ///<summary>
        /// 包装数
        ///</summary>
        public decimal? PackingNumber { get; set; } // PackingNumber

        ///<summary>
        /// 创建时间
        ///</summary>
        public System.DateTime? CreateTime { get; set; } // CreateTime

        ///<summary>
        /// 创建用户ID
        ///</summary>
        public int? CreateUserId { get; set; } // CreateUserID

        ///<summary>
        /// 创建用户名称
        ///</summary>
        public string CreateUserName { get; set; } // CreateUserName (length: 32)

        ///<summary>
        /// 广告图
        ///</summary>
        public string AdvertisementImageSrc { get; set; } // AdvertisementImageSrc (length: 128)

        ///<summary>
        /// 最新修改删除时间
        ///</summary>
        public System.DateTime? ModifyTime { get; set; } // ModifyTime

        ///<summary>
        /// 最后修改删除用户ID
        ///</summary>
        public int? ModifyUserId { get; set; } // ModifyUserID

        ///<summary>
        /// 最后修改删除用户名称
        ///</summary>
        public string ModifyUserName { get; set; } // ModifyUserName (length: 32)

        ///<summary>
        /// 供货价
        ///</summary>
        public decimal? SupplyPrice { get; set; } // SupplyPrice

        ///<summary>
        /// 每份公司提成
        ///</summary>
        public decimal? CompanyCommission { get; set; } // CompanyCommission

        /// <summary>
        /// 
        /// </summary>
        public ProductsPrice()
        {
            OperationAreaId = 1;
            SupplyPrice = 0m;
            CompanyCommission = 0m;
        }
    }

}
