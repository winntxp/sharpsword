/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/26/2015 3:08:45 PM
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Web;

namespace SharpSword.WebApi.ValueProviders.Impl
{
    /// <summary>
    /// 服务器环境变量值提供器，定义的实体请将-去掉
    /// </summary>
    public class ServerVariablesValueProvider : ValueProviderBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IDictionary<string, object> _valueDictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        public ServerVariablesValueProvider(HttpContextBase httpContext)
        {
            foreach (var key in httpContext.Request.ServerVariables.AllKeys)
            {
                this._valueDictionary.Add(key.Replace("-", string.Empty), httpContext.Request.ServerVariables[key]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IDictionary<string, object> GetValueDictionary()
        {
            return this._valueDictionary;
        }

        /// <summary>
        /// 注册的所有只提供都找不到对应的键值，就获取服务器环境变量
        /// </summary>
        public override int Order
        {
            get { return int.MinValue; }
        }
    }
}