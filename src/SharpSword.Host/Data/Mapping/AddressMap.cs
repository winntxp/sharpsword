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
    public class AddressMap : ComplexTypeConfigurationBase<Domain.Address>
    {
        /// <summary>
        /// 
        /// </summary>
        public AddressMap()
        {
            this.Property(t => t.City).HasMaxLength(50);
        }
    }
}