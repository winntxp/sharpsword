/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/22 20:37:56
 * ****************************************************************/
using Autofac.Integration.Mvc;
using SharpSword.Notifications;
using SharpSword.Tasks;
using System;
using System.IO;
using System.Web.Mvc;
using System.Web.Routing;

namespace SharpSword
{
    /// <summary>
    /// 系统初始化入口
    /// </summary>
    public class AppStarter
    {
        /// <summary>
        /// 系统框架默认的配置文件地址
        /// </summary>
        private const string DefaultConfig = "~/DynamicWebConfig.cs";

        /// <summary>
        /// 初始化，初始化的时候做了如下事情(请注意必须顺序，全局配置必须在其他前面，业务注册服务需要依赖配置)
        /// 1.先初始化外部配置文件(如果指定了apiConfigPhysicalFilePath参数，否则会自动搜索根目录下面的~/DynamicWebConfig.cs文件)
        /// 2.注册系统所有服务
        /// 3.MVC创建Controller容器更改
        /// </summary>
        /// <param name="configFilePath">~/开头或者实际的物理路径G:\sharpsword</param>
        public static void Initialize(string configFilePath = "")
        {
            //0.注册全局配置信息(必须在最前注册)
            if (configFilePath.IsNullOrEmpty())
            {
                //映射成物理路径
                var defaultApiConfigPhysicalFilePath = HostHelper.MapPath(DefaultConfig);
                //未指定接口框架配置文件，系统自动查找根节点，DynamicWebConfig.cs文件
                if (File.Exists(defaultApiConfigPhysicalFilePath))
                {
                    configFilePath = defaultApiConfigPhysicalFilePath;
                }
            }
            //检测是否以~/开头，就进行映射成物理路径
            else
            {
                configFilePath = HostHelper.MapPath(configFilePath);
            }

            //进行配置注册
            if (!configFilePath.IsNullOrEmpty())
            {
                DynamicCompiledDependencyRegistarManager.Registar(configFilePath);
            }

            //HOST域名未配置
            if (GlobalConfiguration.Instance.HttpHost.IsNullOrEmpty())
            {
                GlobalConfiguration.Instance.HttpHost = string.Empty;
            }
            else
            {
                //外部配置的httpHost信息
                string httpHost = GlobalConfiguration.Instance.HttpHost;
                string httpStart = "http://", httpsStart = "https://";

                //去除 http:// 或者 https:// 前缀
                if (httpHost.StartsWith(httpStart, StringComparison.InvariantCultureIgnoreCase))
                {
                    GlobalConfiguration.Instance.HttpHost = httpHost.Substring(7, httpHost.Length - 7);
                }
                if (httpHost.StartsWith(httpsStart, StringComparison.InvariantCultureIgnoreCase))
                {
                    GlobalConfiguration.Instance.HttpHost = httpHost.Substring(8, httpHost.Length - 8);
                }

                //去除URL虚拟路径
                httpHost = GlobalConfiguration.Instance.HttpHost;
                int indexOf = httpHost.IndexOf("/", StringComparison.Ordinal);
                GlobalConfiguration.Instance.HttpHost = indexOf != -1 ? httpHost.Substring(0, indexOf) : httpHost;
            }

            //1.注册系统所有服务
            var container = ServicesContainer.Current.Initialize(GlobalConfiguration.Instance);

            //初始化一下
            ServicesContainer.Current.Resolve<INotificationDefinitionManager>()
                                     .As<NotificationDefinitionManager>()
                                     .Initialize();

            //2.类型查找器
            var typeFinder = ServicesContainer.Current.Resolve<ITypeFinder>();

            //3.将实现IStartUp实现类，初始化一下
            StartUpManager.Start(typeFinder);

            //4.发布注册路由
            ServicesContainer.Current.Resolve<IRoutePublisher>().RegisterRoutes(RouteTable.Routes);

            //5.覆盖MVC框架自带controller创建器
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //6.启动所有作业任务
            TaskThreadManager.Instance.Initialize(typeFinder);
            TaskThreadManager.Instance.Start();
        }
    }
}