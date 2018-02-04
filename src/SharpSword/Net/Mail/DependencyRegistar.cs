/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;

namespace SharpSword.Net.Mail
{
    /// <summary>
    /// 
    /// </summary>
    internal class DependencyRegistar : DependencyRegistarBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="typeFinder"></param>
        /// <param name="globalConfiguration"></param>
        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            //注册空的邮件发送实现
            containerBuilder.RegisterType<NullEmailSender>()
                            .As<IEmailSender>()
                            .SingleInstance()
                            .PropertiesAutowired()
                            //注册下自带入参
                            .WithParameter(new NamedParameter("configuration", new NullEmailSenderConfiguration()
                            {
                                DefaultFromAddress = "master@smtp.sharpsword.com",
                                DefaultFromDisplayName = "sharpsword"
                            }));
        }
    }
}
