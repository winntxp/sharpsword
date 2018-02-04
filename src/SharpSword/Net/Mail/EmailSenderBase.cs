/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/27/2016 11:10:18 AM
 * ****************************************************************/
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SharpSword.Net.Mail
{
    /// <summary>
    /// This class can be used as base to implement <see cref="IEmailSender"/>.
    /// </summary>
    public abstract class EmailSenderBase : IEmailSender
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IEmailSenderConfiguration _configuration;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        protected EmailSenderBase(IEmailSenderConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isBodyHtml"></param>
        /// <returns></returns>
        public async Task SendAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await this.SendAsync(_configuration.DefaultFromAddress, to, subject, body, isBodyHtml);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isBodyHtml"></param>
        public void Send(string to, string subject, string body, bool isBodyHtml = true)
        {
            this.Send(_configuration.DefaultFromAddress, to, subject, body, isBodyHtml);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isBodyHtml"></param>
        /// <returns></returns>
        public async Task SendAsync(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            await this.SendAsync(new MailMessage(from, to, subject, body) { IsBodyHtml = isBodyHtml });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isBodyHtml"></param>
        public void Send(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            this.Send(new MailMessage(from, to, subject, body) { IsBodyHtml = isBodyHtml });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="normalize"></param>
        /// <returns></returns>
        public async Task SendAsync(MailMessage mail, bool normalize = true)
        {
            if (normalize)
            {
                this.NormalizeMail(mail);
            }

            await this.SendEmailAsync(mail);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="normalize"></param>
        public void Send(MailMessage mail, bool normalize = true)
        {
            if (normalize)
            {
                this.NormalizeMail(mail);
            }

            this.SendEmail(mail);
        }

        /// <summary>
        /// Should implement this method to send email in derived classes.
        /// </summary>
        /// <param name="mail">Mail to be sent</param>
        protected abstract Task SendEmailAsync(MailMessage mail);

        /// <summary>
        /// Should implement this method to send email in derived classes.
        /// </summary>
        /// <param name="mail">Mail to be sent</param>
        protected abstract void SendEmail(MailMessage mail);

        /// <summary>
        /// Normalizes given email.
        /// Fills <see cref="MailMessage.From"/> if it's not filled before.
        /// Sets encodings to UTF8 if they are not set before.
        /// </summary>
        /// <param name="mail">Mail to be normalized</param>
        protected virtual void NormalizeMail(MailMessage mail)
        {
            if (mail.From == null || mail.From.Address.IsNullOrEmpty())
            {
                mail.From = new MailAddress(
                    _configuration.DefaultFromAddress,
                    _configuration.DefaultFromDisplayName,
                    Encoding.UTF8
                    );
            }

            if (mail.HeadersEncoding.IsNull())
            {
                mail.HeadersEncoding = Encoding.UTF8;
            }

            if (mail.SubjectEncoding.IsNull())
            {
                mail.SubjectEncoding = Encoding.UTF8;
            }

            if (mail.BodyEncoding.IsNull())
            {
                mail.BodyEncoding = Encoding.UTF8;
            }
        }
    }
}