/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/3 19:09:05
 * ****************************************************************/
using SharpSword.EntityFramework;

namespace SharpSword.AccessRecorder.DB.Mapping
{
    /// <summary>
    /// 接口信息映射
    /// </summary>
    public class ActionDescriptorMap : EntityTypeConfigurationBase<Domain.ActionDescriptor>
    {
        /// <summary>
        /// 
        /// </summary>
        public ActionDescriptorMap()
        {
            this.ToTable("SharpSword_ApiDescriptors");
            this.HasKey(p => p.Id);
            this.Property(p => p.ActionName).IsRequired().HasMaxLength(200);
            this.Property(p => p.HttpMethod).HasMaxLength(20);
            this.Property(p => p.Version).HasMaxLength(20);
            this.Property(p => p.Description).HasMaxLength(500);
            this.Property(p => p.AuthorName).HasMaxLength(50);
        }
    }
}
