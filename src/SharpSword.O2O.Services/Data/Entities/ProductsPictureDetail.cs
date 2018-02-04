/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 08/23/2017 15:31:02
 * ****************************************************************/

namespace SharpSword.O2O.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductsPictureDetail
    {

        ///<summary>
        /// 主键ID
        ///</summary>
        public int Id { get; set; } // ID (Primary key)

        ///<summary>
        /// 商品ID(主键)SKUNumberService.ID取得
        ///</summary>
        public int? ImageProductId { get; set; } // ImageProductId

        ///<summary>
        /// 原图800*800路径(商品主图放大,商品详情图点击)
        ///</summary>
        public string ImageUrlOrg { get; set; } // ImageUrlOrg (length: 200)

        ///<summary>
        /// zip为400*400的图路径(商品详情图)
        ///</summary>
        public string ImageUrl400X400 { get; set; } // ImageUrl400x400 (length: 200)

        ///<summary>
        /// zip为200*200的图路径(商品列表图)
        ///</summary>
        public string ImageUrl200X200 { get; set; } // ImageUrl200x200 (length: 200)

        ///<summary>
        /// zip为120*120的图路径
        ///</summary>
        public string ImageUrl120X120 { get; set; } // ImageUrl120x120 (length: 200)

        ///<summary>
        /// zip为60*60的图路径(订单提交页列表小图)
        ///</summary>
        public string ImageUrl60X60 { get; set; } // ImageUrl60x60 (length: 200)

        ///<summary>
        /// 排序
        ///</summary>
        public int? OrderNumber { get; set; } // OrderNumber

        ///<summary>
        /// 是否为主图(只有一张;0:不是;1:是)
        ///</summary>
        public int IsMaster { get; set; } // IsMaster

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
