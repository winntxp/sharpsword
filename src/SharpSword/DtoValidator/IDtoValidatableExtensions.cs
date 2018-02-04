/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/23 17:26:11
 * ****************************************************************/
using System.Linq;

namespace SharpSword
{
    /// <summary>
    /// 可验证对象扩展
    /// </summary>
    public static class IDtoValidatableExtensions
    {
        /// <summary>
        /// 如果对象实现了IDtoValidatable接口，我们将直接调用下看是否验证通过
        /// 注意：此方法仅仅校验IDtoValidatable实现（即：自定义验证），不会调用特性和其他第三方对象验证
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsValid(this IDtoValidatable request)
        {
            //先低调用下验证前的设置
            request.BeforeValid();

            //调用自定义验证
            return !request.Valid().Any();
        }
    }
}
