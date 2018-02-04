using Frxs.O2O.Order.SDK;
using Frxs.O2O.Order.SDK.Request;
using Frxs.O2O.Order.SDK.Resp;
using System;
using System.Linq;
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

            DateTime st = DateTime.Now;
            //var resp = client.Apis.OrderCreate(new OrderCreateRequest("0.0"));
            var r = new OrderCreateRequest("0.0")
            {
                WechatNickname = "测试数据",
                WechatImage = "测试",
                ReceiverName = "测试",
                ReceiverPhone = "1754535353",
                IsValetOrder = 1,
                PayMent = 10,
                StoreId = 1109,
                UserId = new Random().Next(1, 99999999).ToString(),
                UserName = "测试用户",
                Details = new System.Collections.Generic.List<OrderCreateDetail>() {
                         new OrderCreateDetail() {
                             PresaleActivityId = 123,
                             ProductId = 100027,
                             ProductMasterImage = "演示数据",
                             ProductName = "九阳豆浆机",
                             MutValues = "红色",
                             Quantity =new Random().Next(1,2) ,
                             SKU = "3204242"
                        }
                      }
            };
            r.PayMent = r.Details.Sum(x => x.Quantity * (decimal)0.03);

            var resp = client.Apis.OrderCreate(r);

            //client.Apis.OrderCreateProgress(new OrderCreateProgressRequest() {   });
            Console.WriteLine("上送参数");
            Console.WriteLine(resp.Resp_ReqData);

            Console.WriteLine("-----------------------------");

            //提交失败
            if (resp.Flag != 0)
            {
                Console.WriteLine(resp.Info);
                return;
            }

            if (!resp.Data.SubmitStatus)
            {
                Console.WriteLine("提交请求失败，原因：{0}", resp.Data.Message);
                Console.ReadLine();
                return;
            }

            if (resp.Data.SubmitStatus && resp.Data.OrderProgress.Status == OrderCreateResp.OrderProgressStatus.OrderCreateSuccess)
            {
                Console.WriteLine("创建订单【{0}】成功", resp.Data.OrderProgress.OrderId);
                Console.ReadLine();
                return;
            }

            if (resp.Data.SubmitStatus && resp.Data.OrderProgress.Status == OrderCreateResp.OrderProgressStatus.OrderCreateFail)
            {
                Console.WriteLine("创建订单失败，原因：{0}", resp.Data.OrderProgress.Description);
                Console.ReadLine();
                return;
            }

            if (resp.Data.SubmitStatus && (resp.Data.OrderProgress.Status == OrderCreateResp.OrderProgressStatus.Queuing || resp.Data.OrderProgress.Status == OrderCreateResp.OrderProgressStatus.Unkonw))
            {

                Console.WriteLine("您的请求已经提交，已经成功进入队列：{0} 请耐心等待。。。。。", resp.Data.OrderProgress.Rank);

                while (true)
                {

                    System.Threading.Thread.Sleep(200);

                    var resp0 = client.Apis.OrderCreateProgress(new OrderCreateProgressRequest()
                    {
                        Token = resp.Data.OrderProgress.Token
                    });

                    Console.WriteLine("-----------------------------");
                    Console.WriteLine("在您前面大概还有【{0}】位亲在排队，请稍等.....", resp0.Data.Rank);


                    if (resp0.Data.Status == OrderCreateProgressResp.OrderProgressStatus.OrderCreateSuccess)
                    {
                        Console.WriteLine("-----------------------------");
                        Console.WriteLine("下单成功，订单号【{0}】，请立即支付，超过30分钟不支付我们将自动取消", resp0.Data.OrderId);
                        break;
                    }


                    if (resp0.Data.Status == OrderCreateProgressResp.OrderProgressStatus.OrderCreateFail)
                    {
                        Console.WriteLine("-----------------------------");
                        Console.WriteLine("下单失败，原因：{0}", resp0.Data.Description);
                        break;
                    }

                }

            }


            var resp1 = client.Apis.OrderProductSalesGet(new OrderProductSalesGetRequest()
            {
                PresaleActivityId = 123,
                ProductId = 100027
            });

            Console.WriteLine("已经卖出：{0} 份", resp1.Data.SaleQuantity);


            Console.ReadLine();
        }
    }
}
