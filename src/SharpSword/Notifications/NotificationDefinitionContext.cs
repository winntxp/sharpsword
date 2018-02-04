/* ***************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/29/2016 1:58:03 PM
 * ***************************************************************/

namespace SharpSword.Notifications
{
    /// <summary>
    /// 
    /// </summary>
    internal class NotificationDefinitionContext : INotificationDefinitionContext
    {
        /// <summary>
        /// 
        /// </summary>
        public INotificationDefinitionManager Manager { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="manager"></param>
        public NotificationDefinitionContext(INotificationDefinitionManager manager)
        {
            this.Manager = manager;
        }
    }
}
