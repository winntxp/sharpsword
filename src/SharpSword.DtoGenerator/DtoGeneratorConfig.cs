/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 17:05:48
 * ****************************************************************/
using System;
using SharpSword.Configuration.WebConfig;

namespace SharpSword.DtoGenerator
{
    /// <summary>
    /// DTO生成器插件配置信息
    /// </summary>
    [ConfigurationSectionName("sharpsword.module.dtogeneratorconfig"), Serializable]
    public class DtoGeneratorConfig : ConfigurationSectionHandlerBase
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionStringName { get; private set; }

        /// <summary>
        /// 保存源码的位置，为空的时候，将不会主动保存到本地文件
        /// </summary>
        public string SourceSaveDirectory { get; private set; }
    }
}
