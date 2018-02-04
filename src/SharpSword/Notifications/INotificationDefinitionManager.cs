/* ***************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/29/2016 1:49:30 PM
 * ***************************************************************/
using System.Collections.Generic;

namespace SharpSword.Notifications
{
    /// <summary>
    /// 消息类型管理器接口
    /// </summary>
    public interface INotificationDefinitionManager
    {
        /// <summary>
        /// 添加一个消息类型
        /// </summary>
        void Add(NotificationDefinition notificationDefinition);

        /// <summary>
        /// 根据消息类型名称获取消息类型详细信息，如果不存在，请实现为返回null
        /// </summary>
        /// <returns></returns>
        NotificationDefinition Get(string name);

        /// <summary>
        /// 获取框架注册的所有消息类型
        /// </summary>
        /// <returns>返回所有已经注册的消息类型</returns>
        IReadOnlyList<NotificationDefinition> GetAll();
    }
}
