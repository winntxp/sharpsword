/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/4/2016 2:16:19 PM
 * ****************************************************************/

namespace SharpSword.Events.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    public class NullEntityChangedEventHelper : IEntityEventHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static NullEntityChangedEventHelper Instance
        {
            get
            {
                return SingletonInstance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static readonly NullEntityChangedEventHelper SingletonInstance = new NullEntityChangedEventHelper();

        /// <summary>
        /// 
        /// </summary>
        private NullEntityChangedEventHelper() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void TriggerEntityCreatedEvent(object entity) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void TriggerEntityUpdatedEvent(object entity) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void TriggerEntityDeletedEvent(object entity) { }
    }
}
