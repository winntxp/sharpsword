/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/13/2016 11:16:54 AM (4.0.30319.42000)
 * *************************************************************************/
using Dapper;
using SharpSword.Auditing;
using SharpSword.Data;
using SharpSword.Domain.Repositories;
using SharpSword.Domain.Services;
using SharpSword.Domain.Uow;
using SharpSword.Host.Data.Domain;
using SharpSword.Notifications;
using SharpSword.RealTime;
using SharpSword.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpSword.Host.Apis
{
    /// <summary>
    /// 多个实现分发器演示
    /// </summary>
    public class DispatcherAuditingStore : IAuditingStore
    {
        /// <summary>
        /// 
        /// </summary>
        private string id = Guid.NewGuid().ToString();

        /// <summary>
        /// 注意这里需要采取属性注入方式，采取构造函数注入会产生循环引用错误
        /// </summary>
        public IEnumerable<IAuditingStore> AuditingStores { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DispatcherAuditingStore()
        {
            AuditingStores = new List<IAuditingStore>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditInfo"></param>
        public void Save(AuditInfo auditInfo)
        {
            foreach (var item in this.AuditingStores.Where(x => !(x is DispatcherAuditingStore)))
            {
                item.Save(auditInfo);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IAopServices
    {
        string Get();
        string Sql();
        void Update(object dto);
        void Dapper();
        Task Notify();
        Task Send();
    }

    /// <summary>
    /// 
    /// </summary>
    public class AopServices : SharpSwordServicesBase, IAopServices
    {
        private readonly IRepository<Warehouse> _warehouseRespository;
        private readonly IDbContext _dbContext;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRealTimeNotifier _realTimeNotifier;
        private readonly IOnlineClientManager _onlineClientManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="warehouseRespository"></param>
        /// <param name="dbContext"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="realTimeNotifier"></param>
        /// <param name="onlineClientManager"></param>
        public AopServices(IRepository<Warehouse> warehouseRespository,
                           IDbContext dbContext,
                           IUnitOfWorkManager unitOfWorkManager,
                           IRealTimeNotifier realTimeNotifier,
                           IOnlineClientManager onlineClientManager)
        {
            this._warehouseRespository = warehouseRespository;
            this._dbContext = dbContext;
            this._unitOfWorkManager = unitOfWorkManager;
            this._realTimeNotifier = realTimeNotifier;
            this._onlineClientManager = onlineClientManager;
        }

        [Audited]
        public string Get()
        {

            var w = new Warehouse() { Id = "6a7c73a2c444418ebc6d9f6dc57f74f9" };
            w.Address = new Address()
            {
            };

            this._warehouseRespository.Remove(w);

            this._warehouseRespository.Remove(x => x.Id.Contains("0"));

            return _warehouseRespository.Table.FirstOrDefault().Serialize2Josn();
        }

        [Audited]
        public string Sql()
        {

            var k = this._dbContext.Query<Warehouse>("select * from Warehouses where id=@id", new { Id = "0881ee98a5dc4973b9d9986272279d0c" }).ToList();

            var x = this._dbContext.CreateParameter();
            return this._dbContext.Query<dynamic>("select * from Warehouses").Serialize2Josn();
        }

        [Audited]
        public void Update(object dto)
        {
            this._dbContext.Execute("update Warehouses set WhName = @p0", new object[] { Guid.NewGuid().ToString() });
            var q = this._dbContext.ExecuteScalar<int>("select count(1) WhName from Warehouses");
        }

        [DisableAuditing, DisableValidation]
        public void Dapper()
        {
            this._dbContext.Connection.Query<dynamic>("select * from Warehouses");
        }

        public async Task Notify()
        {
            await this._realTimeNotifier.SendNotificationsAsync(new NotificationData()
            {
                Properties = new Dictionary<string, object>().Append("Id", Guid.NewGuid().ToString())
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task Send()
        {
            return Task.FromResult(0);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [ActionName("AopAction"), GZipCompress, Version(1, 0), HttpMethod(HttpMethod.GET)]
    public class AopAction : ActionBase<NullRequestDto, string>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IAopServices _aopServices;

        /// <summary>
        /// 
        /// </summary>
        public AopAction(IAopServices aopServices, GlobalConfiguration config)
        {
            this._aopServices = aopServices;
        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <returns></returns>
        public override ActionResult<string> Execute()
        {

            this._aopServices.Notify().Wait();

            this._aopServices.Send();

            this._aopServices.Dapper();
            this._aopServices.Sql();
            this._aopServices.Update(new { Name = "sharpsword" });
            return this.SuccessActionResult(this._aopServices.Get());
        }

    }
}
