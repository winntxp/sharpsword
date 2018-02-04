/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/24/2017 1:02:47 PM
 * ****************************************************************/
using Dapper;
using SharpSword.O2O.Data.Entities;
using SharpSword.O2O.Services.Domain;
using SharpSword.O2O.Services.Events;
using SharpSword.Timing;
using System.Collections.Generic;
using System;
using System.Linq;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 这里订单操作我们以用户维度作为主库，其他维度库我们采取消息通知方式
    /// 订单状态（1、待付款，2、待提货/已付款，3、交易完成/已提货，4、交易关闭）
    /// </summary>
    public class DefaultOrderServices : IOrderServices
    {
        private readonly IUserOrderDbConnectionFactory _userOrderDbConnectionFactory;
        private readonly IEventPublisher _orderEventPublisher;
        private readonly IOrderExpiredManager _orderExpiredManager;
        private readonly IOrderFinishedManager _orderFinishedManager;
        private readonly GlobalConfig _globalConfig;

        /// <summary>
        /// 日志记录器
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userOrderDbConnectionFactory"></param>
        /// <param name="userOrderDbTableFinder"></param>
        /// <param name="orderEventPublisher"></param>
        /// <param name="orderExpiredManager"></param>
        /// <param name="orderFinishedManager"></param>
        /// <param name="globalConfig"></param>
        public DefaultOrderServices(IUserOrderDbConnectionFactory userOrderDbConnectionFactory,
                                    IEventPublisher orderEventPublisher,
                                    IOrderExpiredManager orderExpiredManager,
                                    IOrderFinishedManager orderFinishedManager,
                                    GlobalConfig globalConfig)
        {
            this._userOrderDbConnectionFactory = userOrderDbConnectionFactory;
            this._orderEventPublisher = orderEventPublisher;
            this._orderExpiredManager = orderExpiredManager;
            this._orderFinishedManager = orderFinishedManager;
            this._globalConfig = globalConfig;
            this.Logger = GenericNullLogger<DefaultOrderSubmitServices>.Instance;
        }

        /// <summary>
        /// 取消订单操作
        /// </summary>
        /// <param name="request"></param>
        public virtual void CloseOrder(CloseOrderRequestDto request)
        {
            //我我们先后去到订单详情和明细
            //var order = this.GetOrder(request.OrderId);

            //判断是否已经支付(需要再次从微信支付查询一次，看是否真实已经支付)

            //判断是否已经取消了

            //取消订单

            //取消成功，执行下面步骤

            //减销售量
            //foreach (var item in order.OrderItems)
            //{
            //    //减数据库总销量

            //    //减数据库限购销量

            //    //减内存销量
            //    this._presaleActivityServices.SubPresaleProductSaleQuantity(item.PresaleActivityId.Value,
            //                                                                item.ProductId,
            //                                                                item.Quantity);

            //    //减限购销量
            //    if (item.UserLimitNumber > 0)
            //    {
            //        this._presaleActivityServices.SubPresaleProductUserBuyQuantity(item.PresaleActivityId.Value,
            //                                                                       item.ProductId,
            //                                                                       item.Quantity,
            //                                                                       order.UserId,
            //                                                                       order.IsValetOrder == 1 ? order.ShipTo : "");
            //    }
            //}

            //检测过期管理器是否存在，存在删除过期管理器? 是否将此步骤移出到外部？
            this._orderExpiredManager.Remove(request.OrderId);

            //发布取消事件
            this._orderEventPublisher.OrderClosed(new OrderClosed()
            {
                OrderId = request.OrderId
            });
        }

        /// <summary>
        /// 获取订单明细
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public virtual OrderDto GetOrder(string orderId)
        {
            using (var conn = this._userOrderDbConnectionFactory.CreateByOrderId(orderId))
            {
                using (var reader = conn.QueryMultiple(@"SELECT * FROM Orders WHERE OrderId = @OrderId;
                                                                 SELECT * FROM OrderItems WHERE OrderId = @OrderId;
                                                                 SELECT * FROM OrderTrack WHERE OrderId = @OrderId;",
                                                            new { OrderId = orderId }))
                {
                    var order = reader.Read<OrderDto>().FirstOrDefault();
                    if (order.IsNull())
                    {
                        return null;
                    }
                    order.OrderItems = reader.Read<OrderItemDto>().ToList();
                    order.OrderTracks = reader.Read<OrderTrack>().ToList();
                    return order;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Order GetOrderSimple(string orderId)
        {
            using (var conn = this._userOrderDbConnectionFactory.CreateByOrderId(orderId))
            {
                return conn.Query<Order>("SELECT * FROM Orders WHERE OrderId=@OrderId", new { OrderId = orderId })
                           .FirstOrDefault();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        protected virtual IEnumerable<OrderItem> GetOrderItems(string orderId)
        {
            using (var conn = this._userOrderDbConnectionFactory.CreateByOrderId(orderId))
            {
                return conn.Query<OrderItem>("SELECT * FROM OrderItems WHERE OrderId=@OrderId", new { OrderId = orderId }).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        protected virtual IEnumerable<OrderTrack> GetOrderTracks(string orderId)
        {
            using (var conn = this._userOrderDbConnectionFactory.CreateByOrderId(orderId))
            {
                return conn.Query<OrderTrack>("SELECT * FROM OrderTrack WHERE OrderId=@OrderId", new { OrderId = orderId })
                           .ToList();
            }
        }

        /// <summary>
        /// 支付订单操作，支付完成标识(1、未支付完成；2、已支付完成； 3、支付中； 4、已付定金)
        /// </summary>
        /// <param name="request"></param>
        public virtual void PayOrder(PayOrderRequestDto request)
        {
            //执行业务逻辑

            //从过期管理器里删除(支付成功了，当然就不需要进行过期管理啦)
            this._orderExpiredManager.Remove(request.OrderId);

            //压入到自动收回管理器
            this._orderFinishedManager.Add(request.OrderId, Clock.Now);

            //发布事件
            this._orderEventPublisher.OrderPayed(new OrderPayed()
            {
                OrderId = request.OrderId
            });
        }

        /// <summary>
        /// 发货操作
        /// </summary>
        /// <param name="request"></param>
        public virtual void ShipOrder(ShipOrderRequestDto request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 完成订单操作
        /// </summary>
        /// <param name="request"></param>
        public virtual void FinishOrder(FinishOrderRequestDto request)
        {
            try
            {
                //执行业务逻辑(我们仅仅更改下状态)
                var result = this._userOrderDbConnectionFactory.CreateByOrderId(request.OrderId)
                                                     .Execute("UPDATE Orders SET OrderStatus=3 WHERE OrderStatus=2 AND OrderId=@OrderId",
                                                     new { OrderId = request.OrderId });

                //防止重复触发
                if (result == 0)
                {
                    return;
                }

                //发布事件(通知异构系统去同步)
                this._orderEventPublisher.OrderFinished(new OrderFinished()
                {
                    OrderId = request.OrderId
                });

            }
            catch (Exception ex)
            {
                this.Logger.Error(ex);
            }
        }


    }
}

