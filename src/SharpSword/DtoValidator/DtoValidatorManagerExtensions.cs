/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/17/2016 1:23:56 PM
 * ****************************************************************/
using System;
using System.Linq;

namespace SharpSword
{
    /// <summary>
    /// RequestDtoValidatorManager Extensions
    /// </summary>
    public static class DtoValidatorManagerExtensions
    {
        /// <summary>
        /// 用于手工处理操作 
        /// </summary>
        /// <param name="dtoValidatorManager"></param>
        /// <param name="requestDto">待验证的数据对象</param>
        /// <param name="afterValidAction">手工操作，比如可以自定义抛出异常等等操作</param>
        public static void Valid(this IDtoValidatorManager dtoValidatorManager, object requestDto, Action<DtoValidatorResult> afterValidAction)
        {
            //为空不进行数据校验
            if (requestDto.IsNull())
            {
                return;
            }

            requestDto.CheckNullThrowArgumentNullException(nameof(requestDto));
            afterValidAction.CheckNullThrowArgumentNullException(nameof(afterValidAction));

            //校验
            var dtoValidatorResult = dtoValidatorManager.Valid(requestDto);

            //自定义校验后处理方式
            afterValidAction(dtoValidatorResult);
        }

        /// <summary>
        /// 默认实现如果校验不提供就会抛出异常，校验通过则什么事情都不做
        /// </summary>
        /// <param name="dtoValidatorManager"></param>
        /// <param name="requestDto">待验证的数据对象</param>
        public static void ValidOrThrowException(this IDtoValidatorManager dtoValidatorManager, object requestDto)
        {
            //校验不通过直接抛出异常
            dtoValidatorManager.Valid(requestDto, dtoValidatorResult =>
            {
                if (!dtoValidatorResult.IsValid)
                {
                    throw new SharpSwordCoreException((from item in dtoValidatorResult.Errors select item.ErrorMessage).ToArray().JoinToString(" "));
                }
            });
        }
    }
}
