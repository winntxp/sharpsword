/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/20/2016 10:19:35 AM
 * ****************************************************************/
using SharpSword.Localization.Dictionaries;
using System.Collections.Generic;
using System.IO;

namespace SharpSword.Localization.Sources.Xml
{
    /// <summary>
    /// 本地文件夹资源包
    /// </summary>
    public class XmlFileLocalizationDictionaryProvider : ILocalizationDictionaryProvider
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly string _directoryPath;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryPath">语言包资源存放路径，虚拟路径(~/Dir)或者物理路径(G:\\Dir)</param>
        public XmlFileLocalizationDictionaryProvider(string directoryPath)
        {
            this._directoryPath = directoryPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceName"></param>
        /// <returns></returns>
        public IEnumerable<LocalizationDictionaryInfo> GetDictionaries(string sourceName)
        {
            //我们转换下虚拟路径
            var directoryPath = HostHelper.MapPath(this._directoryPath);

            //获取文件夹下面的所有XML文件
            var fileNames = Directory.GetFiles(directoryPath, "*.xml", SearchOption.TopDirectoryOnly);

            //加载所有的语言包
            var dictionaries = new List<LocalizationDictionaryInfo>();

            foreach (var fileName in fileNames)
            {
                dictionaries.Add(new LocalizationDictionaryInfo(
                                            dictionary: XmlLocalizationDictionary.BuildFomFile(fileName),
                                            isDefault: fileName.EndsWith(sourceName + ".xml")));
            }

            return dictionaries;
        }
    }
}
