/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/20 15:55:42
 * ****************************************************************/
using System;
using System.Collections.Generic;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 演示数据配置信息
    /// </summary>
    public class DefaultValueItemConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyType">属性类型</param>
        /// <param name="value">默认演示值</param>
        /// <param name="propertyNames">那些属性名称可以使用</param>
        public DefaultValueItemConfig(Type propertyType, dynamic value, params string[] propertyNames)
        {
            propertyType.CheckNullThrowArgumentNullException(nameof(propertyType));
            this.PropertyType = propertyType;
            this.Value = value;
            this.PropertyNames = propertyNames.IsNull() ? new List<string>().ToArray() : propertyNames;
        }

        /// <summary>
        /// 属性类型
        /// </summary>
        public Type PropertyType { get; set; }

        /// <summary>
        /// 公用value值的属性名称，不区分大小写
        /// </summary>
        public string[] PropertyNames { get; set; }

        /// <summary>
        /// 默认给予的演示值
        /// </summary>
        public dynamic Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "{0}`{1}`{2}".With(this.PropertyType.Name, string.Join(",", this.PropertyNames), (object)Value);
        }

    }
}
