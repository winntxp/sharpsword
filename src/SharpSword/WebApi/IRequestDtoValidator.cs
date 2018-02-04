/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/1/26 17:02:36
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// API上送参数校验接口
    /// </summary>
    public interface IRequestDtoValidator
    {
        /// <summary>
        /// 参数校验
        /// </summary>
        /// <param name="rquestDto">绑定出来的上送对象</param>
        /// <returns></returns>
        DtoValidatorResult Valid(object rquestDto);
    }
}