/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace SharpSword.OAuth
{
    /// <summary>
    /// 基于WEB.config配置
    /// </summary>
    public class WebConfigAppSourceProvider : IAppSourceProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<App> GetApps()
        {
            var applicationCollection = new List<App>();
            var platformCollection = new List<Platform>();

            //配置文件中查找平台属性填充(注意填充平台下包含的应用)
            OAuthConfig oAuthLoginConfig;
            try
            {
                oAuthLoginConfig = (OAuthConfig)ConfigurationManager.GetSection("oauth");
            }
            catch (Exception exc)
            {
                throw new SharpSwordCoreException("配置错误，详细错误：{0}，{1}".With(exc.Message, exc.StackTrace));
            }

            var platforms = oAuthLoginConfig.Platforms;
            var providers = oAuthLoginConfig.AuthorizationProviders;

            var authorizationProviders = new Dictionary<string, IAuthorizationProvider>();
            for (int i = 0; i < providers.Count; i++)
            {
                var authorizationProvider = (IAuthorizationProvider)Activator.CreateInstance(Type.GetType(providers[i].Type));
                authorizationProviders.Add(providers[i].Platform, authorizationProvider);
            }

            foreach (PlatformElement item in platforms)
            {
                //填充平台数据
                Platform platform = new Platform();
                platform.Name = item.Name;
                platform.FullName = item.FullName;
                platform.ApiUrl = item.ApiUrl;
                platform.AuthorizationUrl = item.AuthorizationUrl;
                platform.TokenUrl = item.TokenUrl;

                //授权提供程序               
                for (int i = 0; i < item.Apps.Count; i++)
                {
                    //填充平台下应用数据
                    var provider = authorizationProviders.FirstOrDefault(o => o.Key == item.Name).Value;
                    if (provider.IsNull())
                    {
                        continue;
                    }

                    //在认证授权提供者有的情况下，我们才将APP信息注册到系统里面
                    App app = new App(platform, provider);
                    app.AppKey = item.Apps[i].Appkey;
                    app.Secret = item.Apps[i].Secret;
                    app.RedirectUrl = item.Apps[i].RedirectUrl;
                    platform.AddApp(app);
                    applicationCollection.Add(app);
                }

                //添加到平台集合
                platformCollection.Add(platform);
            }

            return applicationCollection;
        }
    }
}
