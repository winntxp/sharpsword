/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/1/16 15:56:08
 * ****************************************************************/
using System;

namespace SharpSword.WebApi.ValueProviders
{
    /// <summary>
    /// 值提供器扩展类
    /// </summary>
    public static class ValueProviderExtensions
    {
        /// <summary>
        /// 获取值提供器提供的值，转换成基元类型数据，比如：int 或者int? 或者string 等等
        /// </summary>
        /// <param name="valueProvider">值提供器</param>
        /// <param name="key">key</param>
        /// <param name="defaultFactory">当key返回null的时候，返回默认的指定值，委托入参为：key值</param>
        /// <returns></returns>
        public static T GetValue<T>(this IValueProvider valueProvider, string key, Func<string, T> defaultFactory)
        {
            valueProvider.CheckNullThrowArgumentNullException(nameof(valueProvider));

            //当前的值提供器
            var value = valueProvider.GetValue(key);

            //值为null，直接使用默认的委托返回数据
            if (value.IsNull())
            {
                return defaultFactory.IsNull() ? default(T) : defaultFactory(key);
            }

            //返回数据
            return value.ToString().As<T>();
        }
    }
}
