/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/15/2016 3:43:52 PM
 * ****************************************************************/
using System;

namespace SharpSword.Domain.Entitys
{
    /// <summary>
    /// 创建时间，修改时间 都会被记录
    /// </summary>
    public abstract class FullAudited<TPrimaryKey> : CreationAudited<TPrimaryKey>, IFullAudited
    {
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用户编号
        /// </summary>
        public string LastModifyUserId { get; set; }

        /// <summary>
        /// 最后修改用户名称
        /// </summary>
        public string LastModifyUserName { get; set; }
    }
}
