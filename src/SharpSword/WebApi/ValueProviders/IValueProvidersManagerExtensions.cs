/* *******************************************************
 * SharpSword zhangliang4629@163.com 8/16/2016 5:49:54 PM
 * ****************************************************************/

namespace SharpSword.WebApi.ValueProviders
{
    /// <summary>
    /// 值提供器管理器扩展
    /// </summary>
    public static class IValueProvidersManagerExtensions
    {
        /// <summary>
        /// 获取指定的值，如果只不存在，就直接抛出异常
        /// </summary>
        /// <param name="valueProvidersManager">值提供器管理器</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>object value</returns>
        /// <exception cref="SharpSwordCoreException">指定键的值为null，会直接抛出异常</exception>
        public static object GetRequiredValue(this IValueProvidersManager valueProvidersManager, string propertyName)
        {
            var value = valueProvidersManager.GetValue(propertyName);
            if (value.IsNull())
            {
                throw new SharpSwordCoreException("propertyName value not exists");
            }
            return value;
        }

        /// <summary>
        /// 尝试读取值，读取到，返回true
        /// </summary>
        /// <param name="valueProvidersManager">值提供器管理器</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="value">返回值</param>
        /// <returns>true/false</returns>
        public static bool TryGetValue(this IValueProvidersManager valueProvidersManager, string propertyName, out object value)
        {
            value = valueProvidersManager.GetValue(propertyName);
            return !value.IsNull();
        }
    }
}
