/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/29/2016 2:53:56 PM
 * *******************************************************/

namespace SharpSword.Notifications
{
    /// <summary>
    /// 提供给外部配置NotificationProvider
    /// </summary>
    public interface INotificationConfiguration
    {
        /// <summary>
        /// 注册消息类型提供者
        /// </summary>
        ITypeList<NotificationDefinitionProvider> Providers { get; }
    }
}
