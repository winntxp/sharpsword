/*************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/29/2016 1:55:56 PM
 * ***********************************************************/

namespace SharpSword.Notifications
{
    /// <summary>
    /// 用于外部扩展自定义消息类型注册上下文
    /// </summary>
    public interface INotificationDefinitionContext
    {
        /// <summary>
        /// 消息类型管理器
        /// </summary>
        INotificationDefinitionManager Manager { get; }
    }
}
