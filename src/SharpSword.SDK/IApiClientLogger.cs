/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/2/2015 8:32:16 PM
 * ****************************************************************/

namespace SharpSword.SDK
{
    /// <summary>
    /// 日志打点接口。
    /// </summary>
    public interface IApiClientLogger
    {
        /// <summary>
        /// 记录错误类型日志
        /// </summary>
        /// <param name="message"></param>
        void Error(string message);

        /// <summary>
        /// 记录警告类型日志
        /// </summary>
        /// <param name="message"></param>
        void Warn(string message);

        /// <summary>
        /// 记录消息类型日志
        /// </summary>
        /// <param name="message"></param>
        void Info(string message);
    }
}
