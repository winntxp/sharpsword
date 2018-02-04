/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace SharpSword.OAuth
{
    /// <summary>
    /// 此第三方平台登录管理器需要注册成单例模式
    /// </summary>
    internal class OAuthManager : IOAuthManager
    {
        /// <summary>
        /// 
        /// </summary>
        private IList<App> _cachedApps = new List<App>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appSourceProvider"></param>
        public OAuthManager(IAppSourceProvider appSourceProvider)
        {
            _cachedApps = appSourceProvider.IsNull()
                ? new NullAppSourceProvider().GetApps().ToList()
                : new List<App>(appSourceProvider.GetApps());
        }

        /// <summary>
        /// 系统所有注册的应用信息集合，只读
        /// </summary>
        public IReadOnlyCollection<App> Apps
        {
            get
            {
                return _cachedApps.ToImmutableList();
            }
        }

        /// <summary>
        /// 注册一个APP应用信息到系统
        /// </summary>
        /// <param name="app"></param>
        public void AddApp(App app)
        {
            if (this.GetApp(app.Platform.Name, app.AppKey).IsNull())
            {
                _cachedApps.Add(app);
            }
        }

        /// <summary>
        /// 根据平台名称和APPKEY获取到应用信息
        /// </summary>
        /// <param name="platformName"></param>
        /// <param name="appKey"></param>
        /// <returns></returns>
        public App GetApp(string platformName, string appKey)
        {
            return _cachedApps.FirstOrDefault(o => o.AppKey == appKey && o.Platform.Name == platformName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="platformName"></param>
        /// <returns></returns>
        public IEnumerable<App> GetApps(string platformName)
        {
            return _cachedApps.Where(o => o.Platform.Name == platformName).ToList();
        }
    }
}
