/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 17:05:48
 * ****************************************************************/
using System;
using SharpSword.Configuration.WebConfig;
using SharpSword.Data;

namespace SharpSword.Configuration.SqlServer
{
    /// <summary>
    /// 
    /// </summary>
    [ConfigurationSectionName("sharpsword.module.configuration.sqlserver"), Serializable]
    public class ConfigurationConfig : ConfigurationSectionHandlerBase, IDataTablePrefix
    {
        /// <summary>
        /// 
        /// </summary>
        public ConfigurationConfig()
        {
            this.TablePrefix = "SharpSword";
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionStringName { get; set; }

        /// <summary>
        /// 生成的表前缀，默认为：SharpSword
        /// </summary>
        public string TablePrefix { get; set; }
    }
}
