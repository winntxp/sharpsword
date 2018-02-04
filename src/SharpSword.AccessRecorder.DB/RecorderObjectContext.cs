/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 14:47:12
 * ****************************************************************/
using SharpSword.AccessRecorder.DB.Domain;
using SharpSword.EntityFramework;
using System.Data.Entity;
using System.Reflection;

namespace SharpSword.AccessRecorder.DB
{
    /// <summary>
    /// Object context
    /// </summary>
    public class RecorderObjectContext : DbContextBase
    {
        /// <summary>
        /// 初始化数据库上下文
        /// </summary>
        /// <param name="config">数据库连接字符串</param>
        public RecorderObjectContext(DataBaseAccessRecorderConfig config)
            : base(config.ConnectionStringName)
        {
            //记录下SQL生成，方便进行调试
            //this.Database.Log = (sql) => { this.Logger.Debug(sql); };
        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Domain.AccessRecorder> AccessRecorders { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<ActionDescriptor> ActionDescriptors { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Response> Responses { get; set; }

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