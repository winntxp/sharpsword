/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/16/2017 2:43:02 PM
 * ****************************************************************/
using SharpSword.Net.Mail;
using System;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 报警系统-采取发送邮件的方式实现
    /// </summary>
    public class MaillSystemWarningTrigger : ISystemWarningTrigger, IPerLifetimeDependency
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IEmailSender _emailSender;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailSender"></param>
        public MaillSystemWarningTrigger(IEmailSender emailSender)
        {
            this._emailSender = emailSender;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="waningMessage"></param>
        /// <param name="exception"></param>
        public void Warning(object source, string waningMessage, Exception exception = null)
        {
            this._emailSender.Send("24040132@qq.com", waningMessage, exception.StackTrace, true);
        }
    }
}
