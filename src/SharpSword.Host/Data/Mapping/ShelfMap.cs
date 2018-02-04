/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/3/9 13:56:58
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharpSword.EntityFramework;

namespace SharpSword.Host.Data.Mapping
{
    /// <summary>
    /// 
    /// </summary>
    public class ShelfMap : EntityTypeConfigurationBase<Domain.Shelf>
    {
        /// <summary>
        /// 
        /// </summary>
        public ShelfMap()
        {
            this.ToTable("Shelf");
            this.HasKey(t => t.Id);
            this.HasRequired(o => o.Warehouse).WithMany(o => o.Shelfs).HasForeignKey(o => o.Wid).WillCascadeOnDelete(true);
        }
    }
}