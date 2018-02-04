/* ***************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/27/2016 10:06:08 AM
 * ***************************************************************/

namespace SharpSword.Notifications
{
    /// <summary>
    /// 用于向特定用户发送消息对象
    /// </summary>
    public class UserNotification
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 发送的消息对象
        /// </summary>
        public NotificationData Data { get; set; }
    }
}
