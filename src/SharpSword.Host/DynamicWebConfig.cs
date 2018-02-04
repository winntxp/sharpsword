/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/29 12:18:22
 * ****************************************************************/
using SharpSword.DynamicApi;
using SharpSword.Localization;
using SharpSword.Localization.Sources;
using SharpSword.Localization.Sources.Xml;
using SharpSword.WebApi;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SharpSword.Host
{
    /// <summary>
    /// 动态编译并注册接口框架配置
    /// 注意：此文件需要单独放置于网站根目录；系统框架将会在启动的时候自动进行注册配置；
    /// 开发的时候此文件生成操作设置成：编译；发布的时候，请将此文件生成操作设置为：内容
    /// </summary>
    public class DynamicWebConfig : IDynamicCompiledDependencyRegistar
    {
        /// <summary>
        /// 注册各种需要初始化的数据
        /// </summary>
        public void Register(GlobalConfiguration globalConfiguration)
        {
            // 服务器名称；正式环境里，请将不同的API服务器名称分别配置，便于在分布式接口中跟踪调试服务器
            globalConfiguration.ServerName = string.Format("sharpsword-server-{0}", Guid.NewGuid().ToString("N"));

            // HOST站点地址，如果是IP配置的站点，格式为：192.x.x.x 或者 192.x.x.x:80 等
            globalConfiguration.HttpHost = "www.sharpsword.com";

            // 检测用户ID和用户名称是否提交(框架里只定义接口，具体不实现。交给外部配置来检测用户名称和用户ID是否提交了)
            globalConfiguration.ValidUserIdAndUserNameFun = (requestDto) =>
            {
                //皮之不存毛将焉附?
                if (requestDto.IsNull())
                {
                    return false;
                }
                return !string.IsNullOrWhiteSpace(requestDto.UserId) && !string.IsNullOrWhiteSpace(requestDto.UserName);
            };

            //动态接口API输出器配置(项目5.0.SharpSword.DynamicApi配置)
            globalConfiguration.UseDynamicApi(new DynamicApiConfig()
            {
                DynamicDirectory = "~/App_Data/DynamicApi",
                ActionNameSpace = "DynaimcApi.Actions",
                WorkMode = WorkMode.Dynamic | WorkMode.Develop
            });

            //本地化配置
            var localizationConfiguration = new LocalizationConfiguration();
            localizationConfiguration.Languages.Add(new LanguageInfo("zh-CN", "中文") { IsDefault = true });
            localizationConfiguration.Languages.Add(new LanguageInfo("en", "英语"));
            localizationConfiguration.Sources.Add(new DictionaryBasedLocalizationSource("SharpSword",
                new XmlEmbeddedFileLocalizationDictionaryProvider(typeof(IDependency).Assembly, "SharpSword.Localization.XmlSource")));
            //localizationConfiguration.Sources.Add(new DictionaryBasedLocalizationSource("SharpSword0",
            //    new XmlFileLocalizationDictionaryProvider("~/App_Data/Languages")));
            localizationConfiguration.CultureName = "zh-CN";
            localizationConfiguration.IsEnabled = true;
            localizationConfiguration.LocalizerSourceName = "SharpSword";
            globalConfiguration.UseLocalization(localizationConfiguration);

            //审计存储配置
            // globalConfiguration.UseSqlServerAudited(new AuditingStoreConfig()
            //{
            //    ConnectionStringName = "V200",
            //    IsEnabled = true
            //});

            //WEBAPI接口配置
            globalConfiguration.UseWebApi(new ApiConfiguration()
            {
                EnabledAccessRecod = true,
                DefaultActionVersionFailToHighestActionVersion = false,
                //ActionDocResourcePaths = new string[] { "~/bin/ServiceCenter.Api.Host.XML", "~/config/ServiceCenter.Api.Core.XML" }
            });

            // 2.注册IP白名单(正式环境根据安全性需要来进行配置)
            //WhiteIpManager.Ips.Add("192.168.0.11", "192.168.3.24", "192.168.2.20", "192.168.3.122");

            // 3.注册全局拦截器
            //GlobalActionFiltersManager.Filters.Add(new GlobalRequestHanderInterceptors.DefaultGlobalRequestHanderInterceptor());

            // 4.如果需要给接口配置不同的策略，可以按照下面方式来配置注意先后顺序，后注册的配置文件会覆盖掉前面注册的属性配置
            ApiConfigManager.Configs

            .Register(new ActionConfigItem()
            {
                //框架级别全局接口配置，全部接口都会生效，同时会覆盖掉对应接口框架默认的参数设置(兜底设置)

                //缓存过期时间设置（>0为启用缓存，时间单位为：分钟）
                //CacheTime                     = 0,

                //是否允许Ajax访问
                //EnableAjaxRequest             = false,

                //允许客户端提交方式
                //HttpMethod                    = HttpMethod.POST | HttpMethod.GET,

                //接口是否过期
                //Obsolete                      = false,

                //是否需要安全连接访问
                //RequireHttps                  = false,

                //是否进行全局校验
                //AllowAnonymous                = true,

                //是否允许打包到SDK
                //CanPackageToSdk               = true,

                //是否允许记录日志
                //EnableRecordApiLog            = true
            })

            // 接口注销(正式环境下需要将将界面显示的接口注销掉)
            // .Obsolete("API.BuildSDK", "Api.Doc", "Api.Doc.Builder", "API.Logs.Get", "API.Logs.List", "API.TestTool")
            // .Obsolete("API.BuildSDK", "1.0")
            // .ObsoleteSystemActions()

            //注册特性路由
            .Route("API.ServerTime.Get", "api/gettime")

            //.Obsolete("API.Index")

            //注册配置
            .Register("API.ServerTime.Get", new ActionConfigItem()
            {
                // 单一接口未指定版本全局配
                AllowAnonymous = true,
                EnableAjaxRequest = false,
                EnableRecordApiLog = true,
                HttpMethod = HttpMethod.POST | HttpMethod.GET,
                Obsolete = false,
                RequireHttps = false
            })

            // 匿名方式进行注册，只要匿名对象属性与配置对象参数一致既可(参数不区分大小写)
            .Register("API.ServerTime.Get", new { RequireHttps = false, CacheTime = 0 })

            //接口分组
            .Group("API.ServerTime.Get", "公共接口")

            // 注册1.0
            .Register("API.ServerTime.Get", "1.0", new ActionConfigItem()
            {
                //是否忽略掉操作用户
                HttpMethod = HttpMethod.POST | HttpMethod.GET

                // 1.0版本值配置了缓存配置，但是系统框架会自动将此属性赋值到此接口未指定版本号的配置上面，
                // 这样此接口配置的真实配置为

                // ********************************************************
                // AllowAnonymous       = true
                // CacheTime            = 100
                // EnableAjaxRequest    = false
                // EnableRecordApiLog   = true
                // HttpMethod           = HttpMethod.POST | HttpMethod.GET
                // Obsolete             = false
                // RequireHttps         = false
                // ********************************************************

            })

            .Register("API.ServerTime.Get", "1.0", new ActionConfigItem()
            {
                //不走加解密流程
                DataSignatureTransmission = false,

                //执行完事件后，会自动执行此缓存键清理
                //UnloadCacheKeys = new[] { "API.Logs" }
            });

            //删除掉WebForm视图
            var webFormViewEngine = ViewEngines.Engines.FirstOrDefault(o => o.GetType() == typeof(WebFormViewEngine));
            ViewEngines.Engines.Remove(webFormViewEngine);
        }
    }
}