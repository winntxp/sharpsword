/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/22/2016 10:11:53 AM
 * *******************************************************/
using Dapper;
using SharpSword.Data;
using SharpSword.Timing;
using System.Linq;

namespace SharpSword.Auditing.SqlServer
{
    /// <summary>
    /// 优化，直接使用SQL语句才执行
    /// </summary>
    internal class SqlAuditingStore : IAuditingStore
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly AuditingStoreConfig _auditingStoreConfig;
        private readonly IDbContextFactory _dbContextFactory;
        private readonly ISession _session;
        private static string sql = "";

        /// <summary>
        /// 
        /// </summary>
        static SqlAuditingStore()
        {
            sql = BuildBatchInsertSql();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="auditingStoreConfig"></param>
        /// <param name="dbContextFactory"></param>
        public SqlAuditingStore(ISession session,
                                AuditingStoreConfig auditingStoreConfig,
                                IDbContextFactory dbContextFactory)
        {
            this._session = session;
            this._auditingStoreConfig = auditingStoreConfig;
            this._dbContextFactory = dbContextFactory;
        }


        /// <summary>
        /// 方便后续的其他数据库同步继承，重写此方法
        /// </summary>
        /// <returns></returns>
        private static string BuildBatchInsertSql()
        {
            var propertys = typeof(AuditInfo).GetProperties().Where(p => !p.IsSpecialName);
            return "INSERT INTO [{0}]({1}) VALUES({2})"
                        .With(AuditingStoreContext.TableName,
                                propertys.Select(o => o.Name).JoinToString(),
                                propertys.Select(o => "@" + o.Name).JoinToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditInfo"></param>
        /// <returns></returns>
        public void Save(AuditInfo auditInfo)
        {
            //当前审计存储模式关闭
            if (!this._auditingStoreConfig.IsEnabled)
            {
                return;
            }

            this._dbContextFactory.Create(this._auditingStoreConfig.ConnectionStringName)
                                  .Connection
                                  .Execute(sql, auditInfo);
        }
    }
}
