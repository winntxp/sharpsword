/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/4/2017 11:26:02 AM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 数据库字符串提供者
    /// </summary>
    public interface IDbConnectionStringProvider
    {
        /// <summary>
        /// 获取数据库连接字符串集合信息
        /// 具体实现需要注意下面1点：如果数据提供者没有提供任何
        /// 字符串连接信息，那么请返回一个空的List集合，因为后续的接口操作都以此为前提
        /// </summary>
        /// <returns></returns>
        IEnumerable<ConnectionStringSetting> GetDbConnectionStrings();

        /// <summary>
        /// 如果name不存在，我们需要在具体实现里抛出异常(切记)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns></returns>
        ConnectionStringSetting GetRequireByName(string name, bool ignoreCase = true);

    }

    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    public class ConnectionStringSetting
    {
        /// <summary>
        /// 数据库连接字符串名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 数据驱动提供者
        /// </summary>
        public string ProviderName { get; set; }
    }
}
