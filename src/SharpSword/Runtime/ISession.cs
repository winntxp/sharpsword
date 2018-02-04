/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/27/2016 4:26:32 PM
 * ****************************************************************/
using System;
using System.Collections.Generic;

namespace SharpSword
{
    /// <summary>
    /// 获取当前登录的用户信息；如果采取的IOC容器，请将此实现类注册成为单例模式
    /// </summary>
    public interface ISession : IDisposable
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// 用于保存当前登录用户其他的一些数据
        /// </summary>
        IDictionary<string, object> Properties { get; }
    }
}
