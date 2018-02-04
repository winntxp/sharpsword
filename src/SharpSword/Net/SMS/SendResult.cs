/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/27/2016 11:42:14 AM
 * ****************************************************************/

namespace SharpSword.Net.SMS
{
    /// <summary>
    /// 发送结果对象
    /// </summary>
    public class SendResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        public SendResult(ResultStatus status, string message)
        {
            this.Status = status;
            this.Message = message;
        }

        /// <summary>
        /// 是否执行成功
        /// </summary>
        public ResultStatus Status { get; private set; }

        /// <summary>
        /// 失败或者成功的消息
        /// </summary>
        public string Message { get; private set; }

    }
}
