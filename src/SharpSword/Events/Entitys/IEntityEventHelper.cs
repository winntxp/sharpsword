/* ****************************************************************
 * SharpSword zhangliang4629@163.com 10/4/2016 1:51:57 PM
 * ****************************************************************/
using System;

namespace SharpSword.Events.Entitys
{
    /// <summary>
    /// 当实体进行存储操作的时候触发事件封装，用于在工作单元提交完成后触发事件。
    /// </summary>
    public interface IEntityEventHelper
    {
        /// <summary>
        /// 创建实体事件
        /// </summary>
        /// <param name="entity"></param>
        void TriggerEntityCreatedEvent(object entity);

        /// <summary>
        /// 更新实体事件
        /// </summary>
        /// <param name="entity"></param>
        void TriggerEntityUpdatedEvent(object entity);

        /// <summary>
        /// 删除实体事件
        /// </summary>
        /// <param name="entity"></param>
        void TriggerEntityDeletedEvent(object entity);
    }
}
