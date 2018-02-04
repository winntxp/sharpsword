/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/13/2017 8:53:44 AM
 * ****************************************************************/
using System;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 系统报警触发器（具体实现里，需要进行异步操作，不要影响到主线程的流程），具体采取短信或者邮件的方式，我们交给具体实现去做
    /// </summary>
    /// <typeparam name="TSource">消息触发源</typeparam>
    public interface ISystemWarningTrigger
    {
        /// <summary>
        /// 触发报警
        /// </summary>
        /// <param name="source">触发源</param>
        /// <param name="waningMessage">警告消息</param>
        /// <param name="exception">异常,方便我们将捕获到的异常发送给相关人员</param>
        void Warning(object source, string waningMessage, Exception exception = null);
    }
}
