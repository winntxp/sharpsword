/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/12/2016 2:05:16 PM
 * ****************************************************************/
using Castle.DynamicProxy;

namespace SharpSword.Domain.Uow
{
    /// <summary>
    /// 执行事务切入点，用于拦截具体的方法
    /// </summary>
    public class UnitOfWorkInterceptor : IInterceptor
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWorkManager">工作单元管理器</param>
        public UnitOfWorkInterceptor(IUnitOfWorkManager unitOfWorkManager)
        {
            this._unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 执行方法代理
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            //当前工作单不为空，我们继续执行下一个
            if (this._unitOfWorkManager.Current != null)
            {
                invocation.Proceed();
                return;
            }

            //需要进行工作单元管理的特性
            var unitOfWorkAttr = invocation.MethodInvocationTarget.GetUnitOfWorkAttributeOrDefault();

            //不需要执行工作单元
            if (unitOfWorkAttr.IsNull() || unitOfWorkAttr.IsDisabled)
            {
                invocation.Proceed();
                return;
            }

            //开启工作单元
            using (var uow = _unitOfWorkManager.Begin(unitOfWorkAttr.CreateOptions()))
            {
                invocation.Proceed();
                uow.Complete();
            }

        }
    }
}
