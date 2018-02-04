/* ****************************************************************
 * SharpSword zhangliang4629@163.com 8/25/2016 2:47:37 PM
 * ****************************************************************/
using System;

namespace SharpSword.Domain.Entitys
{
    /// <summary>
    /// 物理输出标志，注意，当实体同时实现,ISoftDelete，IPhysicallyDelete接口，将会采取物理删除方式
    /// </summary>
    public interface IPhysicallyDelete { }
}
