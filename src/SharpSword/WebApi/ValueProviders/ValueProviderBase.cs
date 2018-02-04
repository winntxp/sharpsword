/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/22/2016 12:51:11 PM
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.WebApi.ValueProviders
{
    /// <summary>
    /// 基于字符的值提供器基类
    /// </summary>
    public abstract class ValueProviderBase : IValueProvider
    {
        /// <summary>
        /// 获取值字典信息
        /// </summary>
        /// <returns></returns>
        protected abstract IDictionary<string, object> GetValueDictionary();

        /// <summary>
        /// 值提供器优先级
        /// </summary>
        public virtual int Order => 0;

        /// <summary>
        /// 获取所有的键
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetAllKeys()
        {
            return this.GetValueDictionary().Keys;
        }

        /// <summary>
        /// 获取指定键对应的值
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public object GetValue(string propertyName)
        {
            return (from key in this.GetValueDictionary().Keys
                    where key.Equals(propertyName, StringComparison.OrdinalIgnoreCase)
                    select this.GetValueDictionary()[key]).FirstOrDefault();
        }
    }
}
