/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/6/2016 9:49:16 AM
 * ****************************************************************/
using System;

namespace SharpSword.Domain.Entitys
{
    /// <summary>
    /// 实体需要带最后修改时间信息，实现此接口的实体类，在修改保存的时候，会自动带上最后修改时间存储到存储介质
    /// </summary>
    public interface IHasModificationTime
    {
        /// <summary>
        /// 修改时间
        /// </summary>
        DateTime? LastModifyTime { get; set; }

    }
}
