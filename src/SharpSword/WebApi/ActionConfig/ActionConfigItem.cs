/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/23 10:46:35
 * ****************************************************************/
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 接口外部配置映射对象
    /// </summary>
    [Serializable]
    public class ActionConfigItem
    {
        /// <summary>
        /// 缓存键分组前缀(用于批量缓存清理)
        /// </summary>
        public string CachePrefix { get; set; }

        /// <summary>
        /// 缓存时间,系统默认:0分钟
        /// </summary>
        public int? CacheTime { get; set; }

        /// <summary>
        /// 缓存是否忽略掉点操作用户信息(系统默认:true)
        /// </summary>
        public bool? CacheKeyIgnoreUserIdAndUserName { get; set; }

        /// <summary>
        /// 需要卸载的缓存键(即接口执行完成后，会自动进行缓存键的清理，如果未配置则不清理)
        /// 如缓存键安装正则匹配模式进行删除，即所有匹配成功的缓存键，都不会删除
        /// </summary>
        public string[] UnloadCacheKeys { get; set; }

        /// <summary>
        /// 是否需要开启https连接才能请求
        /// </summary>
        public bool? RequireHttps { get; set; }

        /// <summary>
        /// 接口是否过期（下线，外部不能访问此即可，搜索接口的时候也会排除掉此接口）
        /// </summary>
        public bool? Obsolete { get; set; }

        /// <summary>
        /// 是否允许AJAX访问
        /// </summary>
        public bool? EnableAjaxRequest { get; set; }

        /// <summary>
        /// 允许的请求访问方式，可以组合配置，如：HttpMethod.POST | HttpMethod.GET
        /// </summary>
        public HttpMethod? HttpMethod { get; set; }

        /// <summary>
        /// 是否允许匿名访问（即全局身份校验对此接口不起作用）
        /// </summary>
        public bool? AllowAnonymous { get; set; }

        /// <summary>
        /// 是否允许记录日志（记录器不记录此接口日志访问）
        /// </summary>
        public bool? EnableRecordApiLog { get; set; }

        /// <summary>
        /// 是否允许自动打包生成SDK访问类
        /// </summary>
        public bool? CanPackageToSdk { get; set; }

        /// <summary>
        /// 接口是否走IApiSecurity加解密流程；系统默认走加解密流程(true)
        /// </summary>
        public bool? DataSignatureTransmission { get; set; }

        /// <summary>
        /// 分组名称（方便接口归类显示）
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 接口特性路由
        /// </summary>
        public string RouteUrl { get; set; }
    }
}