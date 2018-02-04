/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/25/2016 3:43:36 PM
 * ****************************************************************/
using System;
using System.Collections.Generic;

namespace SharpSword.Domain.Entitys
{
    /// <summary>
    /// 所有实体模型基类，非泛型的BaseEntity仅仅用于泛型约束，建议具体的实体也都加上Serializable特性
    /// 因为虽然大部分情况下我们采取JSON序列化对象，但是在有些情况下我们可能采取二进制的方式进行序列化
    /// 这就需要我们将实体对象定义成可序列化
    /// </summary>
    [Serializable]
    public abstract class Entity<TPrimaryKey> : Entity, IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 实体主键
        /// </summary>
        public TPrimaryKey Id { get; set; }

        /// <summary>
        /// 是否还未被持久化过
        /// </summary>
        /// <returns></returns>
        public bool IsTransient()
        {
            return EqualityComparer<TPrimaryKey>.Default.Equals(Id, default(TPrimaryKey));
        }

        /// <summary>
        /// 是否还未初始化（我们使用实体ID来判断是否已经初始化了）
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static bool IsTransient(Entity<TPrimaryKey> obj)
        {
            return !obj.IsNull() && Equals(obj.Id, default(TPrimaryKey));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Entity<TPrimaryKey>);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public virtual bool Equals(Entity<TPrimaryKey> other)
        {
            if (other.IsNull())
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (!IsTransient(this) && !IsTransient(other) && Equals(Id, other.Id))
            {
                var otherType = other.GetUnproxiedEntityType();
                var thisType = this.GetUnproxiedEntityType();
                return thisType.IsAssignableFrom(otherType) || otherType.IsAssignableFrom(thisType);
            }

            return false;
        }

        /// <summary>
        /// 当实体未初始化的时候，我们按照默认的方式生成HashCode
        /// 如果已经初始化了，我们以实体ID作为HashCode返回
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (Equals(Id, default(TPrimaryKey)))
            {
                return base.GetHashCode();
            }
            return Id.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator ==(Entity<TPrimaryKey> x, Entity<TPrimaryKey> y)
        {
            return Equals(x, y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator !=(Entity<TPrimaryKey> x, Entity<TPrimaryKey> y)
        {
            return !(x == y);
        }
    }
}
