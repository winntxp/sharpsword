/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/20 11:02:30
 * ****************************************************************/
using SharpSword.Configuration.WebConfig;
using System;
using System.Xml;

namespace SharpSword.Caching.Memcached
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable, ConfigurationSectionName("sharpsword.module.cachemanager.memcached")]
    public class MemcachedManagerConfig : ConfigurationSectionHandlerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="configContext"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public override object Create(object parent, object configContext, XmlNode section)
        {
            var config = new MemcachedManagerConfig
            {
                Servers = null
            };

            if (section.IsNull())
            {
                return config;
            }

            string serversAttrValue;
            if (this.GetNodeAttributes(section).TryGetValue("servers", out serversAttrValue))
            {
                config.Servers = serversAttrValue.Split(new char[] { ',' });
            }

            return config;
        }

        /// <summary>
        /// 服务器地址列表，多服务器使用,分开
        /// </summary>
        public string[] Servers
        {
            get;
            private set;
        }
    }
}
