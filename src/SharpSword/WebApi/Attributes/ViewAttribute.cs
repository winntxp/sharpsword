using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 用于指定接口映射的路径(注意此路径仅仅会读取资源缓存)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ViewAttribute : Attribute
    {
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">接口view路径，如，(本地)：~/view/index.aspx 或者  内嵌：SharpSword.WebApi.View.index.aspx</param>
        public ViewAttribute(string path)
        {
            path.CheckNullThrowArgumentNullException(nameof(path));
            this.Path = path;
        }
    }
}
