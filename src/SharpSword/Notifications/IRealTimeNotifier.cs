/* ***************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/27/2016 10:06:08 AM
 * ***************************************************************/
using System.Threading.Tasks;

namespace SharpSword.Notifications
{
    /// <summary>
    /// 实时通知接口
    /// </summary>
    public interface IRealTimeNotifier
    {
        /// <summary>
        /// 给所有客户端发送消息
        /// </summary>
        /// <param name="notificationData">消息数据</param>
        /// <returns></returns>
        Task SendNotificationsAsync(NotificationData notificationData);

        /// <summary>
        /// 实时推送消息给客户端
        /// </summary>
        /// <param name="userNotifications">用户消息集合</param>
        /// <returns></returns>
        Task SendNotificationsAsync(UserNotification[] userNotifications);
    }
}
