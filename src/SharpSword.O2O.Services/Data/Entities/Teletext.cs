/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 08/23/2017 15:31:02
 * ****************************************************************/

namespace SharpSword.Host.Data.Entities
{

    // Teletext
    public class Teletext
    {

        ///<summary>
        /// 图文直播ID
        ///</summary>
        public long TeletextId { get; set; } // TeletextID (Primary key)

        ///<summary>
        /// 直播标题
        ///</summary>
        public string TeletextName { get; set; } // TeletextName (length: 64)

        ///<summary>
        /// 供应商编号
        ///</summary>
        public int? VendorId { get; set; } // VendorID

        ///<summary>
        /// 活动编号
        ///</summary>
        public long? PresaleActivityId { get; set; } // PresaleActivityID

        ///<summary>
        /// 商品编号
        ///</summary>
        public int? ProductId { get; set; } // ProductID

        ///<summary>
        /// 删除状态(1.已删除；0.未删除；)
        ///</summary>
        public int? IsDeleted { get; set; } // IsDeleted

        ///<summary>
        /// 占赞人数
        ///</summary>
        public int? ThumbUpCount { get; set; } // ThumbUpCount

        ///<summary>
        /// 发布时间
        ///</summary>
        public System.DateTime? PublishTime { get; set; } // PublishTime

        ///<summary>
        /// 审核状态(1.待审核；2.通过；3.驳回；)
        ///</summary>
        public int? AuditState { get; set; } // AuditState

        ///<summary>
        /// 新增图文审核状态(1.待审核；2.通过；3.驳回；)
        ///</summary>
        public int? NewAuditState { get; set; } // NewAuditState

        ///<summary>
        /// 审核时间
        ///</summary>
        public System.DateTime? AuditTime { get; set; } // AuditTime

        ///<summary>
        /// 审核内容
        ///</summary>
        public string AuditContent { get; set; } // AuditContent (length: 1024)

        ///<summary>
        /// 审核人用户ID
        ///</summary>
        public int? AuditUserId { get; set; } // AuditUserID

        ///<summary>
        /// 审核用户名
        ///</summary>
        public string AuditUserName { get; set; } // AuditUserName (length: 32)

        ///<summary>
        /// 创建时间
        ///</summary>
        public System.DateTime CreateTime { get; set; } // CreateTime

        ///<summary>
        /// 创建用户 ID
        ///</summary>
        public int CreateUserId { get; set; } // CreateUserID

        ///<summary>
        /// 创建用户名称
        ///</summary>
        public string CreateUserName { get; set; } // CreateUserName (length: 32)

        ///<summary>
        /// 最后修改时间
        ///</summary>
        public System.DateTime ModifyTime { get; set; } // ModifyTime

        ///<summary>
        /// 最后修改用户ID
        ///</summary>
        public int? ModifyUserId { get; set; } // ModifyUserID

        ///<summary>
        /// 最后修改用户名称
        ///</summary>
        public string ModifyUserName { get; set; } // ModifyUserName (length: 32)

        // Reverse navigation

        /// <summary>
        /// Child TeletextDetails where [TeletextDetails].[TeletextID] point to this entity (FK_TELETEXTNOTAUDITEDDETAILS_REFERENCE_TELETEXT)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<TeletextDetail> TeletextDetails { get; set; } // TeletextDetails.FK_TELETEXTNOTAUDITEDDETAILS_REFERENCE_TELETEXT
        /// <summary>
        /// Child TeletextThumbUps where [TeletextThumbUp].[TeletextID] point to this entity (FK_TELETEXTTHUMBUP_REFERENCE_TELETEXT)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<TeletextThumbUp> TeletextThumbUps { get; set; } // TeletextThumbUp.FK_TELETEXTTHUMBUP_REFERENCE_TELETEXT

        public Teletext()
        {
            CreateTime = System.DateTime.Now;
            ModifyTime = System.DateTime.Now;
            TeletextDetails = new System.Collections.Generic.List<TeletextDetail>();
            TeletextThumbUps = new System.Collections.Generic.List<TeletextThumbUp>();
        }
    }

}
