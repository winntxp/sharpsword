/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/12 9:45:13
 * ****************************************************************/
using System;

namespace SharpSword.ViewEngine
{
    /// <summary>
    /// 模板页面参数对象
    /// </summary>
    public class ViewParameter
    {
        /// <summary>
        /// 参数类型
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// View页面的参数名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// 添加视图页面参数；参数值为null
        /// </summary>
        /// <param name="paramName">参数名称</param>
        public ViewParameter(string paramName)
            : this(paramName, null)
        {
        }

        /// <summary>
        /// 设置模板参数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="paramValue">参数值</param>
        public ViewParameter(string paramName, object paramValue)
        {
            Type = paramValue.GetType();
            Name = paramName;
            Value = paramValue;
        }
    }
}
