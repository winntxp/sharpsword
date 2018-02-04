/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/7/2016 10:13:31 AM
 * ****************************************************************/

namespace SharpSword.Auditing
{
    /// <summary>
    /// 默认审计存储器，直接使用框架日志记录
    /// </summary>
    internal class NullAuditingStore : IAuditingStore
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly NullAuditingStore SingletonInstance = new NullAuditingStore();

        /// <summary>
        /// 
        /// </summary>
        public static NullAuditingStore Instance => SingletonInstance;

        /// <summary>
        /// 
        /// </summary>
        private NullAuditingStore() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditInfo"></param>
        /// <returns></returns>
        public void Save(AuditInfo auditInfo)
        {
            //this.Logger.Debug(auditInfo.ToString());
        }
    }
}
