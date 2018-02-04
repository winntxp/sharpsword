/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/21/2016 12:32:25 PM
 * ****************************************************************/
using System.IO;

namespace SharpSword.ViewEngine
{
    /// <summary>
    /// 实体引擎基类
    /// </summary>
    public abstract class ViewEnginePageBase
    {
        /// <summary>
        /// 定义视图执行后需要将结果输出到的数据流对象
        /// </summary>
        public StreamWriter Response { get; set; }

        /// <summary>
        /// 本地化器
        /// </summary>
        public Localizer L
        {
            get
            {
                return ServicesContainer.Current.Resolve<Localization.ITextFormatter>().Get;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected ViewEnginePageBase() { }

        /// <summary>
        /// 页面输出开始
        /// </summary>
        protected virtual void BeginRenderPage()
        {
            //this.Response.WriteLine(this.GetType().Assembly.FullName);
        }

        /// <summary>
        /// 页面输出结束
        /// </summary>
        protected virtual void EndRenderPage()
        {
            //this.Response.WriteLine("");
        }
    }
}
