/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/27/2016 11:10:18 AM
 * ****************************************************************/
using SharpSword.Configuration;

namespace SharpSword.Net.Mail
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEmailSenderConfiguration: ISetting
    {
        /// <summary>
        /// 发送方默认的电子邮件
        /// </summary>
        string DefaultFromAddress { get; }
        
        /// <summary>
        /// 发送方默认的发送名称
        /// </summary>
        string DefaultFromDisplayName { get; }
    }
}