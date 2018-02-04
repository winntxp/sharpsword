using SharpSword.Localization;
using System;
using System.Globalization;
/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using System.Threading;
using System.Web;

namespace SharpSword.Host
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiHostApplication : HttpApplication
    {
        /// <summary>
        /// 应用程序启动时初始化系统框架
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Start(object sender, EventArgs e)
        {
            //注册系统配置文件
            AppStarter.Initialize("~/DynamicWebConfig.cs");
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Application_BeginRequest()
        {
            //var localizationConfiguration = ServicesContainer.Current.Resolve<LocalizationConfiguration>();
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(localizationConfiguration.CultureName);
        }

        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            HttpContext httpContext = HttpContext.Current;
            string rawUrl = !httpContext.IsNull() ? httpContext.Request.RawUrl : string.Empty;
            var exception = new SharpSwordCoreException(rawUrl, Server.GetLastError().GetBaseException());
            ServicesContainer.Current.Resolve<ILogger<ApiHostApplication>>().Error(exception);
        }
    }
}