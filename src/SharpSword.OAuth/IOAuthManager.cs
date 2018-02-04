/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.OAuth
{
    /// <summary>
    /// 第三方平台授权登录入口
    /// </summary>
    public interface IOAuthManager
    {
        /// <summary>
        /// 获取当前系统注册的所有第三方APP应用信息
        /// </summary>
        IReadOnlyCollection<App> Apps { get; }

        /// <summary>
        /// 注册一个应用到系统应用集合
        /// </summary>
        /// <param name="app">指定平台的APP应用</param>
        void AddApp(App app);

        /// <summary>
        /// 根据对应的平台和应用appkey获取注册的应用
        /// </summary>
        /// <param name="platformName">开放平台编码</param>
        /// <param name="appKey">开放平台对应的APPkey</param>
        /// <returns></returns>
        App GetApp(string platformName, string appKey);

        /// <summary>
        /// 获取指定平台下所有已经注册的APP信息
        /// </summary>
        /// <param name="platformName">开放平台编码</param>
        /// <returns></returns>
        IEnumerable<App> GetApps(string platformName);
    }

}