/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/7/2016 3:56:55 PM
 * ****************************************************************/
using Autofac;
using Autofac.Features.Metadata;
using Dapper;
using SharpSword.Auditing;
using SharpSword.Commands;
using SharpSword.Data;
using SharpSword.Domain.Repositories;
using SharpSword.Domain.Services;
using SharpSword.Domain.Uow;
using SharpSword.DynamicApi;
using SharpSword.Events;
using SharpSword.Events.Entitys;
using SharpSword.Host.Data.Domain;
using SharpSword.Net.Mail;
using SharpSword.Net.SMS;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Transactions;
using System.Web;
using static SharpSword.WebHttpHelper;

namespace SharpSword.Host.Services
{
    /// <summary>
    /// 演示事件路由，将此类型的事件保存到消息队列
    /// </summary>
    public interface IStoreToMQ { }

    /// <summary>
    /// 事件定义
    /// </summary>
    public class TaskEventData : EventData, IStoreToMQ
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
    }

    /// <summary>
    /// 事件定义
    /// </summary>
    public class Task1EventData : EventData
    {
    }

    /// <summary>
    /// 事件处理类
    /// </summary>
    public class TaskEventHandler :
        IEventHandler<TaskEventData>,
        IEventHandler<Task1EventData>,
        IEventHandler<EntityCreatedEventData<object>>,
        IEventHandler<EntityUpdatedEventData<object>>,
        IEventHandler<EntityDeletedEventData<object>>
    {
        /// <summary>
        /// 
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        private void Log(IEventData eventData)
        {
            if (this.Logger.IsEnabled(LogLevel.Debug))
            {
                this.Logger.Debug("事件类型：{0}\r\n事件参数：".With(eventData.GetType().FullName, eventData.Serialize2Josn()));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityCreatedEventData<object> eventData)
        {

            //......


            this.Log(eventData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityDeletedEventData<object> eventData)
        {
            this.Log(eventData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityUpdatedEventData<object> eventData)
        {
            this.Log(eventData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(Task1EventData eventData)
        {
            this.Log(eventData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(TaskEventData eventData)
        {
            this.Log(eventData);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Task2EventHandler : IEventHandler<TaskEventData>
    {
        /// <summary>
        /// 
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(TaskEventData eventData)
        {
            this.Logger.Debug(eventData.Serialize2Josn());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ITestService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        object Insert();
    }

    /// <summary>
    /// 动态接口演示服务类(Services类)
    /// 针对需要动态生成的服务类，注册类型到IOC容器的时候，需要将自身类型也注册进去，不能只注册对应的接口
    /// 针对服务层：需要继承:DomainServiceBase，将公开的方法定义成虚方法(virtual)即可
    /// </summary>
    [DynamicApi] //如果为了省事，类上定义的，所有符合动态API的方法都会被映射
    public class TestService : SharpSwordServicesBase,
        ITestService,
        IDynamicApiService,
        IEnableClassInterceptor,
        IPerLifetimeDependency,
        IEventHandler<EntityCreatedEventData<object>>,
        IEventHandler<EntityEventData<object>>,
        IEventHandler<EventData>,
        IEventHandler<TaskEventData>
    //需要动态映射的服务类，需要继承此接口IDynamicApiService，此为约定。
    //当然，我们可以不继承的方式来实现，但是那样会加重动态接口查询的速度，因此我们采取约定的方式来架构
    {
        /// <summary>
        ///  用于查看IOC注册生命周期
        /// </summary>
        private readonly string _t = Guid.NewGuid().ToString();

        /// <summary>
        /// 
        /// </summary>
        private readonly ITypeFinder _typeFinder;
        private readonly IRepository<Shelf> _shelfRepository;
        private readonly IRepository<Warehouse> _warehouseRepository;
        private readonly IDtoValidatorManager _requestDtoValidatorManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IDbContextFactory _dbContextFactory;
        private readonly IDbContext _dbContext;
        private string id;

        /// <summary>
        /// 短信发送接口
        /// </summary>
        public ISMSSender SmsSender { get; set; }

        /// <summary>
        /// 邮件发送接口
        /// </summary>
        public IEmailSender EmailSender { get; set; }

        /// <summary>
        /// 演示一个类注入多个缓存系统
        /// </summary>
        public ICacheManager RedisCacheManager { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ICacheManager MemoryCacheManager { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeFinder"></param>
        /// <param name="shelfRepository"></param>
        /// <param name="warehouseRepository"></param>
        /// <param name="session"></param>
        /// <param name="requestDtoValidatorManager"></param>
        /// <param name="commandManager"></param>
        /// <param name="httpContext"></param>
        /// <param name="redisCacheManager"></param>
        /// <param name="memoryCacheManager"></param>
        /// <param name="cs"></param>
        /// <param name="hcp"></param>
        /// <param name="fs"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextFactory"></param>
        /// <param name="cx"></param>
        public TestService(
            ITypeFinder typeFinder,
            IRepository<Shelf> shelfRepository,
            IRepository<Warehouse> warehouseRepository,

            //当前系统环境操作用户信息
            ISession session,

            //验证管理器
            IDtoValidatorManager requestDtoValidatorManager,

            ICommandManager commandManager,

            HttpContextBase httpContext,

            ICacheManager redisCacheManager,
            ICacheManager memoryCacheManager,

            //提供延迟获取对象
            IEnumerable<Meta<Func<ICacheManager>>> cs,
            IEnumerable<Meta<HttpContextBase>> hcp,

            //演示使用委托来适配对应的类
            IEnumerable<Factory> fs,

            IUnitOfWorkManager unitOfWorkManager,

            //当系统只有一个数据库的时候，可以直接这样注入数据访问上下文，当有多个的时候不能这样，因为无法区分连接那个数据库（后注册会覆盖先注册的）
            IDbContext dbContext,

            IDbContextFactory dbContextFactory,

            IComponentContext cx
            )
        {
            this._typeFinder = typeFinder;
            this._shelfRepository = shelfRepository;
            this._warehouseRepository = warehouseRepository;
            this.Logger = GenericNullLogger<TestService>.Instance;
            this.CacheManager = NullCacheManager.Instance;
            this.EventBus = NullEventBus.Instance;
            this._requestDtoValidatorManager = requestDtoValidatorManager;
            this.RedisCacheManager = NullCacheManager.Instance;
            this.MemoryCacheManager = NullCacheManager.Instance;
            this._unitOfWorkManager = unitOfWorkManager;
            this._dbContextFactory = dbContextFactory;


            var fact0 = fs.FirstOrDefault()("xx", 12);
            var fact1 = fs.FirstOrDefault()("cc", 100);

            this._dbContext = dbContext;
        }

        /// <summary>
        /// 演示事件触发以及订阅处理
        /// </summary> 
        public virtual void EventBusTest()
        {
            this.EventBus.Trigger(new EntityUpdatedEventData<object>(new { Id = 200, Name = 20 }) { });
            this.EventBus.Trigger(new TaskEventData() { Id = 100 });
            this.EventBus.Trigger(new Task1EventData() { });
        }

        /// <summary>
        /// 邮件发送演示
        /// </summary>
        public virtual void EmailSenderTest()
        {
            this.EmailSender.Send("24040132@qq.com", "text", "text");
        }

        /// <summary>
        /// 短信发送演示 
        /// </summary>
        public virtual void SmsSenderTest()
        {
            this.SmsSender.Send("18570966678", "test");
        }

        /// <summary>
        /// 集成Dapper操作演示
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<dynamic> Dapper()
        {
            return this._dbContext.Connection.Query("select * from shelf order by id desc");
        }

        /// <summary>
        /// 非动态API服务层方法
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public virtual string EntityQuery(NullRequestDto Id)
        {
            var x = this._shelfRepository.Table.FirstOrDefault();

            //获取第一条记录
            var obj = this._warehouseRepository.Table.FirstOrDefault();

            //返回对象
            return obj.WhName;
        }

        /// <summary>
        /// 演示原始SQL语句，以及无参动态接口定义
        /// </summary>
        [DynamicApi, SqlUsed]
        public virtual object DynamicSqlQuery()
        {
            return this._dbContext.DynamicQuery("Select * from  Shelf where id>@p0", new object[] { 10 });
        }

        /// <summary>
        /// 演示使用SQL直接查询
        /// </summary>
        /// <returns></returns>
        [SqlUsed, UnitOfWork(isTransactional: true), Audited]
        public virtual IEnumerable<dynamic> SqlQuery()
        {

            //运行时变化数据库
            var x0 = this._dbContextFactory.Create("apiV200").Connection.Query<dynamic>("select top 10 * from Warehouses");

            //采取注入的方式动态获取数据库（切换数据库委托给注入方）
            var x1 = this._dbContext.Connection.Query<dynamic>("select top 10 * from Warehouses");

            var x2 = this._dbContext.Execute("update Warehouses set WhName=@p0", true, null, new object[] { Guid.NewGuid().ToString() });

            var u0 = this._unitOfWorkManager.Current;

            //开启一个内部工作单元，此工作单元会独立于外部的工作单元
            using (var uow = this._unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {

                var u1 = this._unitOfWorkManager.Current;

                var q = this._dbContext.Connection.Query<dynamic>("select top 10 * from Warehouses");

                var k = this._warehouseRepository.Table.FirstOrDefault();
                k.WhName = Guid.NewGuid().ToString();

                uow.Complete();

                return q;
            }

        }

        /// <summary>
        /// 演示单条数据插入
        /// </summary>
        /// <returns></returns>
        [DynamicApi]
        public virtual object Insert()
        {
            //相关对象会自动插入
            var warehouse = new Warehouse()
            {
                Id = Guid.NewGuid().ToString("N"),
                WhName = "演示" + Guid.NewGuid().ToString("N"),
                Address = new Address
                {
                    City = "长沙市",
                    Dict = "长沙县",
                    Location = "芙蓉兴盛社区股份有限公司"
                },
                Shelfs = new List<Shelf>
                {
                    new Shelf()
                    {
                        ShelfName = "A",
                        IsDeleted = false,
                        CreationTime = DateTime.Now
                    },
                    new Shelf()
                    {
                        ShelfName = "A",
                        IsDeleted = false,
                        CreationTime = DateTime.Now
                    }
                },
                WarehouseExt = new WarehouseExt()
                {
                    LinkMan = "星沙仓库"
                }
            };

            this._warehouseRepository.Add(warehouse);

            //进行下提交后去自动编号
            this._unitOfWorkManager.Current.SaveChanges();

            //返回编号
            return warehouse.Shelfs.FirstOrDefault().Id;


        }

        /// <summary>
        /// 演示更新操作
        /// </summary>

        public virtual void Update()
        {
            var shelf = this._shelfRepository.Table.FirstOrDefault(o => o.Id > 1);
            if (shelf != null)
            {
                //修改字段
                shelf.ShelfName = "A0001" + Guid.NewGuid().ToString();

                //更新
                this._shelfRepository.Update(shelf);
            }
        }

        /// <summary>
        /// 演示删除操作(virtual需方法，用于AOP动态代理)
        /// </summary>
        [SqlUsed]
        public virtual void Delete()
        {
            //查询出待删除的数据
            var shelfs = this._shelfRepository.Table.Where(o => o.Id < 90000);

            //循环移除
            foreach (var item in shelfs)
            {
                this._shelfRepository.Remove(item);
            }

        }

        /// <summary>
        /// 演示事务（系统框架会自动封装事务，整个方法，只要定义成虚方法，然后定义UnitOfWork特性即可）
        /// </summary>
        [SqlUsed, UnitOfWork]
        public virtual object Trans()
        {
            //验证事务里的查询是否正常
            var q = this._dbContext.Connection.Query("select top(10) * from Shelf");

            //验证下批量插入
            IList<object> values = new List<object>();
            for (var i = 0; i < 1000; i++)
            {
                values.Add(new
                {
                    Id = Guid.NewGuid().ToString(),
                    WhName = "Dapper演示插入",
                    Address_City = "湖南长沙",
                    Address_Location = "新沙",
                    Address_Dict = "新盛"
                });
            }

            //正确的语句
            this._dbContext.Connection.Execute(
                "insert into Warehouses select @Id,@WhName,@Address_City,@Address_Location,@Address_Dict", values);

            //我们故意让其出错，我们来看下是否前面的语句插入，如果没有插入验证整体事务通过
            this._dbContext.Connection.Execute("insert into WarehousesExt select @Id,@LinkMan",
                new { Id = "", LinkMan = "ces" });

            return q;
        }

        /// <summary>
        /// 演示调用存储过程1
        /// </summary>
        /// <returns></returns>
        //public object ExecuteStoredProcedure1()
        //{
        //    var idParameter = this._dataProvider.GetParameter();
        //    idParameter.ParameterName = "Id";
        //    idParameter.Value = 1;

        //    //专门的存储过程执行方法，比下面的调用方式简单点，在进行存储过程使用的时候
        //    //请先判断下this._dataProvider.StoredProceduredSupported是否支持存储过程
        //    //因为有些数据库不执行存储过程
        //    var q = this._dbContext.ExecuteStoredProcedure<Shelf>("SP", idParameter);

        //    return q;
        //}

        /// <summary>
        /// 演示调用存储过程2
        /// </summary>
        /// <returns></returns>
        //public object ExecuteStoredProcedure2()
        //{
        //    var idParameter = this._dataProvider.GetParameter();
        //    idParameter.ParameterName = "Id";
        //    idParameter.Value = 1;
        //    //idParameter.Direction = System.Data.ParameterDirection.Output;

        //    // 执行存储过程,执行语句里需要自己定义存储参数
        //    var q = this._dbContext.Query<Shelf>("EXEC [SP] @Id", idParameter).ToList();

        //    return q;
        //}

        /// <summary>
        /// 演示导航属性
        /// </summary>
        /// <returns></returns>
        public virtual object NavigationProperty1()
        {
            //导航属性演示
            var x = this._shelfRepository.Table.Where(o => o.Id > 1).OrderBy(o => o.Id).Skip(0).Take(10).Select(o => new
            {
                WId = o.Warehouse.Id,
                WName = o.Warehouse.WhName,
                Id = o.Id
            }).ToList();

            return x;
        }

        /// <summary>
        /// 演示导航属性2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[NotDynamicApi]
        public virtual object NavigationProperty2(NullRequestDto request)
        {
            //return this._dbContext.Set<Data.Domain.Shelf>().Where(o => o.Id > 0).Select(o => new
            //{
            //    o.Id,
            //    o.ShelfAreaName,
            //    o.Warehouse.WHName,
            //    wid = o.Warehouse.Id
            //});

            var k = this._warehouseRepository.Table.AsNoTracking().Where(o => o.Id.Contains("0")).Select(o => new
            {
                o.Id,
                WHName = o.WhName,
                Shelfs = o.Shelfs.Select(x => new { x.Id, x.ShelfName })
            }).ToList();

            return k;
        }

        /// <summary>
        /// 非动态API接口
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public int Method2(int x)
        {
            return 0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Login()
        {
            HttpHeader header = new HttpHeader();
            header.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-silverlight, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-silverlight-2-b1, */*";
            header.ContentType = "application/x-www-form-urlencoded";
            header.Method = "GET";
            header.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
            header.MaxTry = 300;

            CookieContainer cookieContainer = new CookieContainer();
            var cookie = new Cookie("UserIdentity", "hbkQZz6p4%2fEO36JbQSZSiwKK1mYgoSVPFUpK3CgF4Z1MbpM7JxNZpNInArPkqBP10e7JHWYIYfx4CsmR98Bh93GrSxbRaPpFk2RqUUymmrwKBFVpEKptWdy80PsP6I0ZzNLq%2f1kIX3jgA1iY3ZJwkH4rpCS53ReDTUvT0V7u7DCu5DZfUY4%2fGxb2ipC7OUdcK4qKMdMygzHBnhVpNqoLvA3wa5WMBuQ6qsF5p8ePEU%2fshKQc1UNp%2fhU7Gryzlsil1jvzSkUIpGs5mh7%2fdMnhm7FbARrnKSZd%2bMHeRBxICYCC31ntRn%2fRgQ%3d%3d");
            cookie.Domain = "frxs.cn";
            cookieContainer.Add(cookie);

            return GetHtml("http://f3_wh.frxs.cn/WarehouseOrder/Shiping2AgencyBuyQueryList", cookieContainer, header);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public string PDF()
        //{
        //    PDDocument doc = PDDocument.load(HostHelper.MapPath("~/1.pdf"));
        //    PDFTextStripper stripper = new PDFTextStripper();
        //    var s = stripper.getText(doc);
        //    return s;
        //}

        /// <summary>
        /// 比较靠谱
        /// </summary>
        /// <returns></returns>
        public string PDF1()
        {
            Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();
            doc.LoadFromFile(HostHelper.MapPath("~/1.pdf"));

            //doc.LoadFromHTML(HostHelper.MapPath("~/1.htm"), false, false, true);
            //doc.SaveToFile(HostHelper.MapPath("~/2.pdf"), Spire.Pdf.FileFormat.PDF);

            //doc.SaveToFile(HostHelper.MapPath("~/2.xps"), Spire.Pdf.FileFormat.XPS);

            StringBuilder sb = new StringBuilder();
            foreach (Spire.Pdf.PdfPageBase p in doc.Pages)
            {
                sb.Append(p.ExtractText(false));

                //var imgs = p.ExtractImages();
            }

            // doc.PrintDocument.Print();

            doc.Close();

            var s = sb.ToString();

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string AngleSharpTest()
        {
            string html = @"<p id='p1'><span class='t'>演示</span><span>sharpsword<span><a href='http://www.sharpsword.com'><a class='x' href='http://125.com'></p>";
            var parser = new AngleSharp.Parser.Html.HtmlParser();
            var doc = parser.Parse(html);
            return doc.QuerySelectorAll("a").ToArray().Reverse().ToArray()[0].Attributes["href"].Value;

        }

        /// <summary>
        /// 
        /// </summary>
        public class Down
        {
            /// <summary>
            /// PDF下载地址
            /// </summary>
            public string PdfUrl { get; set; }
            /// <summary>
            /// EXCEL下载地址
            /// </summary>
            public string ExcelUrl { get; set; }
            /// <summary>
            /// 订单编号
            /// </summary>
            public string OrderId { get; set; }
            /// <summary>
            /// 订单类型:AW/C8
            /// </summary>
            public string Type { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string AngleSharpTest1()
        {
            string html = string.Empty;
            using (StreamReader streamReader = new StreamReader(HostHelper.MapPath("~/1.htm")))
            {
                html = streamReader.ReadToEnd();
            }

            //HTML文档转换器
            var htmlParser = new AngleSharp.Parser.Html.HtmlParser();
            var htmlDoc = htmlParser.Parse(html);

            //查找tr，分析当前页面的所有订单数据
            var rows = htmlDoc.QuerySelectorAll("tr").Where(o => o.Attributes["bgcolor"] != null && o.Attributes["bgcolor"].Value == "ACDAE8").ToList();
            rows.RemoveAt(0);
            rows.Reverse();

            //页面票据列表集合
            var items = new List<Down>();

            //获取页面票据集合
            foreach (var row in rows)
            {
                //单据编号
                string[] orderIdStr = row.QuerySelectorAll("td")[1].TextContent.Trim().Split(new char[] { '_' });

                //pdf下载地址
                var pdfUrl = row.QuerySelector("a").Attributes["href"].Value;

                //Excel导出地址
                var excelHtml = row.QuerySelector("input").Attributes["onclick"].Value.Replace("Ion_Export('", "").Replace("','", ",").Replace("');", "").Split(new char[] { ',' });
                var excelUrl = "jis_export.jsp?SORDERNO={0}&SRECVID={1}".With(excelHtml[0], excelHtml[1]);

                items.Add(new Down()
                {
                    OrderId = orderIdStr[0],
                    ExcelUrl = excelUrl,
                    PdfUrl = pdfUrl,
                    Type = orderIdStr[1]
                });
            }

            //for(int i = 0; i < items.Count-1; i++)
            //{
            //    if(items[i].OrderId+1 != items[i + 1].OrderId)
            //    {

            //    }
            //}

            return items.Serialize2Josn();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityCreatedEventData<object> eventData)
        {
            if (this.Logger.IsEnabled(LogLevel.Debug))
            {
                this.Logger.Debug("事件类型：{0}\r\n事件ID：{1}".With(eventData.GetType().FullName, eventData.EventId));
            }
        }

        /// <summary>
        /// 这里可以接受到所有继承自EntityEventData的事件
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityEventData<object> eventData)
        {
            if (eventData is EntityCreatedEventData<object>)
            {

            }

            if (eventData is EntityDeletedEventData<object>)
            {

            }

            if (eventData is EntityUpdatedEventData<object>)
            {

            }

            //...................
            if (this.Logger.IsEnabled(LogLevel.Debug))
            {
                this.Logger.Debug("实体数据操作订阅所有子类事件\r\n事件类型：{0}\r\n事件ID：{1}\r\n时间：{2}".With(eventData.GetType().FullName,
                eventData.EventId, eventData.EventTime));
              
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EventData eventData)
        {
            if (this.Logger.IsEnabled(LogLevel.Debug))
            {
                this.Logger.Debug("针对所有事件进行订阅\r\n事件类型：{0}\r\n事件ID：{1}\r\n时间:{2}".With(eventData.GetType().FullName,
                eventData.EventId, eventData.EventTime));
                this.Logger.Debug(eventData.Serialize2FormatJosn());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(TaskEventData eventData)
        {

            throw new NotImplementedException();
        }
    }
}
