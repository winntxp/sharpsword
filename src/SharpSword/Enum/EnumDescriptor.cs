/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/28 23:12:03
 * ****************************************************************/

namespace SharpSword
{
    /// <summary>
    /// 枚举描述对象
    /// </summary>
    public class EnumDescriptor
    {
        /// <summary>
        /// 枚举数字值
        /// </summary>
        public int Key { get; set; }

        /// <summary>
        /// 枚举字字符串值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 枚举描述对象（对应于枚举特性标签System.ComponentModel.Description）
        /// </summary>
        public string Description { get; set; }

    }
}
