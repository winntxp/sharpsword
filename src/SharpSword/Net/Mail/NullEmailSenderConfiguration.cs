/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/27/2016 11:10:18 AM
 * ****************************************************************/
using System;

namespace SharpSword.Net.Mail
{
    /// <summary>
    /// Defines configurations used while sending emails.
    /// </summary>
    public class NullEmailSenderConfiguration : IEmailSenderConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        public string DefaultFromAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DefaultFromDisplayName { get; set; }
    }
}