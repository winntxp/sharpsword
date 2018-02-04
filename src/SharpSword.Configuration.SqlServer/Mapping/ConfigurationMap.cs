/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 16:02:03
 * ****************************************************************/
using SharpSword.Configuration.SqlServer;
using SharpSword.Configuration.SqlServer.Domain;
using SharpSword.EntityFramework;

namespace SharpSword.Auditing.SqlServer.Mapping
{
    public class ConfigurationMap : EntityTypeConfigurationBase<ConfigurationEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        public ConfigurationMap()
        {
            this.ToTable(ConfigurationStoreContext.TableName);
            this.HasKey(p => p.Id);
            this.Property(p => p.Id).HasMaxLength(200);
        }
    }
}
