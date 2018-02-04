/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 08/23/2017 15:31:02
 * ****************************************************************/
using System;
using SharpSword.Domain.Entitys;

namespace SharpSword.O2O.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class PresaleProductSaleQuantity : Entity
    {
        ///<summary>
        /// 主键ID
        ///</summary>
        public long SaleQuantityId { get; set; }

        ///<summary>
        /// 活动编号
        ///</summary>
        public long? PresaleActivityId { get; set; }

        ///<summary>
        /// 商品编号
        ///</summary>
        public int? ProductId { get; set; }

        ///<summary>
        /// 销量
        ///</summary>
        public decimal? SaleQuantity { get; set; }

        ///<summary>
        /// 用户ID
        ///</summary>
        public long? UserId { get; set; }

        ///<summary>
        /// 创建时间
        ///</summary>
        public DateTime? CreateTime { get; set; }

        ///<summary>
        /// 修改时间
        ///</summary>
        public DateTime? ModifyTime { get; set; }

        ///<summary>
        /// 代客下单（1、是，0、否）
        ///</summary>
        public int? IsValetOrder { get; set; }

        ///<summary>
        /// 收货 人
        ///</summary>
        public string ShipTo { get; set; }
    }

}
