/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharpSword.WebApi.ValueProviders.Impl
{
    /// <summary>
    /// 基于URL查询字符串的值提供其
    /// </summary>
    internal class QueryStringValueProvider : ValueProviderBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IDictionary<string, object> _valueDictionary =
            new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// URL参数值提供器
        /// </summary>
        /// <param name="httpContext"></param>
        public QueryStringValueProvider(HttpContextBase httpContext)
        {
            httpContext.CheckNullThrowArgumentNullException(nameof(httpContext));
            httpContext.Request.Unvalidated.QueryString.AllKeys.ToList().ForEach(key =>
            {
                if (!key.IsNullOrEmpty() && !_valueDictionary.ContainsKey(key))
                {
                    this._valueDictionary.Add(key, httpContext.Request.Unvalidated.QueryString[key]);
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public override int Order
        {
            get { return 0; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IDictionary<string, object> GetValueDictionary()
        {
            return this._valueDictionary;
        }
    }
}