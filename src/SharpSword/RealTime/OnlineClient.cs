/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/30/2015 9:09:00 AM
 * ****************************************************************/
using SharpSword.Timing;
using System;
using System.Collections.Generic;

namespace SharpSword.RealTime
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class OnlineClient : IOnlineClient
    {
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, object> _properties;

        /// <summary>
        /// 连接唯一编号
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// 当前连接的客户端ip地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 当前连接的用户编号
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 连接时间
        /// </summary>
        public DateTime ConnectTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OnlineClient()
        {
            this.ConnectTime = Clock.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionId">连接唯一编号</param>
        /// <param name="ipAddress">当前连接的客户端ip地址</param>
        /// <param name="userId">当前连接的用户编号</param>
        public OnlineClient(string connectionId, string ipAddress, string userId)
            : this()
        {
            this.ConnectionId = connectionId;
            this.IpAddress = ipAddress;
            this.UserId = userId;
            this.Properties = new Dictionary<string, object>();
        }

        /// <summary>
        /// 根据键获取客户端自定义的数据信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get { return Properties[key]; }
            set { Properties[key] = value; }
        }

        /// <summary>
        /// 客户端自定义的数据
        /// </summary>
        public Dictionary<string, object> Properties
        {
            get { return _properties; }
            set
            {
                value.CheckNullThrowArgumentNullException(nameof(value));
                _properties = value;
            }
        }

        /// <summary>
        /// 序列化一下数据
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Serialize2Josn();
        }
    }
}