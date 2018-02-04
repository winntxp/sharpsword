/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/27/2016 11:42:14 AM
 * ****************************************************************/
using System.Threading.Tasks;

namespace SharpSword.Net.SMS
{
    /// <summary>
    /// 短信发送接口抽象基类
    /// </summary>
    public abstract class SMSSenderBase : ISMSSender
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="body"></param>
        public SendResult Send(string to, string body)
        {
            return this.SendAsync(to, body).Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public abstract Task<SendResult> SendAsync(string to, string body);
    }
}
