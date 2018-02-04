/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/23 17:26:11
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword
{
    /// <summary>
    /// 校验DTO参数是否合法（业务合法），如果上送的参数对象实现了此接口，系统框架会自动进行校验
    /// 使用原则：如果是通用的数据校验比如，字符串长度，最小，最大值等，可以使用特性校验System.ComponentModel.DataAnnotations下的特性标签；
    /// 此接口在复杂业务数据正确性校验下使用
    /// </summary>
    public interface IDtoValidatable : IValidatable
    {
        /// <summary>
        /// 此方法也许在执行Valid（）方法先，执行下数据的处理，比如设置默认值操作等
        /// </summary>
        void BeforeValid();

        /// <summary>
        /// 验证方法;如果验证不通过返回错误集合，如果验证通过了，返回一个空的集合
        /// </summary>
        /// <returns>返回校验后的错误集合;如果通过验证，则返回一个空的列表集合</returns>
        IEnumerable<DtoValidatorResultError> Valid();
    }
}
