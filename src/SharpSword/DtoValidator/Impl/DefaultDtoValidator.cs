/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/24 8:52:48
 * ****************************************************************/
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SharpSword.DtoValidator.Impl
{
    /// <summary>
    /// 系统框架默认RequestDto对象属性校验器，完全兼容：System.ComponentModel.DataAnnotations 命名空间特性验证
    /// </summary>
    internal class DefaultDtoValidator : IDtoValidator
    {
        /// <summary>
        /// 优先级，系统框架默认提供的校验器，我们设置为最低级。用于兜底
        /// </summary>
        public int Priority { get { return int.MinValue; } }

        /// <summary>
        /// 验证实体数据正确性，返回RequestDtoValidatorResult对象
        /// </summary>
        /// <param name="requestDto">待校验的参数对象</param>
        /// <returns></returns>
        public DtoValidatorResult Valid(object requestDto)
        {
            //用于保存验证集合
            var validationResultErrors = new List<DtoValidatorResultError>();

            //校验定义在参数对象上的特性校验
            var validationContext = new ValidationContext(requestDto);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(requestDto, validationContext, results, true))
            {
                results.ForEach(o =>
                {
                    validationResultErrors.Add(new DtoValidatorResultError(
                        string.Join(",", o.MemberNames.ToArray()), o.ErrorMessage));
                });
            }

            //返回校验错误集合
            return new DtoValidatorResult(validationResultErrors);
        }
    }
}
