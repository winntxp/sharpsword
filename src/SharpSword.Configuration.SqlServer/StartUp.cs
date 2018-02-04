/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/20 18:49:15
 * ****************************************************************/
using SharpSword.Configuration.SqlServer.Domain;
using SharpSword.Domain.Repositories;
using SharpSword.Domain.Uow;
using System.Linq;

namespace SharpSword.Configuration.SqlServer
{
    /// <summary>
    /// 我们在启动时候，自动生成下数据库
    /// </summary>
    public class StartUp : StartUpBase
    {
        private readonly IRepository<ConfigurationEntity> _configurationRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationRepository"></param>
        /// <param name="unitOfWorkManager"></param>
        public StartUp(IRepository<ConfigurationEntity> configurationRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            this._configurationRepository = configurationRepository;
            this._unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Init()
        {
            using (var uow = this._unitOfWorkManager.Begin())
            {
                var defaultEntity = this._configurationRepository.TableNoTracking.FirstOrDefault();
            }
        }
    }
}
