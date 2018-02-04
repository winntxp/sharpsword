using SharpSword.Net.Mail;
/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/27/2016 11:10:18 AM
 * ****************************************************************/
using System.Net.Mail;

namespace SharpSword.Mail.Smtp
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISmtpEmailSender : IEmailSender
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        SmtpClient BuildClient();
    }
}