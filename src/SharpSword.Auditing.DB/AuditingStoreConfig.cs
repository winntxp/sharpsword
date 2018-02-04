/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 17:05:48
 * ****************************************************************/
using System;
using SharpSword.Configuration.WebConfig;
using SharpSword.Data;

namespace SharpSword.Auditing.SqlServer
{
    /// <summary>
    /// 模块配置信息
    /// </summary>
    [ConfigurationSectionName("sharpsword.module.auditingstoreconfig.sqlserver"), Serializable]
    public class AuditingStoreConfig : ConfigurationSectionHandlerBase, IDataTablePrefix
    {
        /// <summary>
        /// 
        /// </summary>
        public AuditingStoreConfig() { }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionStringName { get; set; }

        /// <summary>
        /// 生成的表前缀，默认为：SharpSword
        /// </summary>
        public string TablePrefix { get; set; } = "SharpSword";

        /// <summary>
        /// 是否记录日志;默认true
        /// </summary>
        public bool IsEnabled { get; set; } = true;
    }
}
