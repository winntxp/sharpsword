using SharpSword.WebApi;
using System;
using Consul;

namespace SharpSword.Consul
{
    /// <summary>
    /// 
    /// </summary>
    public class StartUp : IStartUp
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IActionSelector _actionSelector;

        /// <summary>
        /// 
        /// </summary>
        public int Priority => -1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionSelector"></param>
        public StartUp(IActionSelector actionSelector)
        {
            this._actionSelector = actionSelector;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Init()
        {
            using (var consul = new ConsulClient())
            {
                foreach (var item in this._actionSelector.GetActionDescriptors(true))
                {
                    //服务注册
                    consul.Agent.ServiceRegister(new AgentServiceRegistration()
                    {
                        ID = item.ActionName,
                        Address = "www.sharpsword.com/api",
                        Port = 80,
                        Name = item.ActionName,
                        Tags = new string[] { item.Version },
                        Check = new AgentServiceCheck
                        {
                            HTTP = "http://www.sharpsword.com/api/gettime",
                            Interval = TimeSpan.FromSeconds(5),
                        },
                        EnableTagOverride = true
                    }).Wait();

                    //服务描述信息
                    consul.KV.Put(new KVPair("Services/{0}".With(item.ActionName))
                    {
                        Value = System.Text.Encoding.UTF8.GetBytes(item.Serialize2FormatJosn())
                    }).Wait();

                }
            }
        }
    }
}
