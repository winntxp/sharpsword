/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/4/2017 3:49:12 PM
 * ****************************************************************/
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using System;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 我们默认采取web.config里的字符串提供节点方式获取，如果以后需要改成XML，或者JSON或者其他存储介质，我们重写下就OK
    /// </summary>
    public class WebConfigDbConnectionStringProvider : IDbConnectionStringProvider, ISingletonDependency
    {
        private static readonly IList<ConnectionStringSetting> ConnectionStringSettings = new List<ConnectionStringSetting>();
        private static readonly object Locker = new object();
        private static bool _initializationed = false;

        /// <summary>
        /// 获取所有注册的连接字符串
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<ConnectionStringSetting> GetDbConnectionStrings()
        {
            if (_initializationed)
            {
                return ConnectionStringSettings;
            }

            lock (Locker)
            {
                if (_initializationed)
                {
                    return ConnectionStringSettings;
                }

                _initializationed = true;

                var connectionStrings = ConfigurationManager.ConnectionStrings;
                foreach (ConnectionStringSettings item in connectionStrings)
                {
                    ConnectionStringSettings.Add(new ConnectionStringSetting()
                    {
                        Name = item.Name,
                        ConnectionString = item.ConnectionString,
                        ProviderName = item.ProviderName
                    });
                }

                return ConnectionStringSettings;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public ConnectionStringSetting GetRequireByName(string name, bool ignoreCase = true)
        {
            var connectionStringSetting = this.GetDbConnectionStrings()
                                                    .FirstOrDefault(x => x.Name.Equals(name, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal));
            if (connectionStringSetting.IsNull())
            {
                throw new SharpSwordCoreException("未找到【{0}】连接字符串".With(name));
            }
            return connectionStringSetting;
        }
    }
}
