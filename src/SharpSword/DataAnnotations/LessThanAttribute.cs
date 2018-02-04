/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/29 13:06:27
 * ****************************************************************/
using SharpSword.Resource;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// 小于指定值
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class LessThanAttribute : AbstractCompareAttribute
    {
        /// <summary>
        /// 比较类型
        /// </summary>
        protected override CompareType CompareType
        {
            get
            {
                return CompareType.LessThan;
            }
        }

        /// <summary>
        /// 设置的值，只要是实现了 IComparable 接口即可
        /// </summary>
        /// <param name="value">属性值必须小于此值</param>
        public LessThanAttribute(object value) : base(value)
        {
            this.ErrorMessage = CoreResource.LessThanAttribute_Error;
        }
    }
}
