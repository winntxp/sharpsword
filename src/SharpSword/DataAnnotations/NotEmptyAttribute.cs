using SharpSword;
using SharpSword.Resource;
/******************************************************************
* SharpSword zhangliang@sharpsword.com.cn 2016/5/6 14:57:31
* *****************************************************************/
using System.Collections;
using System.Linq;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// 集合元素不能为空(即：元素必须大于1个)-字符串或者集合类型适用
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class NotEmptyAttribute : ValidationAttribute
    {
        /// <summary>
        /// 确定对象的指定值是否有效
        /// </summary>
        /// <param name="value">要验证的对象的值</param>
        /// <param name="validationContext">描述执行验证检查的上下文</param>
        /// <returns>如果指定的值有效，则为 true；否则，为 false。</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //转换成可枚举类型
            IEnumerable collection = value as IEnumerable;

            if (collection.IsNull())
            {
                return ValidationResult.Success;
            }

            //大于0，校验通过
            return collection.Cast<object>().Any() ?
                ValidationResult.Success : new ValidationResult(CoreResource.NotEmptyAttribute_Error, new string[] { validationContext.MemberName });
        }
    }
}
