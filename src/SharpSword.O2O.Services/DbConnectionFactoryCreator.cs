/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/30/2017 10:50:39 AM
 * ****************************************************************/

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// DB连接创建工厂(用于依赖注入的时候动态创建连接工厂)
    /// </summary>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    public delegate IDbConnectionFactory DbConnectionFactoryCreator(string connectionString);
}