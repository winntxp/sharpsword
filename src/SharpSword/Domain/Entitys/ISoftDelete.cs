/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/25/2016 2:47:58 PM
 * ****************************************************************/
using System;

namespace SharpSword.Domain.Entitys
{
    /// <summary>
    /// 实体是否实现软删除
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// 是否已经被删除
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
