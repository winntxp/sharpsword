/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/30/2015 9:09:00 AM
 * ****************************************************************/
using System;
using System.Collections.Generic;

namespace SharpSword.RealTime
{
    /// <summary>
    /// 用于描述一个实时连接的客户端信息
    /// </summary>
    public interface IOnlineClient
    {
        /// <summary>
        /// 连接唯一编号
        /// </summary>
        string ConnectionId { get; }

        /// <summary>
        /// 当前连接的客户端ip地址
        /// </summary>
        string IpAddress { get; }

        /// <summary>
        /// 当前连接的用户编号
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// 连接时间
        /// </summary>
        DateTime ConnectTime { get; }

        /// <summary>
        /// 根据键获取客户端自定义的数据信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object this[string key] { get; set; }

        /// <summary>
        /// 客户端自定义的数据
        /// </summary>
        Dictionary<string, object> Properties { get; }
    }
}