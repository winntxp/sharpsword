/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/3/7 8:52:42
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 当前请求接口执行上下文（执行前，执行后）
    /// </summary>
    public abstract class ActionContext
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="action">当前请求接口实例</param>
        /// <param name="requestContext">当前请求上下文</param>
        /// <param name="actionDescriptor">当前接口描述对象</param>
        /// <param name="requestDto">当前接口上送业务参数对象</param>
        protected ActionContext(IAction action, RequestContext requestContext, ActionDescriptor actionDescriptor, object requestDto)
        {
            this.Action = action;
            this.RequestContext = requestContext;
            this.ActionDescriptor = actionDescriptor;
            this.RequestDto = requestDto;
        }

        /// <summary>
        /// 当前请求接口实例
        /// </summary>
        public IAction Action { get; private set; }

        /// <summary>
        /// 当前请求上下文（虽然IAction接口里存在此接口，这里的RequestContext是为了外部访问方便）
        /// 此对象与当前属性Action.RequestContext对象指向同一个内存地址
        /// </summary>
        public RequestContext RequestContext { get; private set; }

        /// <summary>
        /// 当前请求的action描述信息,此属性与Action.ActionDescriptor指向统一内存地址
        /// </summary>
        public ActionDescriptor ActionDescriptor { get; private set; }

        /// <summary>
        /// 当前请求接口上送的参数对象（框架自动赋值，实现类里可以直接使用）;此对象是实现了IRequestDto接口的传输对象
        /// 此对象与当前属性Action.RequestDto对象指向同一个引用
        /// </summary>
        public object RequestDto { get; private set; }

        /// <summary>
        /// 执行返回结果，
        /// 注意：在执行前，如果在定义了返回值，那么结果后续操作将会被拦截掉，不会被执行，如果返回值不为null，那么将会对请求进行拦截
        /// </summary>
        public ActionResult Result { get; set; }
    }
}
