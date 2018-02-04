/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/18 9:02:06
 * ****************************************************************/
using System;
using System.Reflection;
using System.Collections.Generic;
using SharpSword.WebApi.ValueProviders;
using SharpSword.WebApi.ValueProviders.Impl;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 上送参数对象绑定器
    /// </summary>
    internal class DefaultRequestParamsBinder : IRequestParamsBinder
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IValueProvidersManager _valueProviderManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueProviderManager">值提供器管理器</param>
        public DefaultRequestParamsBinder(IValueProvidersManager valueProviderManager)
        {
            this._valueProviderManager = valueProviderManager;
        }

        /// <summary>
        /// 获取接口框架上送参数对象
        /// </summary>
        /// <returns></returns>
        public RequestParams GetRequestParams()
        {
            //return new DefaultModelBinder().Bind<RequestParams>(this._valueProviderManager);
            return DefaultModelBinder.Instance.Bind<RequestParams>(this._valueProviderManager);
        }
    }
}
