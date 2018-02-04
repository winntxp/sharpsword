/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/29/2016 2:58:02 PM
 * *******************************************************/

namespace SharpSword.Notifications
{
    /// <summary>
    /// 提供给外部配置NotificationProvider实现，用来初始化NotificationDefinitionManager里的NotificationDefinition
    /// </summary>
    internal class NotificationConfiguration : INotificationConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        public ITypeList<NotificationDefinitionProvider> Providers { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public NotificationConfiguration()
        {
            this.Providers = new TypeList<NotificationDefinitionProvider>();
        }
    }
}
