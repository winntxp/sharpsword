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
    public class PresaleActivity : Entity
    {
        ///<summary>
        /// 预售活动编号
        ///</summary>
        public long PresaleActivityId { get; set; }

        ///<summary>
        /// 区域ID(OperationAreaID)
        ///</summary>
        public int? OperationAreaId { get; set; }

        ///<summary>
        /// 预售活动名称
        ///</summary>
        public string PresaleActivityName { get; set; }

        ///<summary>
        /// 活动图片
        ///</summary>
        public string ImgUrl { get; set; }

        ///<summary>
        /// 有效起始日期
        ///</summary>
        public DateTime ExpiryDateStart { get; set; }

        ///<summary>
        /// 有效截止日期
        ///</summary>
        public DateTime ExpiryDateEnd { get; set; }

        ///<summary>
        /// 预售发货时间
        ///</summary>
        public DateTime DeliveryTime { get; set; }

        ///<summary>
        /// 支付规则（1、全额支付，2、部分支付）
        ///</summary>
        public int PayRules { get; set; }

        ///<summary>
        /// 预售支付起始时间
        ///</summary>
        public DateTime? PresaleDateStart { get; set; }

        ///<summary>
        /// 预售支付截止时间
        ///</summary>
        public DateTime? PresaleDateEnd { get; set; }

        ///<summary>
        /// 支付尾款起始时间
        ///</summary>
        public DateTime? PayTailStart { get; set; }

        ///<summary>
        /// 支付尾款截止时间
        ///</summary>
        public DateTime? PayTailEnd { get; set; }

        ///<summary>
        /// 预付百分比（注：此处为0到1的正数）
        ///</summary>
        public decimal? PresalePercent { get; set; }

        ///<summary>
        /// 限制次数（为0时不限制）
        ///</summary>
        public int LimitTimers { get; set; }

        ///<summary>
        /// 配送方式（1、自提，2、配送）
        ///</summary>
        public int ShippingModeId { get; set; }

        ///<summary>
        /// 是否可配送（1、是，0、否）
        ///</summary>
        public int IsShipping { get; set; }

        ///<summary>
        /// 是否可自提（1、是，0、否）
        ///</summary>
        public int IsStorePickUp { get; set; }

        ///<summary>
        /// 参与商品（1、全部，0、指定商品）
        ///</summary>
        public int JoinInProduct { get; set; }

        ///<summary>
        /// 删除状态（1、删除，0、正常）
        ///</summary>
        public int? IsDeleted { get; set; }

        ///<summary>
        /// 审核状态（1、已审核，0、待审核）
        ///</summary>
        public int? IsAudit { get; set; }

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
        /// 显示起始时间
        ///</summary>
        public DateTime? ShowStartTime { get; set; }

        ///<summary>
        /// 显示截止时间
        ///</summary>
        public DateTime? ShowEndTime { get; set; }

        ///<summary>
        /// 是否为秒杀活动（1.是；0.否；）
        ///</summary>
        public int? IsSeckill { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public PresaleActivity()
        {
            OperationAreaId = 1;
            PayRules = 1;
            LimitTimers = 0;
            ShippingModeId = 1;
            IsShipping = 0;
            IsStorePickUp = 1;
            JoinInProduct = 0;
            IsDeleted = 0;
            IsAudit = 0;
            CreateTime = DateTime.Now;
            ModifyTime = DateTime.Now;
            IsSeckill = 0;
        }
    }

}
