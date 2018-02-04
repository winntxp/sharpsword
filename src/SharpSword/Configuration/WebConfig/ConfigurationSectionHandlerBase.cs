/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/20 11:06:38
 * ****************************************************************/
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Xml;
using System;

namespace SharpSword.Configuration.WebConfig
{
    /// <summary>
    /// 所有自定义web.config文件section节点配置类，系统框架会在启动的时候会自动注册类实例到IOC容器里(外部系统无需进行注册)
    /// 外部定义的时候，需要另外再定义个配置接口，让具体的节点处理类来实现配置接口，这样外部类可以直接使用配置接口即可，系统框架
    /// 会自动对配置进行初始化传入
    /// </summary>
    public abstract class ConfigurationSectionHandlerBase : IWebConfigConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        protected ConfigurationSectionHandlerBase() { }

        /// <summary>
        /// 获取节点属性集合，注意键不会区分大小写
        /// </summary>
        /// <param name="node">当前node节点</param>
        /// <returns>返回的字典key键不区分大小写</returns>
        protected IDictionary<string, string> GetNodeAttributes(XmlNode node)
        {
            var nodeAttributes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (node.IsNull())
            {
                return nodeAttributes;
            }

            int arrtCount = node.Attributes.Count;
            for (int i = 0; i < arrtCount; i++)
            {
                var arr = node.Attributes[i];
                nodeAttributes.Add(arr.Name, arr.Value);
            }

            return nodeAttributes;
        }

        /// <summary>
        /// 根据字典获取指定值
        /// </summary>
        /// <param name="nodeAttributes">数据字典</param>
        /// <param name="attributeName">属性名称，忽略大小写</param>
        /// <returns></returns>
        protected string GetNodeAttribute(IDictionary<string, string> nodeAttributes, string attributeName)
        {
            return nodeAttributes.ContainsKey(attributeName) ? nodeAttributes[attributeName] : null;
        }

        /// <summary>
        /// 绑定属性到当前对象属性
        /// </summary>
        /// <param name="ignoreCaseNodeAttributes"></param>
        /// <returns></returns>
        protected object ToObject(IDictionary<string, string> ignoreCaseNodeAttributes)
        {
            //获取当前配置文件的所有属性信息
            foreach (var property in this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (!property.CanWrite)
                {
                    continue;
                }

                //忽略掉日志，缓存属性
                if (typeof(ILogger).IsAssignableFrom(property.PropertyType) || typeof(ICacheManager).IsAssignableFrom(property.PropertyType))
                {
                    continue;
                }

                //配置文件未配置此属性
                if (ignoreCaseNodeAttributes.ContainsKey(property.Name))
                {
                    var value = ignoreCaseNodeAttributes[property.Name];

                    //待转换成的数据类型
                    var convertType = property.PropertyType;
                    //判断下映射实体属性是否是可空类型;是空类型需要特殊处理
                    if (property.PropertyType.IsNullable())
                    {
                        var nullableConverter = new NullableConverter(property.PropertyType);
                        convertType = nullableConverter.UnderlyingType;
                    }

                    //对异常不处理
                    try
                    {
                        property.SetValue(this, Convert.ChangeType(value, convertType));
                    }
                    catch (Exception exc)
                    {
                        var logger = ServicesContainer.Current.Resolve<ILogger<ConfigurationSectionHandlerBase>>();
                        if (logger.IsEnabled(LogLevel.Warning))
                        {
                            logger.Warning(exc, exc.Message);
                        }
                    }
                }
            }

            return this;
        }

        /// <summary>
        /// 根据节点XML文件，创建出配置低、对象
        /// </summary>
        /// <param name="parent">当前节点对应的父节点对象</param>
        /// <param name="configContext">配置文件上下文</param>
        /// <param name="section">配置文件对应的节点XML文件</param>
        /// <returns>返回当前处理对象</returns>
        public virtual object Create(object parent, object configContext, XmlNode section)
        {
            return this.ToObject(this.GetNodeAttributes(section));
        }
    }
}
