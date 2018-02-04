/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/22 20:37:56
 * ****************************************************************/

namespace SharpSword.MQ
{
    /// <summary>
    /// 消息队列发布一个消息
    /// </summary>
    public interface IMessagePublisher
    {
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="message">消息对象实体</param>
        /// <param name="messageLabel">消息标签</param>
        /// <returns></returns>
        bool Publish<T>(T message, string messageLabel);
     }
}
