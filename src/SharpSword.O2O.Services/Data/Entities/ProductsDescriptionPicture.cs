/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 08/23/2017 15:31:02
 * ****************************************************************/

namespace SharpSword.O2O.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductsDescriptionPicture
    {

        ///<summary>
        /// 主键ID
        ///</summary>
        public int Id { get; set; } // ID (Primary key)

        ///<summary>
        /// 商品母表ID
        ///</summary>
        public int? BaseProductId { get; set; } // BaseProductId

        ///<summary>
        /// 原图路径
        ///</summary>
        public string ImageUrlOrg { get; set; } // ImageUrlOrg (Primary key) (length: 200)

        ///<summary>
        /// zip为400*400的图路径
        ///</summary>
        public string ImageUrl400X400 { get; set; } // ImageUrl400x400 (Primary key) (length: 200)

        ///<summary>
        /// zip为200*200的图路径
        ///</summary>
        public string ImageUrl200X200 { get; set; } // ImageUrl200x200 (Primary key) (length: 200)

        ///<summary>
        /// zip为120*120的图路径
        ///</summary>
        public string ImageUrl120X120 { get; set; } // ImageUrl120x120 (Primary key) (length: 200)

        ///<summary>
        /// zip为60*60的图路径
        ///</summary>
        public string ImageUrl60X60 { get; set; } // ImageUrl60x60 (Primary key) (length: 200)

        ///<summary>
        /// 排序(1,2,3..)
        ///</summary>
        public int? OrderNumber { get; set; } // OrderNumber

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

    }

}
