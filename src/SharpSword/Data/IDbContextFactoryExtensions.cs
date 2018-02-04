/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/5/2016 9:39:06 AM
 * ****************************************************************/
using System;

namespace SharpSword.Data
{
    /// <summary>
    /// DbContextFactory Extensions
    /// </summary>
    public static class IDbContextFactoryExtensions
    {
        /// <summary>
        /// 创建自定义数据操作上下文
        /// </summary>
        /// <param name="dbContextFactory"></param>
        /// <param name="getConnectionStringFun">获取连接字符串委托</param>
        /// <returns></returns>
        public static IDbContext Create(this IDbContextFactory dbContextFactory, Func<string> getConnectionStringFun)
        {
            dbContextFactory.CheckNullThrowArgumentNullException(nameof(dbContextFactory));
            getConnectionStringFun.CheckNullThrowArgumentNullException(nameof(getConnectionStringFun));
            return dbContextFactory.Create(getConnectionStringFun());
        }

        /// <summary>
        /// 创建自定义数据操作上下文
        /// </summary>
        /// <param name="dbContextFactory"></param>
        /// <param name="getConnectionStringFun">获取连接字符串委托</param>
        /// <returns></returns>
        public static IDbContext Create(this IDbContextFactory dbContextFactory, Func<IDbContextFactory, string> getConnectionStringFun)
        {
            dbContextFactory.CheckNullThrowArgumentNullException(nameof(dbContextFactory));
            getConnectionStringFun.CheckNullThrowArgumentNullException(nameof(getConnectionStringFun));
            return dbContextFactory.Create(getConnectionStringFun(dbContextFactory));
        }
    }
}
