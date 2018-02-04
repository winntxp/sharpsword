/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/2/2016 4:06:32 PM
 * ****************************************************************/

namespace SharpSword.EntityFramework
{
    /// <summary>
    /// 用于自定义SQL操作适配的数据库访问上下文，此类仅仅用于做适配，用于管理数据库连接，无任何访问功能
    /// </summary>
    internal class EfAdapterDbContext : DbContextBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameOrConnectionString"></param>
        public EfAdapterDbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }
    }
}
