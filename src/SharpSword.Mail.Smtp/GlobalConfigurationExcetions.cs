/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/23/2016 1:56:32 PM
 * *******************************************************/

namespace SharpSword.Mail.Smtp
{
    public static class GlobalConfigurationExcetions
    {
        public static void UseSmtpEmail(this GlobalConfiguration globalConfiguration, SmtpEmailSenderConfiguration config)
        {
            config.CheckNullThrowArgumentNullException(nameof(config));
            globalConfiguration.SetConfig(config);
        }
    }
}
