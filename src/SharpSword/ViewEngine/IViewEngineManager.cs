/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/13/2016 9:05:02 AM
 * ****************************************************************/
using System.Collections.Generic;
using System.Text;

namespace SharpSword.ViewEngine
{
    /// <summary>
    /// 视图引擎管理器
    /// </summary>
    public interface IViewEngineManager
    {
        /// <summary>
        /// 获取所有的视图引擎
        /// </summary>
        IEnumerable<IViewEngine> ViewEngines { get; }

        /// <summary>
        /// 编译视图文件并执行视图
        /// </summary>
        /// <param name="viewPath">视图文件路径，比如：g:\\temp\t.aspx 或者 ~/temp/t.aspx</param>
        /// <param name="parameters">视图定义的参数集合</param>
        /// <param name="encode">视图文件文件编码</param>
        /// <returns>返回视图执行结果字符串</returns>
        string CompileByViewPath(string viewPath, IViewParameterCollection parameters, Encoding encode);

        /// <summary>
        /// 编译视图文件并执行视图
        /// </summary>
        /// <param name="viewSource">视图文件源码</param>
        /// <param name="parameters">视图定义的参数集合</param>
        /// <param name="encode">视图文件文件编码</param>
        /// <returns>编译视图源代码，并将视图执行结果返回</returns>
        string CompileByViewSource(string viewSource, IViewParameterCollection parameters, Encoding encode);
    }
}
