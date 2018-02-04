/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 14:47:12
 * ****************************************************************/
using SharpSword.Configuration.SqlServer.Domain;
using SharpSword.EntityFramework;
using System.Data.Entity;
using System.Reflection;
using System;
using SharpSword.Data;

namespace SharpSword.Configuration.SqlServer
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigurationStoreContext : DbContextBase
    {
        /// <summary>
        /// 
        /// </summary>
        internal const string TableName = "SharpSword_Configurations";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameOrConnectionString"></param>
        public ConfigurationStoreContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<ConfigurationEntity> Configurations { get; set; }

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