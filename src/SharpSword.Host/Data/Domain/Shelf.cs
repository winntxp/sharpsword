/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/3/9 13:54:58
 * ****************************************************************/
using System;
using SharpSword.Domain.Entitys;

namespace SharpSword.Host.Data.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class Shelf : Entity<int>, ISoftDelete, IHasCreationTime, ICreationAudited, IHasModificationTime, IModificationAudited, IPhysicallyDelete
    {
        /// <summary>
        /// 
        /// </summary>
        public string Wid { get; set; }

        /// <summary>
        /// 货区
        /// </summary>
        public string ShelfName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Warehouse Warehouse { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改删除用户ID
        /// </summary>
        public string LastModifyUserId { get; set; }

        /// <summary>
        /// 最后修改删除用户名称
        /// </summary>
        public string LastModifyUserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorUserName { get; set; }
    }
}