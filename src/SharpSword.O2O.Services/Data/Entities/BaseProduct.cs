/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 08/23/2017 15:31:02
 * ****************************************************************/

namespace SharpSword.O2O.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseProduct
    {

        ///<summary>
        /// 商品母表ID（初始值和Product.productID一样)
        ///</summary>
        public int BaseProductId { get; set; } // BaseProductId (Primary key)

        ///<summary>
        /// 品牌ID
        ///</summary>
        public int? BrandId1 { get; set; } // BrandId1

        ///<summary>
        /// 品牌名称
        ///</summary>
        public string BrandName1 { get; set; } // BrandName1 (length: 32)

        ///<summary>
        /// 子品牌ID
        ///</summary>
        public int? BrandId2 { get; set; } // BrandId2

        ///<summary>
        /// 是否为多规格属性商品(0、不是；1、是)
        ///</summary>
        public int? IsMutiAttribute { get; set; } // IsMutiAttribute

        ///<summary>
        /// 是否为母商品(0、不是；1、是)
        ///</summary>
        public int IsBaseProductId { get; set; } // IsBaseProductID

        ///<summary>
        /// 母表商品名称（只有为母商品时才会有值)
        ///</summary>
        public string ProductBaseName { get; set; } // ProductBaseName (length: 100)

        ///<summary>
        /// 删除状态(0、未删除；1:已删除；2、子商品另外挂到其它商品)
        ///</summary>
        public int IsDeleted { get; set; } // IsDeleted

        ///<summary>
        /// 商品分享名称
        ///</summary>
        public string ShareName { get; set; } // ShareName (length: 64)

        ///<summary>
        /// 商品分享简介
        ///</summary>
        public string ShareDescription { get; set; } // ShareDescription (length: 256)

        ///<summary>
        /// 产地
        ///</summary>
        public string PlaceOfOrigin { get; set; } // PlaceOfOrigin (length: 64)

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

        // Reverse navigation

        /// <summary>
        /// Parent (One-to-One) BaseProduct pointed by [ProductsDescription].[BaseProductId] (FK_PRODUCTS_PRODUCTSDESCRIPTION_BASEPRODUCTID)
        /// </summary>
        public virtual ProductsDescription ProductsDescription { get; set; } // ProductsDescription.FK_PRODUCTS_PRODUCTSDESCRIPTION_BASEPRODUCTID
        /// <summary>
        /// Child Products where [Products].[BaseProductId] point to this entity (FK_PRODUCTS_REFERENCE_BASEPROD)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Product> Products { get; set; } // Products.FK_PRODUCTS_REFERENCE_BASEPROD
        /// <summary>
        /// Child ProductsDescriptionPictures where [ProductsDescriptionPicture].[BaseProductId] point to this entity (FK_PRODUCTS_PRODUCTSDESCRIPTIONPICTURE_BASEPRODUCTID)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ProductsDescriptionPicture> ProductsDescriptionPictures { get; set; } // ProductsDescriptionPicture.FK_PRODUCTS_PRODUCTSDESCRIPTIONPICTURE_BASEPRODUCTID

        /// <summary>
        /// 
        /// </summary>
        public BaseProduct()
        {
            IsMutiAttribute = 0;
            IsBaseProductId = 0;
            IsDeleted = 0;
            Products = new System.Collections.Generic.List<Product>();
            ProductsDescriptionPictures = new System.Collections.Generic.List<ProductsDescriptionPicture>();
        }
    }

}
