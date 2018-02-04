/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/5/9 13:47:30
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 过滤器对象包装器，此包装器主要用户过滤器执行顺序封装
    /// </summary>
    internal class ActionFilterWrapper
    {
        /// <summary>
        /// 优先级排序，优先级越高，越先执行，默认值：0
        /// 过滤器优先级
        /// 3 全局注册的过滤器
        /// 2 接口自身实现的过滤器
        /// 1 接口上附加的特性过滤器
        /// </summary>
        public int Order { get; private set; }

        /// <summary>
        /// 内部排序;在执行拦截的时候，注意下
        /// 1.拦截接口执行方法前，多个拦截器执行顺序为：InternalOrder,Order高低来排序执行
        /// 2.拦截直接执行方法后，多个拦截器执行顺序为：InternalOrder,Order低到高来执行
        /// 即全局拦截器执行优先于特性定义的拦截器，但是执行后执行顺序却是相反的
        /// 在内部，这个字段将会被赋值，全局拦截器设置为int.MaxValue，自定义的接口特性设置为int.MinValue
        /// </summary>
        public int InternalOrder { get; private set; }

        /// <summary>
        /// 过滤器实例
        /// </summary>
        public IActionFilter ActionFilter { get; private set; }

        /// <summary>
        /// 在构造函数里进行各种过滤器的排序值生成
        /// </summary>
        /// <param name="actionFilter">过滤器</param>
        public ActionFilterWrapper(IActionFilter actionFilter)
        {
            this.ActionFilter = actionFilter;

            //全局过滤器优先级最高
            this.InternalOrder = 3;

            //自身接口过滤器（优先级中等）
            if (actionFilter is IAction)
            {
                this.InternalOrder = 2;
            }

            //接口特性过滤器(优先级最后)
            if (actionFilter is ActionFilterBaseAttribute)
            {
                this.InternalOrder = 1;
                this.Order = (actionFilter as ActionFilterBaseAttribute).Order;
            }
        }
    }
}
