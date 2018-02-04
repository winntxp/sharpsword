/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 16:02:03
 * ****************************************************************/
using SharpSword.EntityFramework;

namespace SharpSword.AccessRecorder.DB.Mapping
{
    /// <summary>
    /// 访问日志映射
    /// </summary>
    public class AccessRecorderMap : EntityTypeConfigurationBase<Domain.AccessRecorder>
    {
        /// <summary>
        /// 
        /// </summary>
        public AccessRecorderMap()
        {
            this.ToTable("SharpSword_ApiRecorders");
            this.HasKey(p => p.Id);
            this.Property(p => p.ApiName).HasColumnName("ApiName").HasMaxLength(200);
            this.Property(p => p.HttpMethod).HasMaxLength(20);
            this.Property(p => p.Ip).HasMaxLength(15);
            this.Property(p => p.Version).HasMaxLength(20);
            this.Property(p => p.Author).HasMaxLength(50);
            this.Property(p => p.Sign).HasMaxLength(50);
            this.Property(p => p.TimeStamp).HasMaxLength(20);
            this.Property(p => p.ResponseFormat).HasMaxLength(10);
            this.Property(p => p.UserId).HasMaxLength(50);
            this.Property(p => p.UserName).HasMaxLength(50);
            this.Property(p => p.RequestId).HasMaxLength(100);
            this.Property(p => p.ResponseFormat).HasMaxLength(50);
        }
    }
}
