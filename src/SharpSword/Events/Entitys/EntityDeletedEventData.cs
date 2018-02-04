﻿/* ****************************************************************
 * SharpSword zhangliang4629@sharpsword.com.cn 10/3/2016 4:26:48 PM
 * ****************************************************************/
using System;

namespace SharpSword.Events.Entitys
{
    /// <summary>
    /// 当实体数据被删除的时候触发
    /// 说明：此事件只有使用基于实体对象方式操作数据的时候会触发，自定义SQL语句的无法触发此事件
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    [Serializable]
    public class EntityDeletedEventData<TEntity> : EntityEventData<TEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public EntityDeletedEventData(TEntity entity) : base(entity) { }
    }
}