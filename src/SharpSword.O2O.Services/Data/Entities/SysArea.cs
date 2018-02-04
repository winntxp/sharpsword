/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 08/23/2017 15:31:02
 * ****************************************************************/

namespace SharpSword.O2O.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class SysArea
    {

        ///<summary>
        /// 区域编号
        ///</summary>
        public int AreaId { get; set; } // AreaID (Primary key)

        ///<summary>
        /// 区域名称
        ///</summary>
        public string AreaName { get; set; } // AreaName (length: 32)

        ///<summary>
        /// 全称
        ///</summary>
        public string AreaFullName { get; set; } // AreaFullName (length: 64)

        ///<summary>
        /// 层级
        ///</summary>
        public int Level { get; set; } // Level

        ///<summary>
        /// 上级编号
        ///</summary>
        public int ParentId { get; set; } // ParentID

        ///<summary>
        /// 排序
        ///</summary>
        public int? SortId { get; set; } // SortID

        ///<summary>
        /// 最后更新时间
        ///</summary>
        public System.DateTime? ModifyTime { get; set; } // ModifyTime

        ///<summary>
        /// 最后更新用户ID
        ///</summary>
        public int? ModifyUserId { get; set; } // ModifyUserId

        ///<summary>
        /// 最后更新用户名称
        ///</summary>
        public string ModifyUserName { get; set; } // ModifyUserName (length: 32)
    }

}
