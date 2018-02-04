/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 17:05:48
 * ****************************************************************/
using SharpSword.Configuration.WebConfig;
using SharpSword.Data;
using System;

namespace SharpSword.AccessRecorder.DB
{
    /// <summary>
    /// 
    /// </summary>
    [ConfigurationSectionName("sharpsword.module.apiaccessRecorder.db"), Serializable]
    public class DataBaseAccessRecorderConfig : ConfigurationSectionHandlerBase, IDataTablePrefix
    {
        /// <summary>
        /// 
        /// </summary>
        public DataBaseAccessRecorderConfig() { }

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
