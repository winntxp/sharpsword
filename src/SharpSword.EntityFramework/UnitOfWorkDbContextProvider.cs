/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using SharpSword.Data;
using SharpSword.Domain.Uow;
using System.Data.Entity;

namespace SharpSword.EntityFramework
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    internal class UnitOfWorkDbContextProvider<TDbContext> : IDbContextProvider<TDbContext> where TDbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;
        private readonly ISqlTraceManager _sqlTraceManager;

        /// <summary>
        /// 获取上下文的时候，我们使用工作单元扩展方法将上下文保存到内部集合里，这样我们再SaveChanges()的时候，才进行事务提交
        /// </summary>
        public TDbContext DbContext
        {
            get
            {
                var dbContext = _currentUnitOfWorkProvider.Current.GetDbContext<TDbContext>();
                dbContext.Database.Log = this._sqlTraceManager.Trace;
                return dbContext;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentUnitOfWorkProvider"></param>
        /// <param name="sqlTraceManager"></param>
        public UnitOfWorkDbContextProvider(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider, ISqlTraceManager sqlTraceManager)
        {
            this._currentUnitOfWorkProvider = currentUnitOfWorkProvider;
            this._sqlTraceManager = sqlTraceManager;
        }
    }
}