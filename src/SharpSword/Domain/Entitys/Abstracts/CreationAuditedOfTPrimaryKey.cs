/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/15/2016 3:39:48 PM
 * ****************************************************************/
using System;

namespace SharpSword.Domain.Entitys
{
    /// <summary>
    /// 创建审计信息
    /// </summary>
    public abstract class CreationAudited<TPrimaryKey> : Entity<TPrimaryKey>, ICreationAudited
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 创建用户编号
        /// </summary>
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 创建用户名称
        /// </summary>
        public string CreatorUserName { get; set; }
    }
}
