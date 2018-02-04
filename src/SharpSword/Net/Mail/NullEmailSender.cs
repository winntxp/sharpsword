/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/27/2016 11:10:18 AM
 * ****************************************************************/
using System.Net.Mail;

namespace SharpSword.Net.Mail
{
    /// <summary>
    /// 电子邮件空实现
    /// </summary>
    public class NullEmailSender : EmailSenderBase
    {
        /// <summary>
        /// 
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Creates a new <see cref="NullEmailSender"/> object.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        public NullEmailSender(IEmailSenderConfiguration configuration)
            : base(configuration)
        {
            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        protected override System.Threading.Tasks.Task SendEmailAsync(MailMessage mail)
        {
            if (this.Logger.IsEnabled(LogLevel.Debug))
            {
                Logger.Debug("SendEmailAsync:");
            }
            LogEmail(mail);
            return System.Threading.Tasks.Task.FromResult(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mail"></param>
        protected override void SendEmail(MailMessage mail)
        {
            if (this.Logger.IsEnabled(LogLevel.Debug))
            {
                Logger.Debug("SendEmail:");
            }
            LogEmail(mail);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mail"></param>
        private void LogEmail(MailMessage mail)
        {
            if (this.Logger.IsEnabled(LogLevel.Debug))
            {
                Logger.Debug(mail.To.ToString());
                Logger.Debug(mail.CC.ToString());
                Logger.Debug(mail.Subject);
                Logger.Debug(mail.Body);
            }
        }
    }
}