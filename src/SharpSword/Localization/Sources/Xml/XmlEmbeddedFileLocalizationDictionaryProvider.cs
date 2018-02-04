/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/20/2016 10:19:04 AM
 * ****************************************************************/
using SharpSword.Localization.Dictionaries;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SharpSword.Localization.Sources.Xml
{
    /// <summary>
    /// 内嵌资源语言包提供器
    /// </summary>
    public class XmlEmbeddedFileLocalizationDictionaryProvider : ILocalizationDictionaryProvider
    {
        private readonly Assembly _assembly;
        private readonly string _rootNamespace;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly">包含语言包的程序集</param>
        /// <param name="rootNamespace">内嵌文件夹起始路径</param>
        public XmlEmbeddedFileLocalizationDictionaryProvider(Assembly assembly, string rootNamespace)
        {
            this._assembly = assembly;
            this._rootNamespace = rootNamespace;
        }

        /// <summary>
        /// 获取当前指定内嵌所有合法的本地化资源包，如果文件名称和sourceName相等，则将此资源包设置为默认
        /// </summary>
        /// <param name="sourceName"></param>
        /// <returns></returns>
        public IEnumerable<LocalizationDictionaryInfo> GetDictionaries(string sourceName)
        {
            var dictionaries = new List<LocalizationDictionaryInfo>();

            //获取所有的内嵌资源(并且要以指定的路径开头的)
            var resourceNames = _assembly.GetManifestResourceNames()
                                         .Where(o => o.StartsWith(_rootNamespace)).ToList();

            //读取所有合法的本地语言资源包
            foreach (var resourceName in resourceNames)
            {
                using (var stream = _assembly.GetManifestResourceStream(resourceName))
                {
                    var bytes = stream.GetBytes();
                    var xmlString = Encoding.UTF8.GetString(bytes, 3, bytes.Length - 3);
                    dictionaries.Add(new LocalizationDictionaryInfo(
                                                dictionary: XmlLocalizationDictionary.BuildFomXmlString(xmlString),
                                                isDefault: resourceName.EndsWith(sourceName + ".xml")
                                            ));
                }
            }

            return dictionaries;
        }
    }
}
