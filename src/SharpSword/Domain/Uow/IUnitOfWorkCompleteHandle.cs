/* *******************************************************
 * SharpSword zhangliang4629@163.com 10/24/2016 3:51:35 PM
 * ****************************************************************/
using System;

namespace SharpSword.Domain.Uow
{
    /// <summary>
    /// 对工作单元进行提交
    /// 注意：此接口不能进行IOC注册
    /// </summary>
    public interface IUnitOfWorkCompleteHandle : IDisposable
    {
        /// <summary>
        /// 提交所有变更，以及作用域区域里的事务
        /// </summary>
        void Complete();
    }
}