/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/30/2017 10:50:39 AM
 * ****************************************************************/
using System.Data;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 创建数据库连接对象工厂，防止我们以后迁移到mysql数据库等其他关系数据库
    /// </summary>
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// 直接支持自定义SQL访问，当分库的时候，我们可以改造自定义的数据库连接字符串进行连接
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns></returns>
        IDbConnection Create(string connectionString);
    }
}
