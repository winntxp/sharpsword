/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/6/2016 9:48:22 AM
 * ****************************************************************/
using System;

namespace SharpSword.Domain.Entitys
{
    /// <summary>
    /// 实体需要带创建时间信息
    /// </summary>
    public interface IHasCreationTime
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreationTime { get; set; }
    }
}
