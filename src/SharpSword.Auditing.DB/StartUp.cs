/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/22/2016 10:11:53 AM
 * *******************************************************/
using SharpSword.Domain.Repositories;
using SharpSword.Domain.Uow;
using System.Linq;

namespace SharpSword.Auditing.SqlServer
{
    /// <summary>
    /// 初始化一下数据库
    /// </summary>
    public class StartUp : StartUpBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IRepository<AuditInfo> _auditEntityInfoRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditEntityInfoRepository"></param>
        /// <param name="unitOfWorkManager"></param>
        public StartUp(IRepository<AuditInfo> auditEntityInfoRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            this._auditEntityInfoRepository = auditEntityInfoRepository;
            this._unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Init()
        {
            //初始化一下数据库
            using (var uow = this._unitOfWorkManager.Begin())
            {
                var defaultEntity = this._auditEntityInfoRepository.TableNoTracking.FirstOrDefault();
            }
        }
    }
}