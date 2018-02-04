/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/3/2016 4:28:12 PM
 * ****************************************************************/
using System;

namespace SharpSword.Events.Entitys
{
    /// <summary>
    /// 实体事件基类型
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    [Serializable]
    public class EntityEventData<TEntity> : EventData, IEventDataWithInheritableGenericArgument
    {
        /// <summary>
        /// 
        /// </summary>
        public TEntity Entity { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public EntityEventData(TEntity entity)
        {
            this.Entity = entity;
        }

        /// <summary>
        /// 获取构造函数入参数据
        /// </summary>
        /// <returns></returns>
        public virtual object[] GetConstructorArgs()
        {
            return new object[] { this.Entity };
        }
    }
}
