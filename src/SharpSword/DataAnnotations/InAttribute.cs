/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/29 12:36:19
 * ****************************************************************/
using SharpSword;
using SharpSword.Resource;
using System.Linq;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// 输入的值必须包含在指定的范围值域内，比如：[1,2,3,4]或者["z","x","Y"]
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class InAttribute : ValidationAttribute
    {
        /// <summary>
        /// 值域范围
        /// </summary>
        public object[] Values { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values">值域范围，比如：[1,2,3,4]或者["z","x","Y"]</param>
        public InAttribute(params object[] values)
        {
            values.CheckNullThrowArgumentNullException(nameof(values));
            this.Values = values;
            this.ErrorMessage = CoreResource.InAttribute_Error.With(string.Join(",", values));
        }

        /// <summary>
        /// 确定对象的指定值是否有效
        /// </summary>
        /// <param name="value">要验证的对象的值</param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            return value.IsNull() || this.Values.Contains(value);
        }
    }
}
