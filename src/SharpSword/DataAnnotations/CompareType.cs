/* *******************************************************
 * SharpSword zhangliang4629@163.com 10/21/2016 2:18:21 PM
 * ****************************************************************/

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// 比较类型，属性比较特性的时候用到
    /// </summary>
    public enum CompareType
    {
        /// <summary>
        /// 大于
        /// </summary>
        GreaterThan,

        /// <summary>
        /// 小于
        /// </summary>
        LessThan,

        /// <summary>
        /// 小于等于
        /// </summary>
        LessThanOrEqual,

        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterThanOrEqual
    }
}
