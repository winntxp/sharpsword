/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/29/2016 2:07:32 PM
 * *******************************************************/

namespace SharpSword.Notifications
{
    /// <summary>
    /// 消息类型提供者，用于向INotificationDefinitionManager对象（NotificationDefinitionManager）中添加NotificationDefinition
    /// </summary>
    public abstract class NotificationDefinitionProvider
    {
        /// <summary>
        /// 添加消息类型定义.
        /// </summary>
        /// <param name="context">Context</param>
        public abstract void SetNotificationDefinitions(INotificationDefinitionContext context);
    }
}
