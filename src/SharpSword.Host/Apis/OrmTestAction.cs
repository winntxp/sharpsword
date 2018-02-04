/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/3/3 9:55:14
 * ****************************************************************/
using SharpSword.Domain.Repositories;
using SharpSword.Host.Services;
using SharpSword.WebApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Compilation;

namespace SharpSword.Host.Apis
{
    /// <summary>
    /// 演示接口框架(ORM+分库操作（数据表完全一致）+依赖注入方式访问接口)以及代码风格
    /// </summary>
    [Description("演示接口框架(ORM+分库操作（数据表完全一致）+ 依赖注入方式访问接口)以及代码风格")]
    public class OrmTestAction : ActionBase<OrmTestAction.OrmTestActionRequestDto, object>
    {
        /// <summary>
        /// 
        /// </summary>
        public class OrmTestActionRequestDto : RequestDtoBaseWithUserAndPrimaryKey<int?>
        {
            /// <summary>
            /// 
            /// </summary>
            public DateTime Created { get; set; }

            /// <summary>
            /// 演示属性
            /// </summary>
            public List<Data.Domain.Address> WHS { get; set; }
        }

        /// <summary>
        /// 数据访问仓储
        /// </summary>
        readonly IRepository<Data.Domain.Shelf> _shelfRepository;
        readonly IRepository<Data.Domain.Warehouse> _warehouseRepository;
        //readonly IDbContext _dbContext;
        //readonly IDataProvider _dataProvider;
       // readonly IUnitOfWork _unitOfWork;
        readonly ILogger<OrmTestAction> _logger;
        private readonly ITestService _testService;

        /// <summary>
        /// 
        /// </summary>
        public ILogger<OrmTestAction> GenericLogger { get; set; }

        /// <summary>
        /// 直接使用注入的方式来进行数据访问对象初始化(用于写操作)
        /// </summary>
        /// <param name="shelfRepository"></param>
        /// <param name="warehouseRepository"></param>
        /// <param name="logger">日志记录器</param>
        /// <param name="textService"></param>
        /// <param name="testService1"></param>
        /// <param name="s0"></param>
        /// <param name="s1"></param>
        /// <param name="smtpc"></param>
        /// <param name="smtpEmailSender"></param>
        public OrmTestAction(

            //演示数据访问
            IRepository<Data.Domain.Shelf> shelfRepository,
            IRepository<Data.Domain.Warehouse> warehouseRepository,

            //演示自定义SQL语句
           // IDbContext dbContext,
          //  IDataProvider dataProvider,

            //演示仓储单元演示
           // IUnitOfWork unitOfWork,
            ILogger<OrmTestAction> logger,

            //AOP演示，适用类代理
            ITestService textService,
            ITestService testService1,

            TestService s0,
            TestService s1          
           )
        {
            this._shelfRepository = shelfRepository;
            this._warehouseRepository = warehouseRepository;
          //  this._dbContext = dbContext;
         //   this._dataProvider = dataProvider;
          //  this._unitOfWork = unitOfWork;
            this._logger = logger;
            this.GenericLogger = GenericNullLogger<OrmTestAction>.Instance;
            this._testService = textService;

            //s0.Delete();
        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <returns></returns>
        public override ActionResult<object> Execute()
        {

            this._testService.Insert();

            var s = BuildManager.GetReferencedAssemblies();

            var s0 = BuildManager.GetCompiledType("~/Views/Frxs.Test.V20.CacheTest.cshtml");

            var cus = s0.GetConstructors().FirstOrDefault();

            // var d = this._dataProvider.GetParameter();
            // d.ParameterName = "Id";
            // d.Value = 1;

            // 专门的存储过程执行方法，比下面的调用方式简单点，在进行存储过程使用的时候
            // 请先判断下this._dataProvider.StoredProceduredSupported是否支持存储过程
            // 因为有些数据库不执行存储过程
            // var q = this._dbContext.ExecuteStoredProcedure<Data.Domain.Shelf>("SP", d);

            // 执行存储过程,执行语句里需要自己定义存储参数
            // var q1 = this._dbContext.SqlQuery<Data.Domain.Shelf>("EXEC [SP] @Id", d).ToList();

            //var x = this._shelfRepository.Table.Include(o => o.Warehouse).Where(o => o.Id == 20).OrderBy(o => o.Id).Skip(0).Take(10).Select(o => new
            //{
            //    WId = o.Warehouse.Id,
            //    WName = o.Warehouse.WHName,
            //    Id = o.Id
            //}).ToList();

            //导航属性演示
            var x = this._shelfRepository.Table.Where(o => o.Id == 20).OrderBy(o => o.Id).Skip(0).Take(10).Select(o => new
            {
                WId = o.Warehouse.Id,
                WName = o.Warehouse.WhName,
                Id = o.Id
            }).ToList();

            // var x0 = x.Warehouse.WHName;
            //var x1 = x.FirstOrDefault().Warehouse.Shelfs.Skip(2).Take(5).ToList();
            //var x1 = this._warehouseRepository.TableNoTracking.FirstOrDefault(o => o.Id == "200").Shelfs;

            var l0 = GenericNullLogger<OrmTestAction>.Instance;
            var l1 = GenericNullLogger<DefaultActionActivator>.Instance;

            //插入
            this._shelfRepository.Add(new Data.Domain.Shelf()
            {
                ShelfName = "A",
                Wid = "A200",
                IsDeleted = false
            });
            //this._unitOfWork.Commit();

            this._logger.Information(this._logger.GetType().FullName);

            //删除
            //var s = this._shelfRepository.Table.FirstOrDefault();
            //if (s != null)
            //{
            //    this._shelfRepository.Delete(s);
            //}
            //this._unitOfWork.Commit();

            //this.GenericLogger.Information(this.GenericLogger.GetType().FullName);

            //更新代码
            //s.WName = "更新" + Guid.NewGuid().ToString();
            //this._shelfRepository.Update(s);
            //this._unitOfWork.Commit();

            //this.Logger.Information(this.Logger.GetType().FullName);

            return this.SuccessActionResult(this.RequestDto.Serialize2Josn());

            ////正常插入数据
            //w0.Insert(new Data.Domain.Warehouse()
            //{
            //    WHID = Api.Core.SequentialGUID.GenerateSequentialGuid().ToString("N"),
            //    WHName = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffffff")
            //});

            //w1.Insert(new Data.Domain.Warehouse()
            //{
            //    WHID = Api.Core.SequentialGUID.GenerateSequentialGuid().ToString("N"),
            //    WHName = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffffff")
            //});

            //for (int i = 0; i < 100;i++) {

            //    //序列号
            //    n = n + 1;

            //    //单一数据库
            //    this._warehouseRepository.Insert(new Data.Domain.Warehouse()
            //    {
            //        WHID = DateTime.Now.ToString("yyMMddHHmmssffffff") +  n.ToString("000000000"),
            //        WHName = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffffff") + "--" + n.ToString(),
            //    });

            //    this._shelfRepository.Insert(new Data.Domain.Shelf()
            //    {
            //        ModifyTime = DateTime.Now,
            //        ModifyUserName = "system",
            //        ModifyUserID = 0,
            //        ShelfAreaID = 100,
            //        ShelfAreaName = "A",
            //        ShelfCode = "A2",
            //        ShelfID = 1,
            //        ShelfType = "WH",
            //        Status = "S",
            //        StatusStr = "SSS",
            //        WID = 100,
            //        WName = "星沙完成"
            //    });
            //}

            ////this._dbContext.SaveChanges();
            //this._unitOfWork.Commit();


            ////直接查询
            //var q = this._dbContext.SqlQuery<Data.Domain.Warehouse>("select top 1000 * from Warehouses order by whid desc", new object[] { }).OrderBy(o => o.WHID).ToList();

            //var messagePort = new MessagePost(".\\private$\\message");
            //foreach(var m in q)
            //{
            //    var r = messagePort.Post(m, m.WHID);
            //  //  this.Logger.Information(r.ToString());
            //}

            ////创建SQL数据表
            //var sql = @"CREATE TABLE [Warehouses_{0}](
            //         [WHID] [nvarchar](50) NOT NULL,
            //         [WHName] [nvarchar](100) NULL,
            //         [Parent_WHID] [nvarchar](50) NULL,
            //                    CONSTRAINT [PK_dbo.Warehouses_{0}] PRIMARY KEY CLUSTERED 
            //                    (
            //                     [WHID] ASC
            //                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
            //                    ) ON [PRIMARY] ".With(new Random().Next(10000, 99999));

            //int x = this._dbContext.ExecuteSqlCommand(sql);


            //跨库分布式事务(需要开通分布式DTC服务)-》运行：dcomcnfg 命令进行分布式事务开启
            //using (TransactionScope scope = new TransactionScope())
            //{

            //    w0.Insert(new Data.Domain.Warehouse()
            //    {
            //        Created = DateTime.Now,
            //        WHID = System.Guid.NewGuid().ToString(),
            //        WHName = System.Guid.NewGuid().ToString()
            //    });

            //    w1.Insert(new Data.Domain.Warehouse()
            //    {
            //        Created = DateTime.Now,
            //        WHID = System.Guid.NewGuid().ToString(),//测试事务的时候可以将此属性设置为null，来观测事务是否被正确提交
            //        WHName = System.Guid.NewGuid().ToString()
            //    });

            //    scope.Complete();
            //}

            //为了消除对特定数据库的依赖，请尽量不要使用原生SQL语句来查询，原生的SQL语句只用在特别复杂的查询或者报表之类的。方便框架快速切换

            //测试直接使用原生的SQL访问
            //var q = this._dbContext.SqlQuery<Data.Domain.Shelf>("select * from Shelf where ShelfID=6").Count();

            ////
            //var q1 = this._dbContext.SqlQuery<dynamic>("select * from shelf");

            ////原生SQL执行命令
            //var result = this._dbContext.ExecuteSqlCommand("update Shelf set ModifyUserName=@p0 where ShelfID=@p1", parameters: new object[] { "system", 6 });

            ////复杂业务逻辑查询
            //var shelf = this._shelfRepository.TableNoTracking.FirstOrDefault(o => o.ShelfID == 6
            //    && new string[] { "A", "B" }.Any(x => x == o.ShelfCode)
            //    && o.ShelfAreaName.Contains("A")
            //    && o.WName == "XS");

            ////复杂查询以及分页
            //var list = this._shelfRepository.TableNoTracking.Where(o => o.WID == 12 && o.ShelfAreaName.Contains("A")).Skip(1 * 100).Take(100);

            ////同库事务演示
            //using (TransactionScope scope = new TransactionScope())
            //{
            //    //新增货架数据
            //    this._shelfRepository.Insert(new Data.Domain.Shelf()
            //    {
            //        ModifyTime = DateTime.Now,
            //        ModifyUserName = "system",
            //        ModifyUserID = 0,
            //        ShelfAreaID = 100,
            //        ShelfAreaName = "A",
            //        ShelfCode = "A2",
            //        ShelfID = 1,
            //        ShelfType = "WH",
            //        Status = "S",
            //        StatusStr = "SSS",
            //        WID = 100,
            //        WName = "星沙完成"
            //    });

            //    //插入仓库数据
            //    this._warehouseRepository.Insert(new Data.Domain.Warehouse()
            //    {
            //        WHID = "{0}".With(SequentialGUID.GenerateSequentialGuid()),
            //        WHName = "测试仓库:{0}".With(DateTime.Now)
            //    });

            //    //提交事务
            //    scope.Complete();
            //}

            ////修改数据
            //var obj = this._shelfRepository.Table.FirstOrDefault(o => o.ShelfID == 6);
            //if (!obj.IsNull())
            //{
            //    obj.ModifyUserName = "system{0}".With(DateTime.Now);
            //    obj.WName = "修改后{0}".With(DateTime.Now);
            //    this._shelfRepository.Update(obj);
            //}

            //返回成功
            //return this.SuccessActionResult(q.Count);

            //单表查询
            //var q0 = from log in this._accessRecorderServcies.TableNoTracking
            //            .Where(o => o.ApiName == "API.Help" && new string[] { "127.0.01", "192.168.3.246", "192.168.3.32" }.Any(x => x == o.IP)
            //                && o.Created > new DateTime(2015, 11, 1) && o.Created <= DateTime.Now && (o.ResponseFormat == "XML" || o.ResponseFormat == "JSON")
            //                && o.UserId == 100 && o.UserName.Contains("SharpSword") && o.Author.StartsWith("X") && o.HttpMethod == "POST")
            //         select new { log.ApiName, log.Author };

            //var x0 = q0.OrderBy(o => o.ApiName).Skip(2 * 10).Take(10).ToArray();

            ////基于查询的批量/单个更新
            //var v0 = q0.Update(o => new Domain.AccessRecorder() { HttpMethod = "GET" });

            //联合
            //var q1 = from log in this._accessRecorderServcies.Table
            //         join api in this._actionDescriptorServices.Table
            //         on log.ApiName equals api.ActionName
            //         where log.Created <= DateTime.Now
            //         select new { log.ApiName, api.AccessCount };
            //var x1 = from item in q1
            //         group item by item.ApiName into g
            //         select new
            //         {
            //             key = g.Key,
            //             sum = g.Sum(item => item.AccessCount)
            //         };
            //var k1 = x1.ToList();

            //分页
            //var q2 = from log in this._accessRecorderServcies.TableNoTracking.Where(o => o.ResponseFormat == "JSON") orderby log.Id descending, log.ApiName descending select log;
            //var pager = new PagedList<Domain.AccessRecorder>(q2, 1, 50);
            //int totalPages = pager.TotalPages;
            //int totalCount = pager.TotalCount;
            //bool hasNextPage = pager.HasNextPage;
            //bool hasPreviousPage = pager.HasPreviousPage;
            //foreach (var item in pager)
            //{
            //    var s = item.StartTime;
            //}

            //验证数据存在
            //var q3 = this._actionDescriptorServices.TableNoTracking.FirstOrDefault(o => o.ActionName == "API.Help");
            //if (null != q3)
            //{
            //    //已经存在
            //}

            //存在性联合查询(TableNoTracking效率高些，如果无须上下文对象管理)
            //var q4 = from item in this._actionDescriptorServices.TableNoTracking.Where(o => o.ActionName.Contains("Api")) select item.ActionName;
            //var q5 = from item in this._accessRecorderServcies.TableNoTracking where q4.Contains(item.ApiName) && item.HttpMethod == "GET" select new { item.ApiName, item.Author, item.Created, item.HttpMethod };
            //foreach (var item in q5)
            //{
            //    var api = item.ApiName;
            //}
        }
    }

}

