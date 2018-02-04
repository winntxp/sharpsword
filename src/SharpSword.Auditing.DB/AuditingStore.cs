/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/22/2016 10:11:53 AM
 * *******************************************************/
using SharpSword.Domain.Repositories;
using SharpSword.Domain.Uow;
using System.Transactions;

namespace SharpSword.Auditing.SqlServer
{
    /// <summary>
    /// 
    /// </summary>
    internal class AuditingStore : IAuditingStore
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IRepository<AuditInfo> _auditInfoRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly AuditingStoreConfig _auditingStoreConfig;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="auditInfoRepository"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="auditingStoreConfig"></param>
        public AuditingStore(ISession session,
                             IRepository<AuditInfo> auditInfoRepository,
                             IUnitOfWorkManager unitOfWorkManager,
                             AuditingStoreConfig auditingStoreConfig)
        {
            this._auditInfoRepository = auditInfoRepository;
            this._unitOfWorkManager = unitOfWorkManager;
            this._auditingStoreConfig = auditingStoreConfig;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditInfo"></param>
        /// <returns></returns>
        public void Save(AuditInfo auditInfo)
        {
            if (!this._auditingStoreConfig.IsEnabled)
            {
                return;
            }
            using (var uow = this._unitOfWorkManager.Begin(new UnitOfWorkOptions
            {
                IsTransactional = true,
                Scope = TransactionScopeOption.Suppress
            }))
            {
                this._auditInfoRepository.Add(auditInfo);
                uow.Complete();
            }
        }
    }
}
