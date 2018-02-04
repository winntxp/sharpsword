using Frxs.O2O.Order.SDK;
using Frxs.O2O.Order.SDK.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Frxs.O2O.Order.SDK.Request.OrderCreateRequest;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            IApiClientUserPrivoder u = new DefaultApiClientUserPrivoder();

            IApiClient client = new DefaultApiClient(config: new ApiClientConfiguration("http://192.168.8.246:8888/Api", "", "123456"),
                                              userPrivoder: u);
            client.Timeout = 10000;

            Parallel.For(1, 10000, i =>
            {
                var request = new OrderCreateRequest("0.0")
                {
                    WechatNickname = "测试数据",
                    WechatImage = "测试",
                    ReceiverName = Guid.NewGuid().ToString(),
                    ReceiverPhone = "1754535353",
                    IsValetOrder = 0,
                    PayMent = 10,
                    StoreId = 1109,
                    UserId = Math.Abs(Guid.NewGuid().ToString().GetHashCode()).ToString(),
                    UserName = "测试用户",
                    Details = new List<OrderCreateDetail>() {
                         new OrderCreateDetail() {
                             PresaleActivityId = 123,
                             ProductId = 100027,
                             ProductMasterImage = "演示数据",
                             ProductName = "九阳豆浆机",
                             MutValues = "红色",
                             Quantity =new Random().Next(1,3) ,
                             SKU = "3204242"
                        }
                      }
                };
                request.PayMent = request.Details.Sum(x => x.Quantity * (decimal)0.03);
                var resp = client.Apis.OrderCreate(request);

                var resp0 = client.Apis.OrderCreateProgress(new OrderCreateProgressRequest()
                {
                    Token = resp.Data.OrderProgress.Token
                });

                Console.WriteLine(resp.Resp_Body);
            });

            Console.ReadLine();
        }
    }
}
