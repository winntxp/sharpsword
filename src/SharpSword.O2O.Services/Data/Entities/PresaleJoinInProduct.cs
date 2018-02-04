/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 08/23/2017 15:31:02
 * ****************************************************************/
using SharpSword.Domain.Entitys;
using System;

namespace SharpSword.O2O.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class PresaleJoinInProduct : Entity
    {

        ///<summary>
        /// 主键ID
        ///</summary>
        public long JoinInProductId { get; set; }

        ///<summary>
        /// 商品ID
        ///</summary>
        public int ProductId { get; set; }

        ///<summary>
        /// 商品名称
        ///</summary>
        public string ProductName { get; set; }

        ///<summary>
        /// 预售活动编号
        ///</summary>
        public long PresaleActivityId { get; set; }

        ///<summary>
        /// 预售数量（为0时不限购）
        ///</summary>
        public decimal? PresaleQuantity { get; set; }

        ///<summary>
        /// 已售数量
        ///</summary>
        public decimal? SaleQuantity { get; set; }

        ///<summary>
        /// 市场价（上限价)
        ///</summary>
        public decimal MarketPrice { get; set; }

        ///<summary>
        /// 销售价
        ///</summary>
        public decimal PresalePrice { get; set; }

        ///<summary>
        /// 每份提成（0-1的数字）
        ///</summary>
        public decimal? Commission { get; set; }

        ///<summary>
        /// 是否直采（1、直采）
        ///</summary>
        public int? DirectMining { get; set; }

        ///<summary>
        /// 创建时间
        ///</summary>
        public DateTime CreateTime { get; set; }

        ///<summary>
        /// 创建用户ID
        ///</summary>
        public int? CreateUserId { get; set; }

        ///<summary>
        /// 创建用户名称
        ///</summary>
        public string CreateUserName { get; set; }

        ///<summary>
        /// 最新修改删除时间
        ///</summary>
        public DateTime ModifyTime { get; set; }

        ///<summary>
        /// 最后修改删除用户ID
        ///</summary>
        public int? ModifyUserId { get; set; }

        ///<summary>
        /// 最后修改删除用户名称
        ///</summary>
        public string ModifyUserName { get; set; }

        ///<summary>
        /// 供货价
        ///</summary>
        public decimal? SupplyPrice { get; set; }

        ///<summary>
        /// 每份公司提成
        ///</summary>
        public decimal? CompanyCommission { get; set; }

        ///<summary>
        /// 供应商编号
        ///</summary>
        public int? VendorId { get; set; }

        ///<summary>
        /// 排序(V2.2+)
        ///</summary>
        public int? ProductSort { get; set; }

        ///<summary>
        /// 包装数(V2.3+)
        ///</summary>
        public decimal? PackingNumber { get; set; }

        ///<summary>
        /// 用户限购数量(V2.3+)
        ///</summary>
        public decimal? UserLimitNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public PresaleJoinInProduct()
        {
            CreateTime = DateTime.Now;
            ModifyTime = DateTime.Now;
            SupplyPrice = 0m;
            CompanyCommission = 0m;
        }
    }

}
