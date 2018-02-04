/* ****************************************************************
 * SharpSword zhangliang4629@163.com 12/15/2016 3:39:48 PM
 * ****************************************************************/
using System;

namespace SharpSword.Domain.Entitys
{
    /// <summary>
    /// 删除审计信息
    /// </summary>
    public abstract class DeletionAudited<TPrimaryKey> : Entity<TPrimaryKey>, IDeletionAudited
    {
        /// <summary>
        /// 删除用户编号
        /// </summary>
        public string DeleterUserId { get; set; }

        /// <summary>
        /// 删除用户名称
        /// </summary>
        public string DeleterUserName { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeletionTime { get; set; }

        /// <summary>
        /// 是否已经被删除标识
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
