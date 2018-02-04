/* ***************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/27/2016 10:06:08 AM
 * ***************************************************************/
using System.Threading.Tasks;

namespace SharpSword.Notifications
{
    /// <summary>
    /// 实时消息发送空实现，系统框架默认的注册，具体的实现我们留到外部去实现
    /// </summary>
    public class NullRealTimeNotifier : IRealTimeNotifier
    {
        /// <summary>
        /// 
        /// </summary>
        public static NullRealTimeNotifier Instance { get { return SingletonInstance; } }

        /// <summary>
        /// 
        /// </summary>
        private static readonly NullRealTimeNotifier SingletonInstance = new NullRealTimeNotifier();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userNotifications"></param>
        /// <returns></returns>
        public Task SendNotificationsAsync(UserNotification[] userNotifications)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notificationData"></param>
        /// <returns></returns>
        public Task SendNotificationsAsync(NotificationData notificationData)
        {
            return Task.FromResult(0);
        }
    }
}
