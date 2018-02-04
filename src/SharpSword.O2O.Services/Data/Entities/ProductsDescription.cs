/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 08/23/2017 15:31:02
 * ****************************************************************/
using System;

namespace SharpSword.O2O.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductsDescription
    {

        ///<summary>
        /// 商品母表ID（初始值和BaseProduct.BaseProductID一样)
        ///</summary>
        public int BaseProductId { get; set; } // BaseProductId (Primary key)

        ///<summary>
        /// 商品描述
        ///</summary>
        public string Description { get; set; } // Description (length: 1073741823)

        ///<summary>
        /// 最新修改删除时间
        ///</summary>
        public DateTime? ModifyTime { get; set; } // ModifyTime

        ///<summary>
        /// 最后修改删除用户ID
        ///</summary>
        public int? ModifyUserId { get; set; } // ModifyUserID

        ///<summary>
        /// 最后修改删除用户名称
        ///</summary>
        public string ModifyUserName { get; set; } // ModifyUserName (length: 32)

    }
}
