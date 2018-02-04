/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/24 8:52:48
 * ****************************************************************/
using FluentValidation;
using System.Linq;
using SharpSword;
using System;

namespace SharpSword.FluentValidation
{
    /// <summary>
    /// 
    /// </summary>
    public class FluentRequestDtoValidator : IDtoValidator
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IIocResolver _iocResolver;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iocResolver"></param>
        public FluentRequestDtoValidator(IIocResolver iocResolver)
        {
            this._iocResolver = iocResolver;
        }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority => 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public DtoValidatorResult Valid(object requestDto)
        {
            //判断当前RequestDto对象是否映射了验证
            if (!FluentValidationManager.Configs.Keys.Contains(requestDto.GetType()))
            {
                return DtoValidatorResult.Success;
            }

            //获取验证类型
            var fluentValidationType = FluentValidationManager.Configs[requestDto.GetType()];

            //创建验证
            var fluentValidationInstance = (IValidator)this._iocResolver.ResolveUnregistered(fluentValidationType);

            //校验开始
            var validResult = fluentValidationInstance.Validate(requestDto);

            //校验成功
            if (validResult.IsValid)
            {
                return DtoValidatorResult.Success;
            }

            //失败返回错误消息
            return new DtoValidatorResult(from item in validResult.Errors
                                          select new DtoValidatorResultError(item.PropertyName, item.ErrorMessage));
        }
    }
}
