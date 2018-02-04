/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/3/9 14:29:53
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SharpSword.Domain.Entitys;

namespace SharpSword.Host.Data.Domain
{
    /// <summary>
    ///  仓库信息
    /// </summary>
    [Serializable]
    public class Warehouse : Entity<string>
    {
        /// <summary>
        /// 
        /// </summary>
        public Warehouse()
        {
            this.Shelfs = new HashSet<Shelf>();
        }

        /// <summary>
        /// 仓库名称
        /// </summary>
        public string WhName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<Shelf> Shelfs { get; set; }

        /// <summary>
        /// 
        /// </summary>
       // public Guid Guid { get; set; }

        /// <summary>
        /// 
        /// </summary>
       // public byte[] RowVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual WarehouseExt WarehouseExt { get; set; }

    }

    /// <summary>
    /// 仓库扩展表和仓库表1对1
    /// </summary>
    public class WarehouseExt : Entity<string>
    {
        /// <summary>
        /// 
        /// </summary>
        public string LinkMan { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Warehouse Warehouse { get; set; }

    }
}