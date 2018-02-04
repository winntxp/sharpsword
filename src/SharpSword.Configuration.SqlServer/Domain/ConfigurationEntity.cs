/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 16:01:31
 * ****************************************************************/
using System;
using SharpSword.Domain.Entitys;

namespace SharpSword.Configuration.SqlServer.Domain
{
    /// <summary>
    /// 数据库对象
    /// </summary>
    [Serializable]
    public class ConfigurationEntity : Entity<string>
    {
        /// <summary>
        /// 用于存储参数配置
        /// </summary>
        public string Value { get; set; }
    }
}
