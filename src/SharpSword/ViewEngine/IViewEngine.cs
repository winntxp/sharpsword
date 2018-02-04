/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/17/2016 12:46:13 PM
 * ****************************************************************/
using System.Text;

namespace SharpSword.ViewEngine
{
    /// <summary>
    /// 系统框架视图引擎接口，注意此接口为多实现协作接口，即：外部多个实现系统框架都会认为合法，并且会依次循环所有实现
    /// </summary>
    public interface IViewEngine
    {
        /// <summary>
        /// 支持的后缀，格式必须如：.cshtml或者.aspx或者 .xxx；此属性请实现为当只有编译指定路径的视图模板的时候器作用即可，编译源代码的可以忽略
        /// </summary>
        string SupportedExtension { get; }

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
