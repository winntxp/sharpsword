/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/2/2015 8:32:16 PM
 * ****************************************************************/
using System;
using System.Collections.Generic;

namespace SharpSword.SDK
{
    /// <summary>
    /// 排序字典类，便于签名
    /// </summary>
    internal class ApiDictionary : SortedDictionary<string, string>
    {
        /// <summary>
        /// 
        /// </summary>
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 
        /// </summary>
        public ApiDictionary() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary"></param>
        public ApiDictionary(IDictionary<string, string> dictionary)
            : base(dictionary)
        { }

        /// <summary>
        /// 添加一个新的键值对。空键或者空值的键值对将会被忽略。
        /// </summary>
        /// <param name="key">键名称</param>
        /// <param name="value">键对应的值，目前支持：string, int, long, double, bool, DateTime类型</param>
        public void Add(string key, object value)
        {
            string strValue;

            if (value == null)
            {
                strValue = null;
            }
            else if (value is string)
            {
                strValue = (string)value;
            }
            else if (value is DateTime?)
            {
                DateTime? dateTime = value as DateTime?;
                strValue = dateTime.Value.ToString(DateTimeFormat);
            }
            else if (value is int?)
            {
                strValue = (value as int?).Value.ToString();
            }
            else if (value is long?)
            {
                strValue = (value as long?).Value.ToString();
            }
            else if (value is double?)
            {
                strValue = (value as double?).Value.ToString();
            }
            else if (value is bool?)
            {
                strValue = (value as Nullable<bool>).Value.ToString().ToLower();
            }
            else
            {
                strValue = value.ToString();
            }

            this.Add(key, strValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public new void Add(string key, string value)
        {
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
            {
                base.Add(key, value);
            }
        }
    }
}
