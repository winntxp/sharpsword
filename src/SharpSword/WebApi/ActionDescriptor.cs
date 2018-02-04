/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/26 9:31:22
 * ****************************************************************/
using System;
using System.Collections.Generic;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 具体实现已移到ReflectedActionDescriptor类实现
    /// </summary>
    [Serializable]
    public class ActionDescriptor : IActionDescriptor, IEquatable<ActionDescriptor>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IEnumerable<IActionFilter> _actionFilterAttributes;
        private readonly IEnumerable<IAuthentication> _actionAuthenticationBaseAttributes;
        private readonly IActionDescriptor _actionDescriptor;

        /// <summary>
        /// 接口描述对象
        /// </summary>
        /// <param name="actionDescriptor">接口描述对象</param>
        public ActionDescriptor(IActionDescriptor actionDescriptor)
        {
            //不能为null,直接抛出异常
            actionDescriptor.CheckNullThrowArgumentNullException(nameof(actionDescriptor));
            actionDescriptor.ActionType.CheckNullThrowArgumentNullException(nameof(actionDescriptor.ActionType));

            this._actionDescriptor = actionDescriptor;

            this.ActionType = actionDescriptor.ActionType;
            this._actionFilterAttributes = actionDescriptor.ActionFilters;
            this.HttpMethod = actionDescriptor.HttpMethod;
            this.RequireHttps = actionDescriptor.RequireHttps;
            this.IsObsolete = actionDescriptor.IsObsolete;
            this.Version = actionDescriptor.Version;
            this.Description = actionDescriptor.Description;
            this.GroupName = actionDescriptor.GroupName;
            this.AuthorName = actionDescriptor.AuthorName;
            this.Cache = actionDescriptor.Cache;
            this.Route = actionDescriptor.Route;
            this.UnloadCacheKeys = actionDescriptor.UnloadCacheKeys;
            this._actionAuthenticationBaseAttributes = actionDescriptor.Authentications;
            this.RequiredUserIdAndUserName = actionDescriptor.RequiredUserIdAndUserName;
            this.EnableAjaxRequest = actionDescriptor.EnableAjaxRequest;
            this.EnableRecordApiLog = actionDescriptor.EnableRecordApiLog;
            this.CanPackageToSdk = actionDescriptor.CanPackageToSdk;
            this.AllowAnonymous = actionDescriptor.AllowAnonymous;
            this.ActionName = actionDescriptor.ActionName;
            this.DataSignatureTransmission = actionDescriptor.DataSignatureTransmission;
            this.ResponseFormat = actionDescriptor.ResponseFormat;
        }

        /// <summary>
        /// 接口类型
        /// </summary>
        public Type ActionType { get; private set; }

        /// <summary>
        /// 上送参数类型
        /// </summary>
        public Type RequestDtoType
        {
            get { return this.ActionType.BaseType.GetGenericArguments()[0]; }
        }

        /// <summary>
        /// 下送数据对象类型
        /// </summary>
        public Type ResponseDtoType
        {
            get { return this.ActionType.BaseType.GetGenericArguments()[1]; }
        }

        /// <summary>
        /// 接口定义的所有特性筛选器
        /// </summary>
        public IEnumerable<IActionFilter> ActionFilters
        {
            get
            {
                return this._actionFilterAttributes.IsNull() ? new List<IActionFilter>() : this._actionFilterAttributes;
            }
        }

        /// <summary>
        /// 获取接口校验，签名
        /// </summary>
        public IEnumerable<IAuthentication> Authentications
        {
            get
            {
                return this._actionAuthenticationBaseAttributes.IsNull() ?
                    new List<IAuthentication>() : this._actionAuthenticationBaseAttributes;
            }
        }

        /// <summary>
        /// 允许的请求类型比如：POST,GET
        /// </summary>
        public HttpMethod HttpMethod { get; private set; }

        /// <summary>
        /// 是否需要https请求
        /// </summary>
        public bool RequireHttps { get; private set; }

        /// <summary>
        /// 接口是否过期或者取消
        /// </summary>
        public bool IsObsolete { get; private set; }

        /// <summary>
        /// 是否无需校验身份
        /// </summary>
        public bool AllowAnonymous { get; private set; }

        /// <summary>
        /// 接口版本
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// 接口描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName { get; private set; }

        /// <summary>
        /// 接口作者
        /// </summary>
        public string AuthorName { get; private set; }

        /// <summary>
        /// 接口缓存特性
        /// </summary>
        public ResultCacheAttribute Cache { get; private set; }

        /// <summary>
        /// 特性路由器
        /// </summary>
        public RouteAttribute Route { get; private set; }

        /// <summary>
        /// 待移除的缓存匹配键
        /// </summary>
        public string[] UnloadCacheKeys { get; private set; }

        /// <summary>
        /// 是否需要校验上送的UserId和UserName
        /// </summary>
        public bool RequiredUserIdAndUserName { get; private set; }

        /// <summary>
        /// 是否允许AJAX请求
        /// </summary>
        public bool EnableAjaxRequest { get; private set; }

        /// <summary>
        /// 是否允许记录日志
        /// </summary>
        public bool EnableRecordApiLog { get; private set; }

        /// <summary>
        /// 是否允许打包到SDK
        /// </summary>
        public bool CanPackageToSdk { get; private set; }

        /// <summary>
        /// 是否走IApiSecurity加解密流程
        /// </summary>
        public bool DataSignatureTransmission { get; private set; }

        /// <summary>
        /// 接口名称
        /// </summary>
        public string ActionName { get; private set; }

        /// <summary>
        /// 允许输出格式
        /// </summary>
        public ResponseFormat ResponseFormat { get; private set; }

        /// <summary>
        /// 重写接口是否相等(接口名称+接口版本一致就认定接口描述一致)
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ActionDescriptor other)
        {
            return !other.IsNull() && this.ActionName.Equals(other.ActionName) && this.Version.Equals(other.Version);
        }

        /// <summary>
        /// 重写下ToString()，返回定制的信息
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "{0}-{1}".With(this.ActionName, this.Version);
        }

        /// <summary>
        /// 获取自定义的特性属性集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetCustomAttributes<T>() where T : Attribute
        {
            return this._actionDescriptor.GetCustomAttributes<T>();
        }
    }
}
