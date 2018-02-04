/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/27/2016 11:42:14 AM
 * ****************************************************************/
using System.Threading.Tasks;

namespace SharpSword.Net.SMS
{
    /// <summary>
    /// 发送短信接口，此接口系统框架会自动注册空实现，因此在使用的地方无需在构造函数里进行手工空实现赋值
    /// </summary>
    public interface ISMSSender
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="to">收短信人手机号码</param>
        /// <param name="body">短信内容</param>
        SendResult Send(string to, string body);

        /// <summary>
        /// 异步方式发送短信
        /// </summary>
        /// <param name="to">收短信人手机号码</param>
        /// <param name="body">短信内容</param>
        /// <returns></returns>
        Task<SendResult> SendAsync(string to, string body);
    }
}
