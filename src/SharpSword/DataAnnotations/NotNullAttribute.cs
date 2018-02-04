/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/23 17:31:44
 * ****************************************************************/
using SharpSword;
using SharpSword.Resource;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// 可为空的值类型，或者object不能为null，如果需要使字符串类型不为null，请使用RequiredAttribute特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class NotNullAttribute : ValidationAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public NotNullAttribute()
        {
            base.ErrorMessage = CoreResource.NotNullAttribute_Error;
        }

        /// <summary>
        /// 确定对象的指定值是否有效
        /// </summary>
        /// <param name="value">要验证的对象的值</param>
        /// <returns>如果指定的值有效，则为 true；否则，为 false。</returns>
        public override bool IsValid(object value)
        {
            return !value.IsNull();
        }
    }
}
