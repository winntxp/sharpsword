/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/26 11:31:48
 * ****************************************************************/
using System;
using System.Collections.Generic;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 获取接口描述信息
    /// </summary>
    public interface IActionDescriptor
    {
        /// <summary>
        /// 接口名称
        /// </summary>
        string ActionName { get; }

        /// <summary>
        /// 接口缓存特性
        /// </summary>
        ResultCacheAttribute Cache { get; }

        /// <summary>
        /// 接口类型
        /// </summary>
        Type ActionType { get; }

        /// <summary>
        /// 上送参数类型
        /// </summary>
        Type RequestDtoType { get; }

        /// <summary>
        /// 下送对象类型
        /// </summary>
        Type ResponseDtoType { get; }

        /// <summary>
        /// 接口作者
        /// </summary>
        string AuthorName { get; }

        /// <summary>
        /// 接口描述
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 接口分组名称
        /// </summary>
        string GroupName { get; }

        /// <summary>
        /// 接口是否允许AJAX访问
        /// </summary>
        bool EnableAjaxRequest { get; }

        /// <summary>
        /// 是否允许记录此接口访问记录
        /// </summary>
        bool EnableRecordApiLog { get; }

        /// <summary>
        /// 是否允许打包到SDK
        /// </summary>
        bool CanPackageToSdk { get; }

        /// <summary>
        /// 接口筛选器集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<IActionFilter> ActionFilters { get; }

        /// <summary>
        /// 获取接口授权，签名校验
        /// </summary>
        IEnumerable<IAuthentication> Authentications { get; }

        /// <summary>
        /// 特性路由器
        /// </summary>
        RouteAttribute Route { get; }

        /// <summary>
        /// 允许请求接口类型；POST,GET
        /// </summary>
        HttpMethod HttpMethod { get; }

        /// <summary>
        /// 接口是否过期
        /// </summary>
        bool IsObsolete { get; }

        /// <summary>
        /// 是否不需要校验用户
        /// </summary>
        bool AllowAnonymous { get; }

        /// <summary>
        /// 是否需要校验上送的UserId和UserName
        /// </summary>
        bool RequiredUserIdAndUserName { get; }

        /// <summary>
        /// 是否需要https请求
        /// </summary>
        bool RequireHttps { get; }

        /// <summary>
        /// 接口版本
        /// </summary>
        string Version { get; }

        /// <summary>
        /// 是否走IApiSecurity加解密流程
        /// </summary>
        bool DataSignatureTransmission { get; }

        /// <summary>
        /// 自动移除的缓存匹配键
        /// </summary>
        string[] UnloadCacheKeys { get; }

        /// <summary>
        /// 接口允许输出那种格式
        /// </summary>
        ResponseFormat ResponseFormat { get; }

        /// <summary>
        /// 获取自定义的特性属性集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetCustomAttributes<T>() where T : Attribute;
    }
}
