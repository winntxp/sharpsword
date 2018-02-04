/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 16:02:03
 * ****************************************************************/
using SharpSword.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpSword.AccessRecorder.DB.Mapping
{
    /// <summary>
    /// 访问日志映射
    /// </summary>
    public class ResponseMap : EntityTypeConfigurationBase<Domain.Response>
    {
        /// <summary>
        /// 
        /// </summary>
        public ResponseMap()
        {
            this.ToTable("SharpSword_ApiResponseDatas");
            this.HasKey(p => p.Id);
            this.Property(p => p.RequestData);
            this.Property(p => p.Id).HasDatabaseGeneratedOption(databaseGeneratedOption: DatabaseGeneratedOption.None);
        }
    }
}
