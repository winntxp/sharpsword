/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/27/2016 11:10:18 AM
 * ****************************************************************/
using SharpSword.Configuration.WebConfig;
using System;
using System.ComponentModel;

namespace SharpSword.Mail.Smtp
{
    /// <summary>
    /// SMTP协议发送电子邮件配置文件，默认不自动注册
    /// </summary>
    [ConfigurationSectionName("sharpsword.module.mail.Smtp.configuration"), Serializable]
    public class SmtpEmailSenderConfiguration : ConfigurationSectionHandlerBase, ISmtpEmailSenderConfiguration
    {
        /// <summary>
        /// SMTP Host name/IP.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// SMTP Port.
        /// </summary>
        [DefaultValue(25)]
        public int Port { get; set; }

        /// <summary>
        /// 账户
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 邮箱SMTP服务器地址，可以为null
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Is SSL enabled?
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// Use default credentials?
        /// </summary>
        public bool UseDefaultCredentials { get; set; }

        /// <summary>
        /// 默认发送的地址
        /// </summary>
        public string DefaultFromAddress { get; set; }

        /// <summary>
        /// 默认显示的名称
        /// </summary>
        public string DefaultFromDisplayName { get; set; }
    }
}