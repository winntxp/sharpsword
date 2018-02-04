/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 所有接口实现需要继承此接口
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// 当前请求action访问上下文信息(框架自动赋值，实现类里可以直接使用)
        /// </summary>
        RequestContext RequestContext { get; set; }

        /// <summary>
        /// 当前请求的action描述信息(框架自动赋值，实现类里可以直接使用)
        /// </summary>
        ActionDescriptor ActionDescriptor { get; set; }

        /// <summary>
        /// 上送的参数对象（框架自动赋值，实现类里可以直接使用）;此对象是实现了IRequestDto接口的传输对象
        /// </summary>
        object RequestDto { get; set; }

        /// <summary>
        /// 开始执行接口Execute()前，先执行下框架内部定义的一些判断
        /// </summary>
        void Executing();

        /// <summary>
        /// 业务逻辑真正执行的地方
        /// </summary>
        /// <returns></returns>
        ActionResult<object> Execute();

    }
}
