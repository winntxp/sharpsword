/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 5:29:19 PM
 * ****************************************************************/
using Dapper;
using SharpSword.Data;
using SharpSword.O2O.Services.Domain;
using SharpSword.O2O.Services.Events;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 默认订单处理器
    /// </summary>
    public class DefaultOrderProgressServices : IOrderProgressServices
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IOrderIdGenerator _orderIdGenerator;
        private readonly IOrderSequenceServices _orderSequenceServices;
        private readonly IOrderExpiredManager _orderExpiredManager;
        private readonly IEventPublisher _orderEventPublisher;
        private readonly IPresaleActivityServices _presaleActivityServices;
        private readonly IUserOrderDbConnectionFactory _userOrderDbConnectionFactory;
        private readonly IAreaOrderDbConnectionFactory _areaOrderDbConnectionFactory;
        private readonly IGlobalDbConnectionFactory _globalDbConnectionFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderIdGenerator"></param>
        /// <param name="orderSequenceServices"></param>
        /// <param name="userOrderDbConnectionFactory"></param>
        /// <param name="areaOrderDbConnectionFactory"></param>
        /// <param name="globalDbConnectionFactory"></param>
        /// <param name="orderExpiredManager"></param>
        /// <param name="orderEventPublisher"></param>
        /// <param name="presaleActivityServices"></param>
        public DefaultOrderProgressServices(IOrderIdGenerator orderIdGenerator,
                                            IOrderSequenceServices orderSequenceServices,
                                            IUserOrderDbConnectionFactory userOrderDbConnectionFactory,
                                            IAreaOrderDbConnectionFactory areaOrderDbConnectionFactory,
                                            IGlobalDbConnectionFactory globalDbConnectionFactory,
                                            IOrderExpiredManager orderExpiredManager,
                                            IEventPublisher orderEventPublisher,
                                            IPresaleActivityServices presaleActivityServices)
        {
            this._orderIdGenerator = orderIdGenerator;
            this._orderSequenceServices = orderSequenceServices;
            this._userOrderDbConnectionFactory = userOrderDbConnectionFactory;
            this._areaOrderDbConnectionFactory = areaOrderDbConnectionFactory;
            this._globalDbConnectionFactory = globalDbConnectionFactory;
            this._orderExpiredManager = orderExpiredManager;
            this._orderEventPublisher = orderEventPublisher;
            this._presaleActivityServices = presaleActivityServices;
            this.Logger = GenericNullLogger<DefaultOrderProgressServices>.Instance;
            this.WarningTrigger = NullSystemWarningTrigger.Instance;
        }

        /// <summary>
        /// 日志记录器
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 警告触发器
        /// </summary>
        public ISystemWarningTrigger WarningTrigger { get; set; }

        /// <summary>
        /// 我们事先下显示接口，方便我们后续扩展处理
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        SaveOrderResult IOrderProgressServices.SaveOrder(OrderDto order)
        {
            //获取执行
            var result = this.OrderExecute(order);

            //构造处理结构信息
            var orderProgress = result.Status == SaveOrderResultStatus.OK ? new OrderProgress()
            {
                OrderId = result.OrderId,
                Status = OrderProgressStatus.OrderCreateSuccess,
                Description = "生成订单成功",
                Rank = 0
            } : new OrderProgress()
            {
                OrderId = "",
                Status = OrderProgressStatus.OrderCreateFail,
                Description = result.Message,
                Rank = 0
            };

            //移除排队，其他后面排队的前移
            this._orderSequenceServices.Out(result.Token, orderProgress);

            //压入到过期管理器，压入到创建成果消息队列
            if (result.Status == SaveOrderResultStatus.OK)
            {
                //提交到缓存，进行过期时间判断(比如：30分钟过期)，数据结构：{OrderId} {Expired}
                //后续处理，检测过期时间是否>当前时间，如果是则取消订单订单，并且触发订单关闭事件，删除缓存订单过期
                this._orderExpiredManager.Add(result.OrderId, DateTime.Now);

                //发布一个创建事件（通知后续进程进行数据同步操作）
                this._orderEventPublisher.OrderCreated(new OrderCreated()
                {
                    OrderId = result.OrderId,
                    Order = order
                });

                //this.SaveOrder2Area(order);
                //this.Logger.Debug(order.Serialize2Josn());
            }

            //没有处理成功的，需要将缓存里的卖掉的总库存和用户购买的记录缓存清理掉
            if (result.Status != SaveOrderResultStatus.OK)
            {
                foreach (var item in order.OrderItems)
                {
                    //减掉总销量
                    this._presaleActivityServices.SubPresaleProductSaleQuantity(item.PresaleActivityId.Value,
                                                                                item.ProductId,
                                                                                item.Quantity);

                    //减掉个人限购销量（如果设置了个人限购）
                    if (item.UserLimitNumber > 0)
                    {
                        this._presaleActivityServices.SubPresaleProductUserBuyQuantity(item.PresaleActivityId.Value,
                                                                                       item.ProductId,
                                                                                       item.Quantity,
                                                                                       order.UserId,
                                                                                       order.IsValetOrder == 1 ? order.ShipTo : null);
                    }
                }
            }

            //返回结果
            return result;
        }

        /// <summary>
        /// 后台订单处理
        /// </summary>
        /// <param name="order"></param>
        protected virtual SaveOrderResult OrderExecute(OrderDto order)
        {
            //获取到订单的token（排队的时候，token挂靠在订单编号上面）
            string token = order.OrderId;

            //获取到订单号
            var orderId = this._orderIdGenerator.Create(order);
            order.OrderId = orderId;
            order.OrderItems.ToList().ForEach(x => x.OrderId = orderId);
            //跟踪表设置订单编号
            order.OrderTracks.ToList().ForEach(x => x.OrderId = orderId);

            //判断下是否有带限购的商品，如果有我们就判断下库存是否足够（提交的消息队列里的订单有可能由于并发出现超卖） 减少连接数，我们将校验一批发送给数据库
            var totalLimitSqls = new List<string>();
            var userLimitSqls = new List<string>();

            foreach (var item in order.OrderItems.Where(x => x.PresaleQuantity > 0 || x.UserLimitNumber > 0))
            {
                if (item.PresaleQuantity > 0)
                {
                    //指定活动商品数量是否已经售完
                    totalLimitSqls.Add(@"EXISTS(SELECT 1 FROM [PresaleJoinInProduct] WHERE PresaleActivityId={0} AND ProductId={1} AND SaleQuantity<={2})"
                                                    .With(item.PresaleActivityId,
                                                          item.ProductId,
                                                          item.PresaleQuantity - item.Quantity));
                }

                if (item.UserLimitNumber > 0)
                {
                    //限制用户购买的商品用户是否已经达到了限购数
                    userLimitSqls.Add(@"(SELECT ISNULL(SUM(SaleQuantity),0) AS SaleQuantity FROM [PresaleProductSaleQuantity] WHERE PresaleActivityID={0} AND ProductID={1} AND UserID=@UserId {2})<={3} "
                                                    .With(item.PresaleActivityId,
                                                          item.ProductId,
                                                          order.IsValetOrder == 1 ? " AND IsValetOrder = @IsValetOrder AND ShipTo = @ShipTo " : "",
                                                          item.UserLimitNumber));
                }
            }

            //打开活动数据库
            IDbConnection globalDbConnection = this._globalDbConnectionFactory.Create();

            //打开订单数据库
            IDbConnection userOrderDbConnection = this._userOrderDbConnectionFactory.CreateByUserId(order.UserId);

            //总库存
            if (totalLimitSqls.Any())
            {
                int result = globalDbConnection.Query<int>(@"IF({0}) BEGIN SELECT 1 END ELSE BEGIN SELECT 0 END ".With(totalLimitSqls.JoinToString(" AND "))).First();
                if (result == 0)
                {
                    return new SaveOrderResult(token, SaveOrderResultStatus.LOWSTOCKS, "库存不足");
                }
            }

            //用户限购
            if (userLimitSqls.Any())
            {
                int result = userOrderDbConnection.Query<int>(@"IF({0}) BEGIN SELECT 1 END ELSE BEGIN SELECT 0 END ".With(userLimitSqls.JoinToString(" AND ")), new
                {
                    UserId = order.UserId,
                    IsValetOrder = order.IsValetOrder,
                    ShipTo = order.ShipTo
                }).First();

                if (result == 0)
                {
                    return new SaveOrderResult(token, SaveOrderResultStatus.LOWSTOCKS, "您的购买数量已经超限");
                }
            }

            //连接并关闭了的话，我们重新打开下
            if (userOrderDbConnection.State != ConnectionState.Open)
            {
                userOrderDbConnection.Open();
            }

            //开启事务
            using (var trans = userOrderDbConnection.BeginTransaction())
            {
                try
                {
                    //订单
                    userOrderDbConnection.Execute(@"INSERT INTO [Orders] ([OrderId]   ,[OrderStatus]          ,[UserId]            ,[UserName]
                                                            ,[WechatNickname]       ,[WechatImage]          ,[ShipTo]            ,[CellPhone]
                                                            ,[Address]              ,[OperationAreaID]      ,[OperationAreaName] ,[TotalCommission]
                                                            ,[Amount]               ,[OrderTotal]           ,[IP]                ,[OrderDate]
                                                            ,[PayDate]              ,[DeliveryTime]         ,[FinishDate]        ,[SupplierID]
                                                            ,[SupplierNO]           ,[SupplierName]         ,[IsShow]            ,[ClientType]
                                                            ,[PayType]              ,[IsHasPay]             ,[PaymentType]       ,[IsPresale]
                                                            ,[PresaleActivityID]    ,[Remark]               ,[LineID]            ,[LineName]
                                                            ,[DistributionClerkID]  ,[DistributionClerkName],[IsValetOrder]      ,[MemberIsShow]
                                                            ,[SupplierIsShow]       ,[BillOfLading]         ,[CompanyCommission] ,[LineSort]) 

                                                    VALUES (@OrderId                ,@OrderStatus           ,@UserId             ,@UserName
                                                           ,@WechatNickname         ,@WechatImage           ,@ShipTo             ,@CellPhone
                                                           ,@Address                ,@OperationAreaID       ,@OperationAreaName  ,@TotalCommission
                                                           ,@Amount                 ,@OrderTotal            ,@IP                 ,@OrderDate
                                                           ,@PayDate                ,@DeliveryTime          ,@FinishDate         ,@SupplierID
                                                           ,@SupplierNO             ,@SupplierName          ,@IsShow             ,@ClientType
                                                           ,@PayType                ,@IsHasPay              ,@PaymentType        ,@IsPresale
                                                           ,@PresaleActivityID      ,@Remark                ,@LineID             ,@LineName
                                                           ,@DistributionClerkID    ,@DistributionClerkName ,@IsValetOrder       ,@MemberIsShow
                                                           ,@SupplierIsShow         ,@BillOfLading          ,@CompanyCommission  ,@LineSort) ",
                                            order, trans);

                    //明细
                    userOrderDbConnection.Execute(@"INSERT INTO [OrderItems] ([OrderId]          ,[ProductId]            ,[SKU]               ,[ProductPriceID]
                                                                ,[PresaleQuantity]  ,[Quantity]             ,[ShipmentQuantity]  ,[ItemListPrice]
                                                                ,[ItemAdjustedPrice],[Commission]           ,[ItemDescription]   ,[ThumbnailsUrl]
                                                                ,[Weight]           ,[SKUContent]           ,[PackingNumber]     ,[OperationAreaID]
                                                                ,[IsStockOut]       ,[IsGetProduct]         ,[VendorID]          ,[VendorName]
                                                                ,[VendorAddress]    ,[VendorTelephone]      ,[PresaleActivityID] ,[DirectMining]
                                                                ,[SupplyPrice]      ,[CompanyCommission]    ,[DeliveryTime]      ,[ProductName]
                                                                ,[LineID]           ,[LineName])

                                                        VALUES (@OrderId            ,@ProductId             ,@SKU                ,@ProductPriceID
                                                                ,@PresaleQuantity   ,@Quantity              ,@ShipmentQuantity   ,@ItemListPrice
                                                                ,@ItemAdjustedPrice ,@Commission            ,@ItemDescription    ,@ThumbnailsUrl
                                                                ,@Weight            ,@SKUContent            ,@PackingNumber      ,@OperationAreaID 
                                                                ,@IsStockOut        ,@IsGetProduct          ,@VendorID           ,@VendorName
                                                                ,@VendorAddress     ,@VendorTelephone       ,@PresaleActivityID  ,@DirectMining
                                                                ,@SupplyPrice       ,@CompanyCommission     ,@DeliveryTime       ,@ProductName
                                                                ,@LineID            ,@LineName) ",
                                           order.OrderItems, trans);

                    //跟踪信息
                    userOrderDbConnection.Execute(@"INSERT INTO [OrderTrack] ([OrderId]          ,[Remark]       ,[CreateTime]       ,[AdminId]
                                                                ,[AdminName]        ,[AdminRole]    ,[TheName]          ,[StateId]
                                                                ,[IsDisplayUser]    ,[StateName])  

                                                         VALUES (@OrderId           ,@Remark        ,@CreateTime        ,@AdminId
                                                                ,@AdminName         ,@AdminRole     ,@TheName           ,@StateId
                                                                ,@IsDisplayUser     ,@StateName) ", order.OrderTracks, trans);


                    //购买记录(只记录限制用户购买数量的商品)
                    foreach (var item in order.OrderItems.Where(x => x.UserLimitNumber > 0))
                    {
                        userOrderDbConnection.Execute(@"IF EXISTS(SELECT 0 FROM PresaleProductSaleQuantity WHERE PresaleActivityID=@PresaleActivityId AND ProductID=@ProductID AND UserID=@UserID {0})
	                                    BEGIN
		                                    UPDATE PresaleProductSaleQuantity SET  SaleQuantity              =   SaleQuantity + @SaleQuantity, 
                                                                                   ModifyTime                =   GETDATE() 
                                                                               WHERE 
                                                                                    PresaleActivityID        =   @PresaleActivityID 
                                                                                    AND ProductID            =   @ProductID 
                                                                                    AND UserID               =   @UserID {0}
	                                    END
                                        ELSE
	                                        BEGIN
		                                        INSERT INTO	PresaleProductSaleQuantity(PresaleActivityID, 
                                                                                       ProductID, 
                                                                                       SaleQuantity, 
                                                                                       UserID, 
                                                                                       IsValetOrder, 
                                                                                       ShipTo,
                                                                                       CreateTime, 
                                                                                       ModifyTime) VALUES (@PresaleActivityID, 
                                                                                                           @ProductID, 
                                                                                                           @SaleQuantity,
                                                                                                           @UserID, 
                                                                                                           @IsValetOrder, 
                                                                                                           @ShipTo, 
                                                                                                           GETDATE(), 
                                                                                                           GETDATE())
	                                        END".With(order.IsValetOrder == 1 ? @" AND IsValetOrder = @IsValetOrder AND ShipTo = @ShipTo " : ""), new
                        {
                            PresaleActivityID = item.PresaleActivityId,
                            ProductId = item.ProductId,
                            SaleQuantity = item.Quantity,
                            UserId = order.UserId,
                            IsValetOrder = order.IsValetOrder,
                            ShipTo = order.ShipTo
                        }, trans);
                    }

                    //提交事务
                    trans.Commit();

                    //存在这样的情况，如果前面的订单插入成功，但是突然执行到此步，我们的基础数据挂了，那么实际销量数字会小于实际订单综合
                    try
                    {
                        //真实销量到数据库
                        globalDbConnection.Execute(@"UPDATE [PresaleJoinInProduct] SET SaleQuantity = SaleQuantity + @SaleQuantity 
                                                                     WHERE ProductID            =   @ProductID 
                                                                     AND   PresaleActivityID    =   @PresaleActivityID ", from item in order.OrderItems
                                                                                                                          select new
                                                                                                                          {
                                                                                                                              item.ProductId,
                                                                                                                              item.PresaleActivityId,
                                                                                                                              SaleQuantity = item.Quantity
                                                                                                                          });
                    }
                    catch (Exception ex)
                    {
                        //记录下错误日志
                        this.Logger.Error(ex);
                    }

                    //返回处理成功
                    return new SaveOrderResult(token, SaveOrderResultStatus.OK, "OK") { OrderId = orderId };

                }
                catch (Exception ex)
                {
                    //记录下错误日志
                    this.Logger.Error("处理订单失败，详情：{0}，订单原始数据：{1}".With(ex.Message, order.Serialize2FormatJosn()));

                    //发送报警信息
                    this.WarningTrigger.Warning(this, ex.Message, ex);

                    //返回处理失败
                    return new SaveOrderResult(token, SaveOrderResultStatus.ERROR, ex.Message);
                }
            }
        }

        /// <summary>
        /// 保存订单数据到区域维度数据库
        /// </summary>
        /// <param name="order"></param>
        private void SaveOrder2Area(OrderDto order)
        {
            try
            {
                //创建区域维度
                var areaOrderDbConnection = this._areaOrderDbConnectionFactory.CreateByOrderId(order.OrderId);

                //开启事务
                using (var trans = areaOrderDbConnection.BeginTransaction())
                {
                    //订单
                    areaOrderDbConnection.Execute(@"INSERT INTO [Orders] ([OrderId]   ,[OrderStatus]          ,[UserId]            ,[UserName]
                                                            ,[WechatNickname]       ,[WechatImage]          ,[ShipTo]            ,[CellPhone]
                                                            ,[Address]              ,[OperationAreaID]      ,[OperationAreaName] ,[TotalCommission]
                                                            ,[Amount]               ,[OrderTotal]           ,[IP]                ,[OrderDate]
                                                            ,[PayDate]              ,[DeliveryTime]         ,[FinishDate]        ,[SupplierID]
                                                            ,[SupplierNO]           ,[SupplierName]         ,[IsShow]            ,[ClientType]
                                                            ,[PayType]              ,[IsHasPay]             ,[PaymentType]       ,[IsPresale]
                                                            ,[PresaleActivityID]    ,[Remark]               ,[LineID]            ,[LineName]
                                                            ,[DistributionClerkID]  ,[DistributionClerkName],[IsValetOrder]      ,[MemberIsShow]
                                                            ,[SupplierIsShow]       ,[BillOfLading]         ,[CompanyCommission] ,[LineSort]) 

                                                    VALUES (@OrderId                ,@OrderStatus           ,@UserId             ,@UserName
                                                           ,@WechatNickname         ,@WechatImage           ,@ShipTo             ,@CellPhone
                                                           ,@Address                ,@OperationAreaID       ,@OperationAreaName  ,@TotalCommission
                                                           ,@Amount                 ,@OrderTotal            ,@IP                 ,@OrderDate
                                                           ,@PayDate                ,@DeliveryTime          ,@FinishDate         ,@SupplierID
                                                           ,@SupplierNO             ,@SupplierName          ,@IsShow             ,@ClientType
                                                           ,@PayType                ,@IsHasPay              ,@PaymentType        ,@IsPresale
                                                           ,@PresaleActivityID      ,@Remark                ,@LineID             ,@LineName
                                                           ,@DistributionClerkID    ,@DistributionClerkName ,@IsValetOrder       ,@MemberIsShow
                                                           ,@SupplierIsShow         ,@BillOfLading          ,@CompanyCommission  ,@LineSort) ",
                                            order, trans);

                    //明细
                    areaOrderDbConnection.Execute(@"INSERT INTO [OrderItems] ([OrderId]          ,[ProductId]            ,[SKU]               ,[ProductPriceID]
                                                                ,[PresaleQuantity]  ,[Quantity]             ,[ShipmentQuantity]  ,[ItemListPrice]
                                                                ,[ItemAdjustedPrice],[Commission]           ,[ItemDescription]   ,[ThumbnailsUrl]
                                                                ,[Weight]           ,[SKUContent]           ,[PackingNumber]     ,[OperationAreaID]
                                                                ,[IsStockOut]       ,[IsGetProduct]         ,[VendorID]          ,[VendorName]
                                                                ,[VendorAddress]    ,[VendorTelephone]      ,[PresaleActivityID] ,[DirectMining]
                                                                ,[SupplyPrice]      ,[CompanyCommission]    ,[DeliveryTime]      ,[ProductName]
                                                                ,[LineID]           ,[LineName])

                                                        VALUES (@OrderId            ,@ProductId             ,@SKU                ,@ProductPriceID
                                                                ,@PresaleQuantity   ,@Quantity              ,@ShipmentQuantity   ,@ItemListPrice
                                                                ,@ItemAdjustedPrice ,@Commission            ,@ItemDescription    ,@ThumbnailsUrl
                                                                ,@Weight            ,@SKUContent            ,@PackingNumber      ,@OperationAreaID 
                                                                ,@IsStockOut        ,@IsGetProduct          ,@VendorID           ,@VendorName
                                                                ,@VendorAddress     ,@VendorTelephone       ,@PresaleActivityID  ,@DirectMining
                                                                ,@SupplyPrice       ,@CompanyCommission     ,@DeliveryTime       ,@ProductName
                                                                ,@LineID            ,@LineName) ",
                                           order.OrderItems, trans);

                    //跟踪信息
                    areaOrderDbConnection.Execute(@"INSERT INTO [OrderTrack] ([OrderId]          ,[Remark]       ,[CreateTime]       ,[AdminId]
                                                                ,[AdminName]        ,[AdminRole]    ,[TheName]          ,[StateId]
                                                                ,[IsDisplayUser]    ,[StateName])  

                                                         VALUES (@OrderId           ,@Remark        ,@CreateTime        ,@AdminId
                                                                ,@AdminName         ,@AdminRole     ,@TheName           ,@StateId
                                                                ,@IsDisplayUser     ,@StateName) ", order.OrderTracks, trans);
                    //提交事务
                    trans.Commit();
                }
            }
            catch (Exception ex)
            {
                //记录下日志
                this.Logger.Error(ex);

                //报警
                this.WarningTrigger.Warning(this, "同步创建订单到区域库失败", ex);
            }
        }
    }
}
