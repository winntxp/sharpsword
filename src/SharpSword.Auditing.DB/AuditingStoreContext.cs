/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 14:47:12
 * ****************************************************************/
using SharpSword.EntityFramework;
using System.Data.Entity;
using System.Reflection;

namespace SharpSword.Auditing.SqlServer
{
    /// <summary>
    /// 
    /// </summary>
    public class AuditingStoreContext : DbContextBase
    {
        /// <summary>
        /// 
        /// </summary>
        internal const string TableName = "SharpSword_Audits";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameOrConnectionString"></param>
        public AuditingStoreContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<AuditInfo> AuditInfos { get; set; }

        /// <summary>
        /// 实体映射程序集
        /// </summary>
        protected override Assembly EntityTypeConfigurationMapAssembly
        {
            get
            {
                return Assembly.GetExecutingAssembly();
            }
        }
    }
}