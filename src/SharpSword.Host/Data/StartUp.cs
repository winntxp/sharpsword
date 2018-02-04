using SharpSword.Domain.Repositories;
using SharpSword.Domain.Uow;
using SharpSword.Host.Data.Domain;
using System.Linq;

namespace SharpSword.Host.Data
{
    /// <summary>
    /// 初始化一下数据库
    /// </summary>
    public class StartUp : StartUpBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IRepository<Warehouse> _warehouseRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="warehouseRepository"></param>
        /// <param name="unitOfWorkManager"></param>
        public StartUp(IRepository<Warehouse> warehouseRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            this._warehouseRepository = warehouseRepository;
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
                var x = this._warehouseRepository.TableNoTracking.FirstOrDefault();
                uow.Complete();
            }
        }
    }
}