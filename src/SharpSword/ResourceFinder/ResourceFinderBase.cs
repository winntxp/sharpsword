/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/16 10:12:06
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSword
{
    /// <summary>
    /// 资源文件读取器
    /// </summary>
    public abstract class ResourceFinderBase : IResourceFinder
    {
        /// <summary>
        /// 读取内嵌资源，指定扩展名；系统框架默认读取：.aspx，.t ,系统框架不区分后缀大小写
        /// 为了安全调用，此属性只能在重写类里重写（框架采取约定的方式）
        /// </summary>
        protected virtual string[] SupportedFileExtensions
        {
            get
            {
                return new string[] { ".aspx", ".asp", ".ascx", ".master", ".js", ".css", ".cshtml", "vbhtml", ".html", ".htm", ".shtml", ".txt", ".xml" };
            }
        }

        /// <summary>
        /// 获取全部资源
        /// </summary>
        /// <returns>获取所有文本资源信息</returns>
        public abstract IDictionary<string, string> GetResources();

        /// <summary>
        /// 获取文本类型资源文件源码
        /// </summary>
        /// <param name="resourceFullPath">针对内嵌资源或者本地资源路径</param>
        /// <returns></returns>
        public string GetResource(string resourceFullPath)
        {
            //当前接口视图文件忽略大小写
            var viewResource = this.GetResources().FirstOrDefault(o => o.Key.Equals(resourceFullPath, StringComparison.OrdinalIgnoreCase));

            //不存在直接进行下一个
            if (!viewResource.IsNull() && !viewResource.Key.IsNullOrEmpty())
            {
                return viewResource.Value;
            }

            //获取空
            return null;
        }

        /// <summary>
        /// 优先级；默认int.Minvalue，最低
        /// </summary>
        public virtual int Priority
        {
            get { return int.MinValue; }
        }
    }
}
