/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/21/2016 10:55:51 AM
 * ****************************************************************/

namespace SharpSword.Configuration.SqlServer
{
    /// <summary>
    /// 基于数据库参数配置需要实现的接口，只要实现此标识接口的类，会自动从
    /// 数据库来读取配置信息
    /// </summary>
    public interface IDbConfiguration : ISetting { }
}
