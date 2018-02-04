/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 08/23/2017 15:31:02
 * ****************************************************************/

namespace SharpSword.O2O.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class DistributionClerk
    {

        ///<summary>
        /// 配送员ID
        ///</summary>
        public int DistributionClerkId { get; set; } // DistributionClerkID (Primary key)

        ///<summary>
        /// 配送员名称
        ///</summary>
        public string DistributionClerkName { get; set; } // DistributionClerkName (length: 32)

        ///<summary>
        /// 区域ID(OperationArea.OperationAreaID)
        ///</summary>
        public int? OperationAreaId { get; set; } // OperationAreaID

        ///<summary>
        /// 冻结状态（1、冻结，0、正常）
        ///</summary>
        public int? IsLock { get; set; } // IsLock

        ///<summary>
        /// 删除状态（1、删除，0、正常）
        ///</summary>
        public int? IsDeleted { get; set; } // IsDeleted

        ///<summary>
        /// 创建时间
        ///</summary>
        public System.DateTime CreateTime { get; set; } // CreateTime

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
        public System.DateTime ModifyTime { get; set; } // ModifyTime

        ///<summary>
        /// 最后修改删除用户ID
        ///</summary>
        public int? ModifyUserId { get; set; } // ModifyUserID

        ///<summary>
        /// 最后修改删除用户名称
        ///</summary>
        public string ModifyUserName { get; set; } // ModifyUserName (length: 32)

        ///<summary>
        /// 联系电话
        ///</summary>
        public string DistributionClerkTel { get; set; } // DistributionClerkTel (length: 32)

        // Reverse navigation

        /// <summary>
        /// Child DistributionLines where [DistributionLine].[DistributionClerkID] point to this entity (FK_DISTRIBU_REFERENCE_DISTRIBU)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<DistributionLine> DistributionLines { get; set; } // DistributionLine.FK_DISTRIBU_REFERENCE_DISTRIBU

        // Foreign keys

        /// <summary>
        /// Parent OperationArea pointed by [DistributionClerk].([OperationAreaId]) (FK_DISTRIBU_REFERENCE_OPERATIO)
        /// </summary>
        public virtual OperationArea OperationArea { get; set; } // FK_DISTRIBU_REFERENCE_OPERATIO

        /// <summary>
        /// 
        /// </summary>
        public DistributionClerk()
        {
            OperationAreaId = 1;
            IsLock = 0;
            IsDeleted = 0;
            CreateTime = System.DateTime.Now;
            ModifyTime = System.DateTime.Now;
            DistributionLines = new System.Collections.Generic.List<DistributionLine>();
        }
    }

}
