/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/14/2016 10:22:23 AM
 * ****************************************************************/
using System;
using System.Linq;
using System.Reflection;

namespace SharpSword.Configuration.WebConfig
{
    /// <summary>
    /// web.config配置文件配置参数获取器
    /// </summary>
    internal class WebConfigSettingFactory : SettingFactoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        public WebConfigSettingFactory() { }

        /// <summary>
        /// 工厂支持处理的数据类型
        /// </summary>
        public override Type Supported
        {
            get { return typeof(IWebConfigConfiguration); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSetting"></typeparam>
        /// <exception cref="SharpSwordCoreException">web.config里没有找到对应的section节点</exception>
        /// <returns></returns>
        public override TSetting Get<TSetting>()
        {
            //获取当前配置参数类型
            var type = typeof(TSetting);

            //默认使用类名称作为配置节点
            var sectionName = type.Name;

            //改变下注册策略，当配置类含有ConfigurationSectionNameAttribute特性的时候，就自动进行注册，未配置就不自动注册，需要手工获取
            if (type.IsDefined(typeof(ConfigurationSectionNameAttribute)))
            {
                //配置对象映射的节点名称特性
                var configurationSectionNameAttribute =
                    (ConfigurationSectionNameAttribute)
                    type.GetCustomAttributes(typeof(ConfigurationSectionNameAttribute)).FirstOrDefault();

                //节点名称
                sectionName = configurationSectionNameAttribute.SectionName;
            }

            //web.config节点，映射到配置对象
            var sectionConfiguration = ConfigurationSectionManager.GetSection(sectionName);

            //web.confg对应的节点没有配置，系统框架直接抛出异常，这样编译使用者立即发现问题(开发阶段就将异常错误排除掉)
            if (sectionConfiguration.IsNull())
            {
                //排除掉接口定义
                var propertyValues = type.GetProperties()
                                         .Where(p => !p.PropertyType.IsInterface)
                                         .Select(p => "{0}=\"{1}\"".With(p.Name, ""))
                                         .JoinToString(" ");

                //抛出异常，方便开发人员直接复制错误，添加到web.config文件
                throw new SharpSwordCoreException(
                   "未找到web.config配置节点：\r\n<configuration>\r\n\t<configSections>\r\n<section name=\"{0}\" type=\"{1},{2}\" />\r\n\t</configSections>\r\n <{0} {3} />\r\n</configuration>".With(
                        sectionName, type.FullName, type.Assembly.GetName().Name, propertyValues));
            }

            //返回配置参数对象
            return (TSetting)sectionConfiguration;
        }
    }
}
