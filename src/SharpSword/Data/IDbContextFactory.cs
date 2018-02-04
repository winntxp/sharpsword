/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/25/2016 5:21:44 PM
 * ****************************************************************/
using System.Data;

namespace SharpSword.Data
{
    /// <summary>
    /// SQL访问提供接口.此实现需要注册成线程单例：InstancePerLifetimeScope
    /// </summary>
    public interface IDbContextFactory
    {
        /// <summary>
        /// 直接支持自定义SQL访问，当分库的时候，我们可以改造自定义的数据库连接字符串进行连接
        /// </summary>
        /// <param name="connectionString">数据库连接字符串或者web.config/connectionStrings连接字符串名称</param>
        /// <returns></returns>
        IDbContext Create(string connectionString);
    }
}
