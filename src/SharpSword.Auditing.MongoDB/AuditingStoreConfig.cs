/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 17:05:48
 * ****************************************************************/
using System;
using SharpSword.Configuration;
using SharpSword.Configuration.WebConfig;

namespace SharpSword.Auditing.MongoDB
{
    /// <summary>
    /// 模块配置信息
    /// </summary>
    [ConfigurationSectionName("sharpsword.module.auditingstoreconfig.mongodb"), FailReturnDefault, Serializable]
    public class AuditingStoreConfig : ConfigurationSectionHandlerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public AuditingStoreConfig() { }

        /// <summary>
        /// 数据库连接字符串
        /// mongodb://localhost:27017
        /// mongodb://localhost:27017,localhost:27018,localhost:27019
        /// </summary>
        public string ConnectionString { get; set; } = "mongodb://localhost:27017";

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DataBase { get; set; } = "auditingstore";

        /// <summary>
        /// 文档名称
        /// </summary>
        public string CollectionName { get; set; } = "sharpsword.audits";

        /// <summary>
        /// 是否记录日志;默认true
        /// </summary>
        public bool IsEnabled { get; set; } = true;
    }
}
