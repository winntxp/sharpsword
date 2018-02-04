/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/21/2016 11:16:36 AM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword
{
    /// <summary>
    /// 系统RequestDto验证管理类，因为系统框架可能会注册多个验证实现
    /// </summary>
    public interface IDtoValidatorManager
    {
        /// <summary>
        /// 获取所有的RequestDto验证管理器
        /// </summary>
        IEnumerable<IDtoValidator> DtoValidators { get; }

        /// <summary>
        /// 针对多个RequestDto校验器进行校验
        /// </summary>
        /// <param name="value">待校验的参数对象</param>
        /// <returns></returns>
        DtoValidatorResult Valid(object value);
    }
}
