/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/2/2016 3:57:50 PM
 * ****************************************************************/
using SharpSword.Data;
using System;
using System.Collections.Concurrent;

namespace SharpSword.EntityFramework
{
    /// <summary>
    /// 此实现需要注册成线程单例：InstancePerLifetimeScope
    /// </summary>
    public class EfDbContextFactory : IDbContextFactory
    {
        /// <summary>
        /// 在当前线程里，可能会需要访问多个数据库，我们使用字符串连接作为键，将不同的数据库上下文在当前线程里保存起来
        /// </summary>
        private ConcurrentDictionary<string, IDbContext> _dbContexts = new ConcurrentDictionary<string, IDbContext>();

        /// <summary>
        /// SQL跟踪器
        /// </summary>
        private readonly ISqlTraceManager _sqlTraceManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlTraceManager"></param>
        public EfDbContextFactory(ISqlTraceManager sqlTraceManager)
        {
            this._sqlTraceManager = sqlTraceManager;
        }

        /// <summary>
        /// 创建数据访问对象
        /// </summary>
        /// <param name="nameOrConnectionString">如果是给出name，那么web.config的connectionStrings节点需要定义</param>
        /// <returns></returns>
        public IDbContext Create(string nameOrConnectionString)
        {
            return this._dbContexts.GetOrAdd(nameOrConnectionString, (string key) => new EfAdapterDbContext(key)
            {
                Database = { Log = new Action<string>(this._sqlTraceManager.Trace) },
                AutoDetectChangesEnabled = false,
                ProxyCreationEnabled = false
            });
        }
    }
}
