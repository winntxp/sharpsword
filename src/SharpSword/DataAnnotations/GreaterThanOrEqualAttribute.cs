/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/29 13:06:27
 * ****************************************************************/
using SharpSword.Resource;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// 大于等于指定值
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class GreaterThanOrEqualAttribute : AbstractCompareAttribute
    {
        /// <summary>
        /// 比较类型
        /// </summary>
        protected override CompareType CompareType
        {
            get
            {
                return CompareType.GreaterThanOrEqual;
            }
        }

        /// <summary>
        /// 设置的值，只要是实现了 IComparable 接口即可
        /// </summary>
        /// <param name="value">属性值必须大于此值</param>
        public GreaterThanOrEqualAttribute(object value) : base(value)
        {
            this.ErrorMessage = CoreResource.GreaterThanOrEqualAttribute_Error;
        }
    }
}
