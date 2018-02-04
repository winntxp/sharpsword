/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/20 11:05:36
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword
{
    /// <summary>
    /// 根据字符串或者匿名对象，获取名值对字典信息
    /// </summary>
    internal class SeparatorStringParse
    {
        /// <summary>
        /// 保存字典信息
        /// </summary>
        private readonly Dictionary<string, object> _attributes = new Dictionary<string, object>();

        /// <summary>
        /// 字符串：UserId=986351;Account=dillys2013;Password=512FC49E43DE9209015318CE63B09A0A2184 拆分，改造成字典
        /// </summary>
        /// <param name="configString">输入字符串必须为，UserId=986351;Account=dillys2013;Password=512FC49E43DE9209015318CE63B09A0A2184</param>
        public SeparatorStringParse(string configString)
        {
            if (configString.IsNullOrEmpty())
            {
                return;
            }
            var configStrings = configString.Split(new char[] { ';' });
            foreach (string c in configStrings)
            {
                var items = c.Split(new char[] { '=' }, System.StringSplitOptions.RemoveEmptyEntries);
                if (!items.IsNull() && items.Length == 2)
                {
                    this._attributes.Add(items[0].Trim(), items[1].Trim());
                }
            }
        }

        /// <summary>
        /// 获取属性名值对
        /// </summary>
        public Dictionary<string, object> Attributes
        {
            get
            {
                return this._attributes;
            }
        }
    }
}
