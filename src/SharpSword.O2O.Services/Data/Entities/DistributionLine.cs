/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 08/23/2017 15:31:02
 * ****************************************************************/
using SharpSword.Domain.Entitys;

namespace SharpSword.O2O.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class DistributionLine : Entity
    {
        ///<summary>
        /// 路线ID
        ///</summary>
        public int LineId { get; set; } // LineID (Primary key)

        ///<summary>
        /// 路线名称
        ///</summary>
        public string LineName { get; set; } // LineName (length: 32)

        ///<summary>
        /// 区域ID(OperationArea.OperationAreaID)
        ///</summary>
        public int? OperationAreaId { get; set; } // OperationAreaID

        ///<summary>
        /// 路线ID
        ///</summary>
        public int? DistributionClerkId { get; set; } // DistributionClerkID

        ///<summary>
        /// 删除状态（1、删除，0、正常）
        ///</summary>
        public int? IsDelete { get; set; } // IsDelete

        ///<summary>
        /// 冻结状态（1、冻结，0、正常）
        ///</summary>
        public int? IsLock { get; set; } // IsLock

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

        // Foreign keys

        /// <summary>
        /// Parent DistributionClerk pointed by [DistributionLine].([DistributionClerkId]) (FK_DISTRIBU_REFERENCE_DISTRIBU)
        /// </summary>
        public virtual DistributionClerk DistributionClerk { get; set; } // FK_DISTRIBU_REFERENCE_DISTRIBU

        /// <summary>
        /// Parent OperationArea pointed by [DistributionLine].([OperationAreaId]) (FK_OperationArea_To_DistributionLine)
        /// </summary>
        public virtual OperationArea OperationArea { get; set; } // FK_OperationArea_To_DistributionLine

        /// <summary>
        /// 
        /// </summary>
        public DistributionLine()
        {
            DistributionClerkId = 1;
            IsDelete = 0;
            IsLock = 0;
            CreateTime = System.DateTime.Now;
            ModifyTime = System.DateTime.Now;
        }
    }

}
