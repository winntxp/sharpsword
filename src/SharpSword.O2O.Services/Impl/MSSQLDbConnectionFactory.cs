/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/30/2017 10:53:42 AM
 * ****************************************************************/
using System.Data;
using System.Data.SqlClient;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 默认实现的创建工厂（MSSQL实现）
    /// </summary>
    public class MSSQLDbConnectionFactory : IDbConnectionFactory
    {
        /// <summary>
        /// 根据数据库连接字符串创建数据连接对象
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns></returns>
        public virtual IDbConnection Create(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
