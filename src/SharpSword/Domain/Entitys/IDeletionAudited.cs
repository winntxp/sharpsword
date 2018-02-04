/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/6/2016 9:50:00 AM
 * ****************************************************************/
using System;

namespace SharpSword.Domain.Entitys
{
    /// <summary>
    /// 记录软删除审计
    /// </summary>
    public interface IDeletionAudited : ISoftDelete
    {
        /// <summary>
        /// 删除用户编号
        /// </summary>
        string DeleterUserId { get; set; }

        /// <summary>
        /// 删除用户名称
        /// </summary>
        string DeleterUserName { get; set; }

        /// <summary>
        /// 软删除时间
        /// </summary>
        DateTime? DeletionTime { get; set; }
    }
}
