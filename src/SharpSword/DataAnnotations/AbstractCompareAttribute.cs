/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/29 13:06:27
 * ****************************************************************/
using SharpSword;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// 比较特性抽象基类
    /// </summary>
    public abstract class AbstractCompareAttribute : ValidationAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">属性值必须大于此值</param>
        protected AbstractCompareAttribute(object value)
        {
            this.Value = value as IComparable;
        }

        /// <summary>
        /// 设置的值，只要是实现了 IComparable 接口即可
        /// </summary>
        public IComparable Value { get; private set; }

        /// <summary>
        /// 比较类型
        /// </summary>
        protected abstract CompareType CompareType { get; }

        /// <summary>
        /// 确定对象的指定值是否有效
        /// </summary>
        /// <param name="value">要验证的对象的值</param>
        /// <param name="validationContext">描述执行验证检查的上下文</param>
        /// <exception cref="SharpSwordCoreException">数据转换失败</exception>
        /// <returns>如果指定的值有效，则为 true；否则，为 false。</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //是否是可比较的类型
            if (!(value is IComparable) || !(this.Value is IComparable))
            {
                return ValidationResult.Success;
            }

            //获取当前属性的类型
            var memberProperty = validationContext.ObjectInstance.GetType().GetProperty(validationContext.MemberName);

            //待转换成的数据类型
            Type convertType = memberProperty.PropertyType;

            //判断下映射实体属性是否是可空类型;是空类型需要特殊处理
            if (memberProperty.PropertyType.IsGenericType && memberProperty.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                NullableConverter nullableConverter = new NullableConverter(memberProperty.PropertyType);
                convertType = nullableConverter.UnderlyingType;
            }

            //验证失败的错误消息
            var validationResult = new ValidationResult(this.ErrorMessage.With(this.Value), new string[] { validationContext.MemberName });

            try
            {
                //检测当前value是否可以转型成
                var obj = Convert.ChangeType(this.Value, convertType);
                var result = ((IComparable)value).CompareTo(obj);

                if (this.CompareType == CompareType.GreaterThan)
                {
                    return result > 0 ? ValidationResult.Success : validationResult;
                }
                if (this.CompareType == CompareType.GreaterThanOrEqual)
                {
                    return result > 0 || result == 0 ? ValidationResult.Success : validationResult;
                }
                if (this.CompareType == CompareType.LessThan)
                {
                    return result < 0 ? ValidationResult.Success : validationResult;
                }
                if (this.CompareType == CompareType.LessThanOrEqual)
                {
                    return result < 0 || result == 0 ? ValidationResult.Success : validationResult;
                }
            }
            catch (Exception ex)
            {
                throw new SharpSwordCoreException(ex.Message);
            }

            //其他全部返回校验成功
            return ValidationResult.Success;
        }
    }
}
