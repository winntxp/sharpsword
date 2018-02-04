/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/21/2016 11:17:05 AM
 * ****************************************************************/

namespace SharpSword
{
    /// <summary>
    /// 上送对象RequestDto对象验证接口，注意此接口为多实现协作接口
    /// </summary>
    public interface IDtoValidator
    {
        /// <summary>
        /// 优先级，优先级越高越先有机会进行校验
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// 验证实体数据正确性，返回RequestDtoValidatorResult对象
        /// </summary>
        /// <param name="requestDto">待校验的参数对象</param>
        /// <returns></returns>
        DtoValidatorResult Valid(object requestDto);
    }
}
