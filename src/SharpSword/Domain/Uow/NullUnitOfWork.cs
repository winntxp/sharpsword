/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/24/2016 3:51:35 PM
 * ****************************************************************/

namespace SharpSword.Domain.Uow
{
    /// <summary>
    /// 空实现
    /// </summary>
    public sealed class NullUnitOfWork : UnitOfWorkBase
    {
        /// <summary>
        /// 
        /// </summary>
        public override void SaveChanges() { }

        /// <summary>
        /// 
        /// </summary>
        protected override void BeginUow() { }

        /// <summary>
        /// 
        /// </summary>
        protected override void CompleteUow() { }

        /// <summary>
        /// 
        /// </summary>
        protected override void DisposeUow() { }

        /// <summary>
        /// 
        /// </summary>
        public NullUnitOfWork()
            : base()
        {
        }
    }
}
