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
        /// ��ʼ�����ݿ�������
        /// </summary>
        /// <param name="config">���ݿ������ַ���</param>
        public RecorderObjectContext(DataBaseAccessRecorderConfig config)
            : base(config.ConnectionStringName)
        {
            //��¼��SQL���ɣ�������е���
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
        /// ʵ��ӳ�����
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