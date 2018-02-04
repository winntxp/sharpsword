/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/15 10:21:38
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 接口配置表扩展
    /// </summary>
    public static class ActionConfigCollectionExtensions
    {
        /// <summary>
        /// 添加一个接口配置，此配置为系统框架级别全局配置，可以多次调用，但是后注册的会覆盖掉前面注册的属性
        /// </summary>
        /// <param name="actionConfigCollection">接口配置表对象</param>
        /// <param name="anonymousObjectActionConfigItemValue">接口配置对象，匿名类型，只要属性名称和ActionConfigItem一致既可</param>
        public static IActionConfigCollection Register(this IActionConfigCollection actionConfigCollection, object anonymousObjectActionConfigItemValue)
        {
            return actionConfigCollection.Register(anonymousObjectActionConfigItemValue.MapTo<ActionConfigItem>());
        }

        /// <summary>
        /// 添加一个接口配置，此配置为单一接口全局配置，可以多次调用，但是后注册的会覆盖掉前面注册的属性
        /// </summary>
        /// <param name="actionConfigCollection">接口配置表对象</param>
        /// <param name="actionName">接口名称，大小写不敏感</param>
        /// <param name="anonymousObjectActionConfigItemValue">>接口配置对象，匿名类型，只要属性名称和ActionConfigItem一致既可</param>
        public static IActionConfigCollection Register(this IActionConfigCollection actionConfigCollection, string actionName, object anonymousObjectActionConfigItemValue)
        {
            return actionConfigCollection.Register(actionName, anonymousObjectActionConfigItemValue.MapTo<ActionConfigItem>());
        }

        /// <summary>
        /// 添加一个接口配置，此接口只对特定版本起作用，会覆盖掉未指定版本号的配置，可以多次调用，但是后注册的会覆盖掉前面注册的属性
        /// </summary>
        /// <param name="actionConfigCollection">接口配置表对象</param>
        /// <param name="actionName">接口名称，大小写不敏感</param>
        /// <param name="version">接口版本，接口版本格式为：1.0</param>
        /// <param name="anonymousObjectActionConfigItemValue">>接口配置对象，匿名类型，只要属性名称和ActionConfigItem一致既可</param>
        public static IActionConfigCollection Register(this IActionConfigCollection actionConfigCollection, string actionName, string version, object anonymousObjectActionConfigItemValue)
        {
            return actionConfigCollection.Register(actionName, version, anonymousObjectActionConfigItemValue.MapTo<ActionConfigItem>());
        }

        /// <summary>
        /// 注销一个接口
        /// </summary>
        /// <param name="actionConfigCollection">接口配置表对象</param>
        /// <param name="actionNames">接口名称，大小写不敏感</param>
        public static IActionConfigCollection Obsolete(this IActionConfigCollection actionConfigCollection, params string[] actionNames)
        {
            actionNames.CheckNullThrowArgumentNullException(nameof(actionNames));
            foreach (var current in actionNames)
            {
                actionConfigCollection.Register(current, new ActionConfigItem() { Obsolete = true });
            }
            return actionConfigCollection;
        }

        /// <summary>
        /// 注销接口
        /// </summary>
        /// <param name="actionConfigCollection">接口配置表对象</param>
        /// <param name="actionName">接口名称，大小写不敏感</param>
        /// <param name="version">接口版本，接口版本格式为：1.0</param>
        /// <returns></returns>
        public static IActionConfigCollection Obsolete(this IActionConfigCollection actionConfigCollection, string actionName, string version)
        {
            return actionConfigCollection.Register(actionName, version, new ActionConfigItem() { Obsolete = true });
        }

        /// <summary>
        /// 接口的特性路由
        /// </summary>
        /// <param name="actionConfigCollection">接口配置表对象</param>
        /// <param name="actionName">接口名称，大小写不敏感</param>
        /// <param name="url">特性路由，不能含有{}字符</param>
        /// <returns></returns>
        public static IActionConfigCollection Route(this IActionConfigCollection actionConfigCollection, string actionName, string url)
        {
            return actionConfigCollection.Register(actionName, new ActionConfigItem() { RouteUrl = url });
        }

        /// <summary>
        /// 禁用所有的系统框架接口
        /// </summary>
        /// <param name="actionConfigCollection">接口配置表对象</param>
        /// <returns></returns>
        public static IActionConfigCollection ObsoleteSystemActions(this IActionConfigCollection actionConfigCollection)
        {
            var actionNames = new[] { "API.Help", "Api.Index" };
            foreach (var current in actionNames)
            {
                actionConfigCollection.Register(current, new ActionConfigItem() { Obsolete = true });
            }
            return actionConfigCollection;
        }

        /// <summary>
        /// 接口需要安全连接 https
        /// </summary>
        /// <param name="actionConfigCollection">接口配置表对象</param>
        /// <param name="actionName">接口名称，大小写不敏感</param>
        public static IActionConfigCollection RequireHttps(this IActionConfigCollection actionConfigCollection, string actionName)
        {
            return actionConfigCollection.Register(actionName, new ActionConfigItem() { RequireHttps = true });
        }

        /// <summary>
        /// 接口需要安全连接 https
        /// </summary>
        /// <param name="actionConfigCollection">接口配置表对象</param>
        /// <param name="actionName">接口名称，大小写不敏感</param>
        /// <param name="version">接口版本，接口版本格式为：1.0</param>
        /// <returns></returns>
        public static IActionConfigCollection RequireHttps(this IActionConfigCollection actionConfigCollection, string actionName, string version)
        {
            return actionConfigCollection.Register(actionName, version, new ActionConfigItem() { RequireHttps = true });
        }

        /// <summary>
        /// 接口设置缓存
        /// </summary>
        /// <param name="actionConfigCollection">接口配置表对象</param>
        /// <param name="actionName">接口名称，大小写不敏感</param>
        /// <param name="cacheTime">缓存时间</param>
        public static IActionConfigCollection Cache(this IActionConfigCollection actionConfigCollection, string actionName, int cacheTime)
        {
            return actionConfigCollection.Register(actionName, new ActionConfigItem() { CacheTime = cacheTime });
        }

        /// <summary>
        /// 接口设置缓存
        /// </summary>
        /// <param name="actionConfigCollection">接口配置表对象</param>
        /// <param name="actionName">接口名称，大小写不敏感</param>
        /// <param name="version">接口版本，接口版本格式为：1.0</param>
        /// <param name="cacheTime"></param>
        /// <returns></returns>
        public static IActionConfigCollection Cache(this IActionConfigCollection actionConfigCollection,
            string actionName, string version, int cacheTime)
        {
            return actionConfigCollection.Register(actionName, version, new ActionConfigItem() { CacheTime = cacheTime });
        }

        /// <summary>
        /// 接口设置缓存
        /// </summary>
        /// <param name="actionConfigCollection">接口配置表对象</param>
        /// <param name="actionName">接口名称，大小写不敏感</param>
        /// <param name="version">接口版本，接口版本格式为：1.0，如果不指定接口请配置为null</param>
        /// <param name="cachePrefix">缓存KEY前缀</param>
        /// <param name="cacheTime">缓存时间</param>
        /// <param name="ignoreUserIdAndUserName">生成的缓存键是否忽略掉操作用户信息</param>
        /// <returns></returns>
        public static IActionConfigCollection Cache(this IActionConfigCollection actionConfigCollection,
            string actionName, string version, string cachePrefix, int cacheTime, bool ignoreUserIdAndUserName)
        {
            return actionConfigCollection.Register(actionName, version,
                new ActionConfigItem()
                {
                    CachePrefix = cachePrefix,
                    CacheTime = cacheTime,
                    CacheKeyIgnoreUserIdAndUserName = ignoreUserIdAndUserName
                });
        }

        /// <summary>
        /// 在操作结束后会自动移除缓存键
        /// </summary>
        /// <param name="actionConfigCollection">接口配置表对象</param>
        /// <param name="actionName">接口名称，大小写不敏感</param>
        /// <param name="version">接口版本，接口版本格式为：1.0，如果不指定接口请配置为null</param>
        /// <param name="cachePrefixs">缓存KEY前缀</param>
        /// <returns></returns>
        public static IActionConfigCollection UnloadCache(this IActionConfigCollection actionConfigCollection,
            string actionName, string version, string[] cachePrefixs)
        {
            return actionConfigCollection.Register(actionName, version, new ActionConfigItem()
            {
                UnloadCacheKeys = cachePrefixs
            });
        }

        /// <summary>
        /// 记录接口日志
        /// </summary>
        /// <param name="actionConfigCollection">接口配置表对象</param>
        /// <param name="actionName">接口名称，大小写不敏感</param>
        public static IActionConfigCollection EnableRecordApiLog(this IActionConfigCollection actionConfigCollection, string actionName)
        {
            return actionConfigCollection.Register(actionName, new ActionConfigItem() { EnableRecordApiLog = true });
        }

        /// <summary>
        /// 记录接口日志
        /// </summary>
        /// <param name="actionConfigCollection">接口配置表对象</param>
        /// <param name="actionName">接口名称，大小写不敏感</param>
        /// <param name="version">接口版本，接口版本格式为：1.0</param>
        /// <returns></returns>
        public static IActionConfigCollection EnableRecordApiLog(this IActionConfigCollection actionConfigCollection, string actionName, string version)
        {
            return actionConfigCollection.Register(actionName, version, new ActionConfigItem() { EnableRecordApiLog = true });
        }

        /// <summary>
        /// 需要走加密传输流程
        /// </summary>
        /// <param name="actionConfigCollection">接口配置表对象</param>
        /// <param name="actionName">接口名称，大小写不敏感</param>
        public static IActionConfigCollection DataSignatureTransmission(this IActionConfigCollection actionConfigCollection, string actionName)
        {
            return actionConfigCollection.Register(actionName, new ActionConfigItem() { DataSignatureTransmission = true });
        }

        /// <summary>
        /// 需要走加密传输流程
        /// </summary>
        /// <param name="actionConfigCollection">接口配置表对象</param>
        /// <param name="actionName">接口名称，大小写不敏感</param>
        /// <param name="version">接口版本，接口版本格式为：1.0</param>
        /// <returns></returns>
        public static IActionConfigCollection DataSignatureTransmission(this IActionConfigCollection actionConfigCollection, string actionName, string version)
        {
            return actionConfigCollection.Register(actionName, version, new ActionConfigItem() { DataSignatureTransmission = true });
        }

        /// <summary>
        /// 允许匿名访问，即：不走授权接口校验流程
        /// </summary>
        /// <param name="actionConfigCollection">接口配置表对象</param>
        /// <param name="actionName">接口名称，大小写不敏感</param>
        public static IActionConfigCollection AllowAnonymous(this IActionConfigCollection actionConfigCollection, string actionName)
        {
            return actionConfigCollection.Register(actionName, new ActionConfigItem() { AllowAnonymous = true });
        }

        /// <summary>
        /// 允许匿名访问，即：不走授权接口校验流程
        /// </summary>
        /// <param name="actionConfigCollection">接口配置表对象</param>
        /// <param name="actionName">接口名称，大小写不敏感</param>
        /// <param name="version">接口版本，接口版本格式为：1.0</param>
        /// <returns></returns>
        public static IActionConfigCollection AllowAnonymous(this IActionConfigCollection actionConfigCollection, string actionName, string version)
        {
            return actionConfigCollection.Register(actionName, version, new ActionConfigItem() { AllowAnonymous = true });
        }

        /// <summary>
        /// 不打包到SDK
        /// </summary>
        /// <param name="actionConfigCollection">接口配置表对象</param>
        /// <param name="actionName">接口名称，大小写不敏感</param>
        public static IActionConfigCollection DisablePackageSdk(this IActionConfigCollection actionConfigCollection, string actionName)
        {
            return actionConfigCollection.Register(actionName, new ActionConfigItem() { CanPackageToSdk = false });
        }

        /// <summary>
        /// 不打包到SDK
        /// </summary>
        /// <param name="actionConfigCollection">接口配置表对象</param>
        /// <param name="actionName">接口名称，大小写不敏感</param>
        /// <param name="version">接口版本，接口版本格式为：1.0</param>
        /// <returns></returns>
        public static IActionConfigCollection DisablePackageSdk(this IActionConfigCollection actionConfigCollection, string actionName, string version)
        {
            return actionConfigCollection.Register(actionName, version, new ActionConfigItem() { CanPackageToSdk = false });
        }

        /// <summary>
        /// 设置接口分组
        /// </summary>
        /// <param name="actionConfigCollection">接口配置表对象</param>
        /// <param name="actionName">接口名称，大小写不敏感</param>
        /// <param name="groupName">分组名称</param>
        public static IActionConfigCollection Group(this IActionConfigCollection actionConfigCollection, string actionName, string groupName)
        {
            return actionConfigCollection.Register(actionName, new ActionConfigItem() { GroupName = groupName });
        }
    }
}
