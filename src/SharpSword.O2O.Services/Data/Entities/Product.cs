/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 08/23/2017 15:31:02
 * ****************************************************************/

namespace SharpSword.O2O.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Product
    {
        ///<summary>
        /// 商品ID(主键)SKUNumberService.ID取得
        ///</summary>
        public int ProductId { get; set; } // ProductId (Primary key)

        ///<summary>
        /// 商品编码(0000001,0000002),最长7个长度;唯一（SKUNumberService.ID取得补0)
        ///</summary>
        public string Sku { get; set; } // SKU (length: 10)

        ///<summary>
        /// 商品名称
        ///</summary>
        public string ProductName { get; set; } // ProductName (length: 100)

        ///<summary>
        /// 商品副标题
        ///</summary>
        public string ProductName2 { get; set; } // ProductName2 (length: 400)

        ///<summary>
        /// 母商品ID(BaseProduct.BaseProductId)
        ///</summary>
        public int BaseProductId { get; set; } // BaseProductId

        ///<summary>
        /// 对应产品的图片,对应商品的Product.ProductId
        ///</summary>
        public int? ImageProductId { get; set; } // ImageProductId

        ///<summary>
        /// Erp编码
        ///</summary>
        public string ErpCode { get; set; } // ErpCode (length: 20)

        ///<summary>
        /// 搜索关键字
        ///</summary>
        public string Keywords { get; set; } // Keywords (length: 200)

        ///<summary>
        /// 删除状态(0:未删除;1:已删除)
        ///</summary>
        public int IsDeleted { get; set; } // IsDeleted

        ///<summary>
        /// 天下图库的商品ID(从天下图库创建时，带有该ID）
        ///</summary>
        public string Txtkid { get; set; } // TXTKID (length: 36)

        ///<summary>
        /// 天下图库Code
        ///</summary>
        public string TxtkCode { get; set; } // TXTKCode (length: 12)

        ///<summary>
        /// 多规格属性id:,多个时按分号分开
        ///</summary>
        public string MutAttributes { get; set; } // MutAttributes (length: 200)

        ///<summary>
        /// 多规格属性值id,多个时按分号分开
        ///</summary>
        public string MutValues { get; set; } // MutValues (length: 200)

        ///<summary>
        /// 销量
        ///</summary>
        public int? SaleCounts { get; set; } // SaleCounts

        ///<summary>
        /// 上下架状态（1、上架，0、下架）
        ///</summary>
        public int? Upselling { get; set; } // Upselling

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

        /// <summary>
        /// 
        /// </summary>
        public Product()
        {
            IsDeleted = 0;
            Upselling = 1;
        }
    }

}
