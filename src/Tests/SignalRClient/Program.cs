using System;
using Microsoft.AspNet.SignalR.Client;

namespace SharpSword.ApiMonitor
{
    class Program
    {
        static HubConnection hubConnection;

        static void Main(string[] args)
        {

            Console.WriteLine("请输入服务器地址，如：http://192.168.8.246:7777");

            string address = string.Empty;

            while (true)
            {
                address = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(address))
                {
                    break;
                }
            }

            hubConnection = new HubConnection(address);

            hubConnection.Reconnecting += HubConnection_Reconnecting;
            hubConnection.Error += HubConnection_Error;
            hubConnection.Closed += HubConnection_Closed;
            hubConnection.Received += HubConnection_Received;
            hubConnection.Reconnected += HubConnection_Reconnected;
            hubConnection.StateChanged += HubConnection_StateChanged;

            hubConnection.Headers.Add("userid", "1000");

            var hubProxy = hubConnection.CreateHubProxy("SharpSwordCommonHub");

            hubProxy.On("getNotification", (str) =>
            {
                dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(str);
                Console.WriteLine();
                Console.WriteLine(obj);
            });

          

            hubConnection.Start().ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    Console.WriteLine(t.Exception.Message);
                }
                else
                {

                    Console.WriteLine("connected");

                    hubProxy.Invoke("sendMessage", "这里我给服务器发送一个消息");
                }

            }).Wait();

            Console.ReadLine();
        }

        private static void HubConnection_StateChanged(StateChange obj)
        {
            Console.WriteLine("statechanged：{0}-->{1}", obj.OldState, obj.NewState);
            if (obj.NewState == ConnectionState.Connected)
            {
                Console.WriteLine("connection success");
            }
        }

        private static void HubConnection_Reconnected()
        {
            Console.WriteLine("reconnected");
            
        }

        private static void HubConnection_Received(string obj)
        {
            //Console.WriteLine("消息为：" + obj);
        }

        private static void HubConnection_Closed()
        {
            Console.WriteLine("closed");
        }

        private static void HubConnection_Error(Exception obj)
        {
            Console.WriteLine("ERR:" + obj.Message);
        }

        private static void HubConnection_Reconnecting()
        {
            Console.WriteLine("reconnecting .....");
        }

    }
}
