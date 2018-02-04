/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/1/26 17:02:36
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 系统框架默认提供的上送参数校验器
    /// </summary>
    internal class DefaultRequestDtoValidator : IRequestDtoValidator
    {
        /// <summary>
        /// 
        /// </summary>
        private IDtoValidatorManager _dtoValidatorManager;

        /// <summary>
        /// 我们这里校验直接使用系统框架提供的校验器来校验
        /// </summary>
        /// <param name="dtoValidatorManager">DTO验证器</param>
        public DefaultRequestDtoValidator(IDtoValidatorManager dtoValidatorManager)
        {
            dtoValidatorManager.CheckNullThrowArgumentNullException(nameof(dtoValidatorManager));
            this._dtoValidatorManager = dtoValidatorManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestDto">API接口上送参数</param>
        public DtoValidatorResult Valid(object requestDto)
        {
            requestDto.CheckNullThrowArgumentNullException(nameof(requestDto));
            return this._dtoValidatorManager.Valid(requestDto);
        }
    }
}
