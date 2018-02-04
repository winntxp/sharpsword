/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/3/29 11:40:50
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword
{
    /// <summary>
    /// 所有接口所在程序集视图文件查找器（注意此接口只找文本类型的，比如：js,css,aspx,asp,cshtml等）
    /// 此接口属于协作接口，即：注册多个资源查找器系统会依次在各个查找器里进行资源查找
    /// </summary>
    public interface IResourceFinder
    {
        /// <summary>
        /// 优先级排序，数字越大，优先级越高
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// 返回所有程序集内嵌资源信息；此方法的实现最好能够进行缓存机制，即第一次加载到时候描述所有程序集，后续直接从缓存里读取
        /// key:资源文件名称
        /// value:资源文件源代码
        /// </summary>
        /// <returns></returns>
        IDictionary<string, string> GetResources();

        /// <summary>
        /// 获取到资源文件原始文本
        /// </summary>
        /// <param name="resourceViewFullPath">内嵌资源路径</param>
        /// <returns>内嵌资源原始文件(找不的将返回null，所以调用的时候需要注意下null情况)</returns>
        string GetResource(string resourceViewFullPath);
    }
}
