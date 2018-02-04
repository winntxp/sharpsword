/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System;

namespace SharpSword.OAuth
{
    /// <summary>
    /// 表示外部交易平台，存储该平台的基本接口信息。
    /// </summary>
    [Serializable]
    public class Platform
    {
        /// <summary>
        /// 
        /// </summary>
        private IList<App> _apps;

        /// <summary>
        /// 
        /// </summary>
        public Platform()
        {
            this._apps = new List<App>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apps"></param>
        public Platform(IEnumerable<App> apps)
        {
            this._apps = new List<App>(apps);
        }

        /// <summary>
        /// 平台的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 平台的全称
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 获取或设置平台的授权Url。
        /// </summary>
        public string AuthorizationUrl { get; set; }

        /// <summary>
        /// 获取或设置平台的令牌换取Url。
        /// </summary>
        public string TokenUrl { get; set; }

        /// <summary>
        /// 获取或设置平台的接口访问Url。
        /// </summary>
        public string ApiUrl { get; set; }

        /// <summary>
        /// 获取当前平台下的所有应用。
        /// </summary>
        public IReadOnlyCollection<App> Apps
        {
            get { return this._apps.ToImmutableList(); }
        }

        /// <summary>
        /// 获取当前平台下具有指定标识的应用。
        /// </summary>
        /// <param name="appkey">应用在其所属平台内的唯一标识。</param>
        public App GetApp(string appkey)
        {
            foreach (App app in this.Apps)
            {
                if (app.AppKey == appkey)
                {
                    return app;
                }
            }

            return null;
        }

        /// <summary>
        /// 在对应的平台下面添加一个APP应用
        /// </summary>
        /// <param name="app"></param>
        public void AddApp(App app)
        {
            app.CheckNullThrowArgumentNullException(nameof(app));
            this._apps.Add(app);
        }

        /// <summary>
        /// 再平台下面删除一个APP应用
        /// </summary>
        /// <param name="app"></param>
        public void RemoveApp(App app)
        {
            app.CheckNullThrowArgumentNullException(nameof(app));
            app.Platform.CheckNullThrowArgumentNullException(nameof(app.Platform));
            var application = this._apps.FirstOrDefault(o => o.Platform.Name == app.Platform.Name && o.AppKey == app.AppKey);
            if (!application.IsNull())
            {
                this._apps.Remove(application);
            }
        }
    }
}