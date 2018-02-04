/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/30/2016 4:54:16 PM
 * ****************************************************************/

namespace SharpSword.Domain.Uow
{
    /// <summary>
    /// 此接口用于管理工作单元作用域（因为工作单元会嵌套）
    /// </summary>
    public interface ICurrentUnitOfWorkProvider
    {
        /// <summary>
        /// 当前作用域的工作单元
        /// </summary>
        IUnitOfWork Current { get; set; }
    }
}
