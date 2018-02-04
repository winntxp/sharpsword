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
        /// ��ȡ�����ĵ�ʱ������ʹ�ù�����Ԫ��չ�����������ı��浽�ڲ����������������SaveChanges()��ʱ�򣬲Ž��������ύ
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