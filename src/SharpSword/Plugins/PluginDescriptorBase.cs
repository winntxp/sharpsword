/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/5/27 12:26:22
 * ****************************************************************/
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SharpSword
{
    /// <summary>
    /// 接口创建描述基类
    /// </summary>
    public abstract class PluginDescriptorBase : IPluginDescriptor
    {
        /// <summary>
        /// 资源查找器
        /// </summary>
        private readonly IResourceFinderManager _resourceFinderManager;

        /// <summary>
        /// 过滤掉一些系统自带的dll
        /// </summary>
        private const string AssemblySkipPattern = "^mscorlib"; // "^mscorlib|^System,|System.Xml,|System.Core,|System.Web,";

        /// <summary>
        /// 插件默认的LOGO地址
        /// </summary>
        protected const string DefaultLogoUrl = "/GetResource?resourceName={0}";

        /// <summary>
        /// 插件LOGO合法的图片后缀集
        /// </summary>
        private static readonly string[] LogoFileExtensions = new string[] { "jpg", "png", "gif" };

        /// <summary>
        /// 组件描述说明文件
        /// </summary>
        private static readonly string[] ProjectDescriptionFileNames = new string[] {  "Readme.md", "Readme.txt", "ProjectDescription.txt", "PluginDescriptor.txt" };

        /// <summary>
        /// 系统默认的插件logo
        /// </summary>
        private static string DefaultLogo = "{0}.Resource.System.png".With(typeof(IPluginDescriptor).Namespace);

        /// <summary>
        /// 当前插件所属程序集
        /// </summary>
        private readonly Assembly _currenAssembly;

        /// <summary>
        /// 当前插件程序集命名空间
        /// </summary>
        private readonly string _currentAssemblyName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceFinderManager">资源查找器</param>
        protected PluginDescriptorBase(IResourceFinderManager resourceFinderManager)
        {
            this._resourceFinderManager = resourceFinderManager;
            this._currenAssembly = this.GetType().Assembly;
            this._currentAssemblyName = this._currenAssembly.GetName().Name;
        }

        /// <summary>
        /// 插件名称，默认显示当前创建命名空间
        /// </summary>
        public virtual string DisplayName => this._currentAssemblyName;

        /// <summary>
        /// 插件首页地址;默认为空
        /// </summary>
        public virtual string IndexUrl => string.Empty;

        /// <summary>
        /// 插件版本，默认直接获取当前插件程序集版本
        /// </summary>
        public virtual string Version => this._currenAssembly.GetName().Version.ToString();

        /// <summary>
        /// 插件LOGO;采取约定方式，默认会搜索DLL内嵌资源，根目录Logo.jpg,Logo.gif,Logo.Png和Resource/(Logo.jpg,Logo.gif,Logo.Png)
        /// </summary>
        public virtual string Logo
        {
            get
            {
                //Resource目录
                foreach (var fileExtension in LogoFileExtensions)
                {
                    var resourceName = "{0}.Resource.Logo.{1}".With(this._currentAssemblyName, fileExtension);

                    //是否存在LOGO资源缓存
                    var logoBase64String = this._resourceFinderManager.GetResource(resourceName);

                    if (!logoBase64String.IsNullOrEmpty())
                    {
                        return this.GetAssemblyResourceLogoUrl(resourceName);
                    }
                }

                //根目录
                foreach (var fileExtension in LogoFileExtensions)
                {
                    var resourceName = "{0}.Logo.{1}".With(this._currentAssemblyName, fileExtension);

                    //是否存在LOGO资源缓存
                    var logoBase64String = this._resourceFinderManager.GetResource(resourceName);

                    if (!logoBase64String.IsNullOrEmpty())
                    {
                        return this.GetAssemblyResourceLogoUrl(resourceName);
                    }
                }

                //返回默认
                return this.GetAssemblyResourceLogoUrl(DefaultLogo);
            }
        }

        /// <summary>
        /// 获取内嵌的LOGO访问地址
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        protected string GetAssemblyResourceLogoUrl(string resourceName)
        {
            return DefaultLogoUrl.With(resourceName);
        }

        /// <summary>
        /// 插件作者，默认为当前程序集的公司名称特性
        /// </summary>
        public virtual string Author => SharpSwordConsts.Author;

        /// <summary>
        /// 创建描述，使用约定方式，每个插件的根目录放置ProjectDescription.txt或者Resource/ProjectDescription.txt文件，系统框架自动回搜索读取此说明文件
        /// </summary>
        public virtual string Description
        {
            get
            {
                foreach (var projectDescriptionFileName in ProjectDescriptionFileNames)
                {
                    //资源名称
                    var resourceName = "{0}.{1}".With(this._currentAssemblyName, projectDescriptionFileName);

                    //先找根目录
                    var projectDescription = this._resourceFinderManager.GetResource(resourceName);
                    if (!projectDescription.IsNull())
                    {
                        return projectDescription;
                    }

                    //再找Resource文件夹
                    resourceName = "{0}.Resource.{1}".With(this._currentAssemblyName, projectDescriptionFileName);
                    projectDescription = this._resourceFinderManager.GetResource(resourceName);
                    if (!projectDescription.IsNull())
                    {
                        return projectDescription;
                    }
                }

                //还找不到就直接找程序集
                var assemblyDescriptionAttribute = this._currenAssembly.GetCustomAttribute<AssemblyDescriptionAttribute>();
                if (!assemblyDescriptionAttribute.IsNull())
                {
                    return assemblyDescriptionAttribute.Description;
                }

                //实在找不到了，就直接返回空了
                return string.Empty;
            }
        }

        /// <summary>
        /// 项目UI前端排序
        /// </summary>
        public virtual int DisplayIndex => 0;

        /// <summary>
        /// 创建依赖那些程序集，如果不存在则返回一个空的集合
        /// </summary>
        public virtual IEnumerable<string> ReferencedAssemblies
        {
            get { return FilterAssemblies(this._currenAssembly.GetReferencedAssemblies().Select(o => o.FullName)); }
        }

        /// <summary>
        /// 是否支持正式环境移除
        /// </summary>
        public virtual bool Hotswap => false;

        /// <summary>
        /// 过滤一些系统级别的程序集
        /// </summary>
        /// <param name="referencedAssemblies">插件引用的程序集</param>
        /// <returns></returns>
        private static IEnumerable<string> FilterAssemblies(IEnumerable<string> referencedAssemblies)
        {
            return referencedAssemblies.Where(referencedAssembly =>
            !Regex.IsMatch(referencedAssembly, AssemblySkipPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled))
            .ToList();
        }
    }
}
