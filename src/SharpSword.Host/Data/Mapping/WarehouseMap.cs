/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/3/9 14:30:46
 * ****************************************************************/
using System.ComponentModel.DataAnnotations.Schema;
using SharpSword.EntityFramework;

namespace SharpSword.Host.Data.Mapping
{
    /// <summary>
    /// 
    /// </summary>
    public class WarehouseMap : EntityTypeConfigurationBase<Domain.Warehouse>
    {
        /// <summary>
        /// 
        /// </summary>
        public WarehouseMap()
        {
            this.ToTable("Warehouses");
            this.HasKey(t => t.Id);
            this.Property(t => t.Id).HasMaxLength(50);
            this.Property(t => t.WhName).HasMaxLength(100);
          //  this.Property(t => t.Guid).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
         //   this.Property(t => t.RowVersion).IsRowVersion();
            this.Property(t => t.Index).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class WarehouseExrMap : EntityTypeConfigurationBase<Domain.WarehouseExt>
    {
        /// <summary>
        /// 
        /// </summary>
        public WarehouseExrMap()
        {
            this.ToTable("WarehousesExt");
            this.HasKey(t => t.Id);
            this.Property(t => t.Id).HasMaxLength(50);
            this.HasRequired(t => t.Warehouse).WithRequiredDependent(t => t.WarehouseExt).WillCascadeOnDelete(true);
        }
    }
}