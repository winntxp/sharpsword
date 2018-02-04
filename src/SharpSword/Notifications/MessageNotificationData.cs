/*************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/12/28 17:08:53
 * ***********************************************************/
using System;

namespace SharpSword.Notifications
{
    /// <summary>
    /// 一个普通的消息对象，继承自：NotificationData ，增加Message属性，方便我们进行消息传递
    /// </summary>
    [Serializable]
    public class MessageNotificationData : NotificationData
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 初始化通知消息对象
        /// </summary>
        /// <param name="message">消息内容</param>
        public MessageNotificationData(string message)
        {
            this.Message = message;
        }
    }
}
