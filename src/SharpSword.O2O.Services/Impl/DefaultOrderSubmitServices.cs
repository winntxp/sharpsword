/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/24/2017 1:02:47 PM
 * ****************************************************************/
using SharpSword.O2O.Data.Entities;
using SharpSword.O2O.Services.Domain;
using SharpSword.Timing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 下单服务，我们尽量在此服务里将尽可能多的校验。因为此API服务可以采取负载均衡堆机器来解决性能问题
    /// 让我们后端的订单处理程序尽可能少的进行计算，这样下单成功后能立即返回处理结果，此接口主要用来限流抗压，将无效流量进行拦截
    /// </summary>
    public class DefaultOrderSubmitServices : IOrderSubmitServices
    {
        private readonly IMessageManager _messageManager;
        private readonly IStoreServices _storeServices;
        private readonly IPresaleActivityServices _presaleServices;
        private readonly ITokenServices _tokenServices;
        private readonly IOrderProgressServices _orderProgressServices;
        private readonly IOrderSequenceServices _orderSequenceServices;
        private readonly GlobalConfig _config;
        private readonly HttpRequestBase _httpRequest;

        /// <summary>
        /// 日志记录器
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 系统报警器
        /// </summary>
        public ISystemWarningTrigger WarningTrigger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeServices"></param>
        /// <param name="presaleServices"></param>
        /// <param name="messageManager"></param>
        /// <param name="tokenServices"></param>
        /// <param name="orderProgressServices"></param>
        /// <param name="orderSequenceServices"></param>
        /// <param name="config"></param>
        /// <param name="httpRequest"></param>
        public DefaultOrderSubmitServices(IStoreServices storeServices,
                                          IPresaleActivityServices presaleServices,
                                          IMessageManager messageManager,
                                          ITokenServices tokenServices,
                                          IOrderProgressServices orderProgressServices,
                                          IOrderSequenceServices orderSequenceServices,
                                          GlobalConfig config,
                                          HttpRequestBase httpRequest)
        {
            this._messageManager = messageManager;
            this._storeServices = storeServices;
            this._presaleServices = presaleServices;
            this._tokenServices = tokenServices;
            this._orderProgressServices = orderProgressServices;
            this._orderSequenceServices = orderSequenceServices;
            this._config = config;
            this._httpRequest = httpRequest;
            this.Logger = GenericNullLogger<DefaultOrderSubmitServices>.Instance;
            this.WarningTrigger = NullSystemWarningTrigger.Instance;
        }

        /// <summary>
        /// 提交订单（获取排队信息）
        /// </summary>
        /// <param name="request"></param>
        public virtual SubmitOrderResult SubmitOrder(OrderCreateRequestDto request)
        {
            //限流（我们设置排队的人数不能超过设置的人数、保证后台在1分钟内可以处理完订单）
            if (this._config.MaxQueueLength > 0)
            {
                var currentQueueLength = _orderSequenceServices.GetCount();
                if (currentQueueLength > this._config.MaxQueueLength)
                {
                    return new SubmitOrderResult(false, "哎呀，排队人数太多了，在你前面已经有【{0}】人在排队了"
                                                .With(currentQueueLength));
                }
            }

            if (request.IsValetOrder == 0 && string.IsNullOrEmpty(request.ReceiverPhone))
            {
                return new SubmitOrderResult(false, "收货人电话不能为空");
            }
            if (string.IsNullOrEmpty(request.ReceiverName))
            {
                return new SubmitOrderResult(false, "收货人不能为空");
            }

            //获取门店信息
            var store = this._storeServices.GetStore(request.StoreId);
            if (store.IsNull() || store.Status == 1 || store.IsDeleted == 1)
            {
                return new SubmitOrderResult(false, "门店被冻结或已经被删除");
            }

            //生成我们的token
            string token = this._tokenServices.Create();

            //验证订单
            decimal orderPayMent = 0; //订单总价（最终支付价格）
            decimal shopcommission = 0; //门店总提成
            decimal companyCommission = 0; //公司总提成
            List<OrderItemDto> orderitemlist = new List<OrderItemDto>();

            //我们先判断下库存是否充足
            foreach (var item in request.Details)
            {
                //购买数量不合法
                if (item.Quantity <= 0)
                {
                    return new SubmitOrderResult(false, "购买份数不能为0");
                }

                //从缓存里读取商品预售商品信息
                var presaleProduct = this._presaleServices.GetPresaleProduct(item.PresaleActivityId, item.ProductId);
                if (presaleProduct.IsNull() || presaleProduct.ExpiryDateStart > Clock.Now
                                            || presaleProduct.ExpiryDateEnd < Clock.Now
                                            || presaleProduct.IsDeleted == 1
                                            || presaleProduct.IsAudit == 0)
                {
                    return new SubmitOrderResult(false, "商品不存在");
                }

                //获取商品已经销售的数量
                var saleQty = this._presaleServices.GetPresaleProductSaleQuantity(item.PresaleActivityId, item.ProductId);

                //如果商品存在限购，我们判断下商品是否已经超过了可售库存
                if (presaleProduct.PresaleQuantity.HasValue && presaleProduct.PresaleQuantity > 0)
                {
                    if (saleQty >= presaleProduct.PresaleQuantity.Value)
                    {
                        return new SubmitOrderResult(false, "商品【{0}】已经售罄".With(presaleProduct.ProductName));
                    }
                    //判断下是否库存不足/库存是否充足
                    if (saleQty + item.Quantity > presaleProduct.PresaleQuantity.Value)
                    {
                        return new SubmitOrderResult(false, "商品【{0}】库存不足".With(presaleProduct.ProductName));
                    }
                }

                //用户数量限制
                if (presaleProduct.UserLimitNumber.HasValue && presaleProduct.UserLimitNumber > 0)
                {
                    //判断购买数量是否大于用户限购数量
                    if (item.Quantity > presaleProduct.UserLimitNumber)
                    {
                        var msg = "产品{0}每人限购{1}份，您当前购买了{2}份".With(presaleProduct.ProductName,
                                                                               presaleProduct.UserLimitNumber.Value,
                                                                               item.Quantity);
                        return new SubmitOrderResult(false, msg);
                    }

                    //获取商品在当前活动中已购买的数量
                    var productUserBuyQuantity = this._presaleServices.GetPresaleProductUserBuyQuantity(item.PresaleActivityId,
                                                                                                        item.ProductId,
                                                                                                        request.UserId.As<int>(),
                                                                                                        request.IsValetOrder == 1 ? request.ReceiverName : null);
                    if (productUserBuyQuantity + item.Quantity > presaleProduct.UserLimitNumber)
                    {
                        //10553 【微信端-购物车】点击立即购买，提示“商品每人限购15份，已购买3份”,提示再加上还可以购买12份（微信端和待客下单都需要）
                        string msg = "您已经超过购买最大限额，每人限购数量{0}".With(presaleProduct.UserLimitNumber);
                        return new SubmitOrderResult(false, msg);
                    }
                }

                //组装商品明细
                OrderItemDto orderitem = new OrderItemDto()
                {
                    Id = 0, //ID必须重新设计
                    PresaleActivityId = presaleProduct.PresaleActivityID,
                    OrderId = token,
                    ProductId = item.ProductId,
                    Sku = item.SKU,
                    PresaleQuantity = presaleProduct.PresaleQuantity ?? 0,
                    UserLimitNumber = presaleProduct.UserLimitNumber ?? 0,
                    Quantity = item.Quantity,
                    ShipmentQuantity = item.Quantity,
                    ItemListPrice = presaleProduct.MarketPrice,
                    ItemAdjustedPrice = presaleProduct.PresalePrice,
                    SupplyPrice = presaleProduct.SupplyPrice,
                    CompanyCommission = presaleProduct.CompanyCommission,
                    Commission = presaleProduct.Commission,
                    ProductName = presaleProduct.ProductName,
                    ItemDescription = presaleProduct.ProductName + item.MutValues,
                    ThumbnailsUrl = item.ProductMasterImage,
                    DirectMining = presaleProduct.DirectMining,
                    IsGetProduct = 0,  //是否已提货（1、已提货，0、待提货）
                    VendorId = presaleProduct.VendorID,
                    LineId = store.LineID,
                    LineName = store.LineName,
                    VendorName = presaleProduct.VendorName,
                    VendorAddress = presaleProduct.VendorAddress,
                    PackingNumber = presaleProduct.PackingNumber,
                    VendorTelephone = presaleProduct.VendorTelephone,
                    DeliveryTime = presaleProduct.DeliveryTime,
                    IsStockOut = null,
                    OperationAreaId = null,
                    SkuContent = null,
                    ProductPriceId = 0,
                    Weight = null
                };

                //订单支付金额
                orderPayMent += orderitem.ItemAdjustedPrice * orderitem.Quantity;

                //门店提成金额
                shopcommission += orderitem.Commission.Value * orderitem.Quantity;

                //优选提成金额
                companyCommission += orderitem.CompanyCommission.Value * orderitem.Quantity;

                orderitemlist.Add(orderitem);
            }

            //比对下总金额
            if (orderPayMent != request.PayMent)
            {
                return new SubmitOrderResult(false, "购买商品金额不对");
            }

            //TODO:这里最好进行限流商品票据获取，初始化为限购商品，每次先-1，当库存为<=0的时候，
            //我们就不允许在进来了，方式并发少买情况出现，配合后续的二次库存校验，将少买情况降低到最低

            //第三步、添加订单表数据
            OrderDto order = new OrderDto()
            {
                OrderId = token,
                OrderStatus = 1, //待付款
                UserId = request.UserId.As<long>(),
                UserName = request.UserName,
                WechatNickname = request.WechatNickname,
                WechatImage = request.WechatImage,
                ShipTo = request.ReceiverName,
                CellPhone = request.ReceiverPhone,
                IsValetOrder = request.IsValetOrder ?? 0,
                Address = store.DetailAddress,
                Ip = this._httpRequest.GetClientIp(),
                CompanyCommission = companyCommission,
                TotalCommission = shopcommission,
                Amount = orderPayMent,
                OrderTotal = orderPayMent,
                OrderDate = DateTime.Now,
                SupplierId = Convert.ToInt32(store.StoreId),
                SupplierNo = store.StoreNo,
                SupplierName = store.StoreName,
                LineSort = store.LineSort,
                LineId = store.LineID,
                LineName = store.LineName,
                ClientType = 3,//客户端来源(1、android；2、IOS；3、微信；0、未知)
                IsPresale = 1,  //是否为预售订单（1、是，0、否）
                IsHasPay = 1, //支付完成标识(1、未支付完成；2、已支付完成； 3、支付中； 4、已付定金)
                DeliveryTime = orderitemlist.Max(x => x.DeliveryTime),
                Remark = "",
                BillOfLading = null,
                DistributionClerkId = store.DistributionClerkID,
                DistributionClerkName = store.DistributionClerkName,
                OperationAreaId = store.AreaID,
                FinishDate = null,
                IsShow = true,
                MemberIsShow = null,
                OperationAreaName = null,
                PayDate = null,
                PaymentType = null,
                PayType = null,
                PresaleActivityId = null,
                SupplierIsShow = null,
                OrderItems = orderitemlist,
                OrderTracks = new List<OrderTrack>() {
                    new OrderTrack() {
                            Id = 0, //需要重新设计
                            OrderId = token,
                            Remark = "新增订单",
                            AdminId = request.UserId.As<int>(),
                            AdminName = request.UserName,
                            AdminRole = 1,
                            StateId = 1,//订单状态（1、待付款，2、待提货/已付款，3、交易完成/已提货，4、交易关闭）
                            StateName = "待付款",
                            IsDisplayUser =1,
                            CreateTime =DateTime.Now,
                            TheName = ""
                        }
                }
            };

            try
            {

                //我们进行redis的lua执行来减扣库存，保证事务性

                //经过上面的库存判断后，在大并发的情况下，会出现超卖，我们将下单提交到消息队列，让后台线程去处理库存
                //这样经过上面的拦截，和后台线程的拦截，防止超卖的情况出现

                //我们判断下，购买的商品是否有限购或者用户限购，这样我们采取推送到不同队列(有限购的商品一个队列，无限购的商品一个队列)
                //我们做一下消息路由
                string messageRouteKey = order.OrderItems.Any(x => x.PresaleQuantity > 0 || x.UserLimitNumber > 0)
                                                                                        ? "order.save.limit" : "order.save.normal";

                //添加到消息队列
                var result = this._messageManager.Publish<string>(order.Serialize2Josn(), messageRouteKey);

                //添加成功
                if (!result)
                {
                    //返回给用户
                    return new SubmitOrderResult(false, "下单失败，请稍后重试");
                }

                //增加销量（此处在大并发量情况下会出现一个时间窗口期，会出现销售数据大于限购数量情况，但是没有关系，后台线程会进行拦截）
                foreach (var item in order.OrderItems)
                {
                    //增加全局库存销量
                    this._presaleServices.AddPresaleProductSaleQuantity(item.PresaleActivityId.Value, item.ProductId, item.Quantity);

                    //TODO:如果有用户限购，我们就记录下限购数量(此处可能出现少买情况出现，条件：当同一用户同时下单并发出现且有限购；
                    //比如：每个用户限购1个，但是这里重复提交了2次，那么后续订单操作会失败，但是库存却多扣了1，所以就会出现1个商品卖不出去)
                    if (item.UserLimitNumber > 0)
                    {
                        this._presaleServices.AddPresaleProductUserBuyQuantity(item.PresaleActivityId.Value,
                                                                               item.ProductId,
                                                                               item.Quantity,
                                                                               request.UserId.As<long>(),
                                                                               request.IsValetOrder == 1 ? request.ReceiverName : null);
                    }
                }

                //写排队信息
                long sequence = this._orderSequenceServices.In(token);

                //我们暂停50毫秒（这样让后台有一点点时间去处理下这个订单，如果50毫秒还没有处理完成我们就返回下处理进度信息）
                System.Threading.Thread.Sleep(50);

                //经过商品的时间窗口，有可能后台已经处理完了此笔订单
                //所以我们先立即访问下票据是否已经被处理了(这样客户端就无需进行轮训状态，如果消息还未被处理，会返回消息排队信息)
                var orderProgress = this._orderSequenceServices.GetOrderProgress(token);

                //如果排名小于0，我们直接返回下排名
                if (orderProgress.Rank < 0)
                {
                    orderProgress.Rank = sequence;
                }

                //返回token和排序票号（前端根据获取的票据来轮训订单处理情况）
                return new SubmitOrderResult(true, "OK", orderProgress);

            }
            catch (Exception ex)
            {
                //我们记录下错误日志
                this.Logger.Error(ex);

                //系统报警
                this.WarningTrigger.Warning(this, "提交订单失败，请立即检查.", ex);

                //返回错误
                return new SubmitOrderResult(false, ex.Message);
            }
        }
    }
}

