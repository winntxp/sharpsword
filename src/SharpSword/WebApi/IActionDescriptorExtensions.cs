/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/26 9:48:29
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 返回类型的接口类型描述转
    /// </summary>
    public static class IActionDescriptorExtensions
    {
        /// <summary>
        /// 获取actionDescriptor对象
        /// </summary>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        public static ActionDescriptor GetActionDescriptor(this IActionDescriptor actionDescriptor)
        {
            //actionDescriptor不能为null
            actionDescriptor.CheckNullThrowArgumentNullException(nameof(actionDescriptor));
            
            //包装下，返回ActionDescriptor对象
            return new ActionDescriptor(actionDescriptor);
        }
    }
}
