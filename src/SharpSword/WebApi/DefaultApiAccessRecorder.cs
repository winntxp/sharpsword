/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/13 18:29:59
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 系统框架默认的访问记录器；默认为一个空实现
    /// </summary>
    public class DefaultApiAccessRecorder : IApiAccessRecorder
    {
        /// <summary>
        /// 默认使用日志记录器
        /// </summary>
        /// <param name="args"></param>
        public void Record(ApiAccessRecorderArgs args) { }
    }
}
