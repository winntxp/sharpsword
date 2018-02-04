/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 14:47:30
 * ****************************************************************/
using System;

namespace SharpSword.Domain.Entitys
{
    /// <summary>
    /// 当我们的数据实体主键非Id的时候，我们继承此类
    /// </summary>
    [Serializable]
    public abstract class Entity : IEntity { }
}
