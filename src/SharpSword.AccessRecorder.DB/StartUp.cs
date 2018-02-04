/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/22/2016 10:11:53 AM
 * *******************************************************/
using SharpSword.Domain.Repositories;
using SharpSword.Domain.Uow;
using System.Linq;

namespace SharpSword.AccessRecorder.DB
{
    /// <summary>
    /// 初始化一下数据库
    /// </summary>
    public class StartUp : StartUpBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IRepository<Domain.AccessRecorder> _accessRecorderRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessRecorderRepository"></param>
        /// <param name="unitOfWorkManager"></param>
        public StartUp(IRepository<Domain.AccessRecorder> accessRecorderRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            this._accessRecorderRepository = accessRecorderRepository;
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
                var defaultEntity = this._accessRecorderRepository.TableNoTracking.FirstOrDefault();
            }
        }
    }
}