/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/1/26 17:02:36
 * ****************************************************************/
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 默认的上送参数绑定器
    /// </summary>
    internal class DefaultRequestDtoBinder : IRequestDtoBinder
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 默认的上送参数绑定器
        /// </summary>
        public DefaultRequestDtoBinder()
        {
            this.Logger = NullLogger.Instance;
        }

        /// <summary>
        /// 绑定获取上送参数对象
        /// </summary>
        /// <param name="requestContext">当前请求上下文</param>
        /// <param name="actionDescriptor">接口描述信息</param>
        /// <returns>获取成功就返回上送的对象</returns>
        public TRequestDto Bind<TRequestDto>(RequestContext requestContext, IActionDescriptor actionDescriptor) where TRequestDto : IRequestDto
        {
            return (TRequestDto)this.Bind(requestContext, actionDescriptor);
        }

        /// <summary>
        /// 绑定上送参数与实体对象
        /// </summary>
        /// <param name="requestContext">当前请求上下文</param>
        /// <param name="actionDescriptor">接口描述信息</param>
        /// <returns>获取成功就返回上送的对象(弱类型)</returns>
        public object Bind(RequestContext requestContext, IActionDescriptor actionDescriptor)
        {
            //不为null
            requestContext.CheckNullThrowArgumentNullException(nameof(requestContext));
            actionDescriptor.CheckNullThrowArgumentNullException(nameof(actionDescriptor));

            //必须继承自RequestDtoBase
            if (!actionDescriptor.RequestDtoType.IsAssignableToIRequestDto())
            {
                throw new SharpSwordCoreException("requestDtoType类型必须继承自RequestDtoBase或者实现IRequestDto");
            }

            try
            {
                //DATA数据包为空，默认的给一个JSON串
                if (requestContext.RequestParams.Data.IsNullOrEmpty())
                {
                    requestContext.RequestParams.Data = "{}";
                }

                //反序列化上送的DATA数据(使用解密后的Data数据)
                var requestDto = requestContext.RequestParams.Data.DeserializeJsonStringToObject(actionDescriptor.RequestDtoType);

                //将当前上下文请求对象赋值
                requestContext.RequestDto = requestDto;

                //返回上送数据对象
                return requestDto;
            }
            catch (Exception ex)
            {
                //设置错误消息
                //string errorMessage = Resource.CoreResource.ActionBase_GetRequestDto_DeserializeObject_Error.With(this.GetType().FullName);

                //记录下日志
                this.Logger.Error(ex, requestContext.RequestParams.Data ?? string.Empty);

                //出现异常直接返回错误
                return null;
            }
        }
    }
}
