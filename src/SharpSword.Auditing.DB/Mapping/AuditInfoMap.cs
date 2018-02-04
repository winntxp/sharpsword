/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 16:02:03
 * ****************************************************************/
using SharpSword.EntityFramework;

namespace SharpSword.Auditing.SqlServer.Mapping
{
    /// <summary>
    /// 访问日志映射
    /// </summary>
    public class AuditInfoMap : EntityTypeConfigurationBase<AuditInfo>
    {
        /// <summary>
        /// 
        /// </summary>
        public AuditInfoMap()
        {
            this.ToTable(AuditingStoreContext.TableName);
            this.HasKey(p => p.Id);
            this.Property(p => p.Id).HasMaxLength(50).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            this.Property(p => p.ServiceName).HasMaxLength(200);
            this.Property(p => p.MethodName).HasMaxLength(100);
            this.Property(p => p.ClientIpAddress).HasMaxLength(50);
            this.Property(p => p.ClientName).HasMaxLength(50);
            this.Property(p => p.BrowserInfo).HasMaxLength(500);
            this.Property(p => p.ExecutionUserId).HasMaxLength(50);
            this.Property(p => p.ExecutionUserName).HasMaxLength(50);
            this.Property(p => p.ThreadId).HasMaxLength(50);
        }
    }
}
