/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/5/31 9:13:20
 * ****************************************************************/
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.WebApi.ValueProviders
{
    /// <summary>
    /// 默认的值提供器管理器
    /// </summary>
    public class DefaultValueProvidersManager : IValueProvidersManager
    {
        /// <summary>
        /// 保存值提供器集合
        /// </summary>
        private readonly IList<IValueProvider> _valueProviders = new List<IValueProvider>();

        /// <summary>
        /// 使用值提供器集合来构造一个值提供器管理器
        /// </summary>
        /// <param name="valueProviders">值提供器集合</param>
        public DefaultValueProvidersManager(IEnumerable<IValueProvider> valueProviders)
        {
            valueProviders.CheckNullThrowArgumentNullException(nameof(valueProviders));
            //安装值提供器优先级（在多个值提供器提供相同键的情况下，优先级高的先取出，后续的值提供器将不会取值）
            foreach (var item in valueProviders.OrderByDescending(o => o.Order))
            {
                this._valueProviders.Add(item);
            }
        }

        /// <summary>
        /// 获取所有注册的值提供器
        /// </summary>
        public IEnumerable<IValueProvider> ValueProviders
        {
            get { return this._valueProviders; }
        }

        /// <summary>
        /// 获取所有值提供器的键
        /// </summary>
        /// <returns>返回邮件值提供器的键集合</returns>
        public IEnumerable<string> GetAllKeys()
        {
            return this._valueProviders.SelectMany(item => item.GetAllKeys()).ToList();
        }

        /// <summary>
        /// 根据键获取值提供器对应的值
        /// </summary>
        /// <param name="propertyName">键名称</param>
        /// <returns></returns>
        public object GetValue(string propertyName)
        {
            return this._valueProviders.Select(item => item.GetValue(propertyName)).FirstOrDefault(value => !value.IsNull());
        }

    }
}
