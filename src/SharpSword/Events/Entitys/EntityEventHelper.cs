/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/4/2016 1:52:37 PM
 * ****************************************************************/
using SharpSword.Domain.Uow;
using System;

namespace SharpSword.Events.Entitys
{
    /// <summary>
    /// 实体操作事件，内部会自动将触发事件挂靠到实体保存到存储介质的时候触发。将触发事件挂靠到工作单元的Commint方法提交成功后
    /// </summary>
    public class EntityEventHelper : IEntityEventHelper
    {
        /// <summary>
        /// 事件总线
        /// </summary>
        public IEventBus EventBus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWorkManager"></param>
        public EntityEventHelper(IUnitOfWorkManager unitOfWorkManager)
        {
            this.EventBus = NullEventBus.Instance;
            this._unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 触发实体创建事件
        /// </summary>
        /// <param name="entity"></param>
        public void TriggerEntityCreatedEvent(object entity)
        {
            this.TriggerEntityChangeEvent(typeof(EntityCreatedEventData<>), entity);
        }

        /// <summary>
        /// 触发实体更新事件
        /// </summary>
        /// <param name="entity"></param>
        public void TriggerEntityUpdatedEvent(object entity)
        {
            this.TriggerEntityChangeEvent(typeof(EntityUpdatedEventData<>), entity);
        }

        /// <summary>
        /// 触发实体删除事件
        /// </summary>
        /// <param name="entity"></param>
        public void TriggerEntityDeletedEvent(object entity)
        {
            this.TriggerEntityChangeEvent(typeof(EntityDeletedEventData<>), entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="genericEventType"></param>
        /// <param name="entity"></param>
        private void TriggerEntityChangeEvent(Type genericEventType, object entity)
        {
            var entityType = entity.GetType();
            var eventType = genericEventType.MakeGenericType(entityType);

            //事件
            var eventData = (IEventData)Activator.CreateInstance(eventType, entity);

            //工作单元不存在，直接事件总线触发
            if (null == _unitOfWorkManager || null == _unitOfWorkManager.Current)
            {
                //触发下事件
                EventBus.Trigger(eventType, eventData);
                return;
            }

            //工作单元(只有提交成功后，才触发事件)
            this._unitOfWorkManager.Current.Completed += (sender, args) => EventBus.Trigger(eventType, eventData);
        }
    }
}
