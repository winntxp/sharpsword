/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/8/2016 10:01:05 AM
 * ****************************************************************/
using SharpSword.Data;
using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;

namespace SharpSword.EntityFramework
{
    /// <summary>
    /// 用于调试记录下sql语句执行
    /// </summary>
    internal class EfIntercepterLogging : DbCommandInterceptor
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly Lazy<ISqlTraceManager> _sqlTraceManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlTraceManager"></param>
        public EfIntercepterLogging()
        {
            this._sqlTraceManager = new Lazy<ISqlTraceManager>(this.CreateSqlTraceManager);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ISqlTraceManager CreateSqlTraceManager()
        {
            return ServicesContainer.Current.Resolve<ISqlTraceManager>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="interceptionContext"></param>
        public override void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            this._sqlTraceManager.Value.Trace(command.CommandText);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="interceptionContext"></param>
        public override void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            this._sqlTraceManager.Value.Trace(command.CommandText);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="interceptionContext"></param>
        public override void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            this._sqlTraceManager.Value.Trace(command.CommandText);
        }
    }
}
