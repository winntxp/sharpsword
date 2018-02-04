/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/1/26 17:02:36
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// RequestDto参数获取绑定器
    /// </summary>
    public interface IRequestDtoBinder
    {
        /// <summary>
        /// 绑定获取上送参数(强类型)
        /// </summary>
        /// <param name="requestContext">当前请求上下文</param>
        /// <param name="actionDescriptor">接口描述信息</param>
        /// <returns>返回上送参数data对应的RequestDto对象</returns>
        TRequestDto Bind<TRequestDto>(RequestContext requestContext, IActionDescriptor actionDescriptor) where TRequestDto : IRequestDto;

        /// <summary>
        /// 绑定获取上送参数(弱类型)
        /// </summary>
        /// <param name="requestContext">当前请求上下文</param>
        /// <param name="actionDescriptor">接口描述信息</param>
        /// <returns>返回上送参数data对应的RequestDto对象</returns>
        object Bind(RequestContext requestContext, IActionDescriptor actionDescriptor);
    }

}
