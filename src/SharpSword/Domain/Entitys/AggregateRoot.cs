/* *******************************************************
 * SharpSword zhangliang4629@163.com 9/26/2016 8:40:32 AM
 * ****************************************************************/
using System;

namespace SharpSword.Domain.Entitys
{
    /// <summary>
    /// 领域聚合根需要继承此抽象类，代表一个对象是聚合根
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class AggregateRoot<TKey> : Entity<TKey> { }
}
