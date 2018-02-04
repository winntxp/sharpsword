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
    /// 基于web form input 的值提供其
    /// </summary>
    internal class FormValueProvider : ValueProviderBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IDictionary<string, object> _valueDictionary =
            new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Form表单值提供器
        /// </summary>
        /// <param name="httpContext"></param>
        public FormValueProvider(HttpContextBase httpContext)
        {
            httpContext.CheckNullThrowArgumentNullException(nameof(httpContext));
            httpContext.Request.Unvalidated.Form.AllKeys.ToList().ForEach(key =>
            {
                if (!key.IsNullOrEmpty() && !_valueDictionary.ContainsKey(key))
                {
                    this._valueDictionary.Add(key, httpContext.Request.Unvalidated.Form[key]);
                }
            });
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
        /// 
        /// </summary>
        public override int Order
        {
            get { return 1; }
        }
    }
}