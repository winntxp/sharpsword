/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/3/1 10:22:47
 * ****************************************************************/
using SharpSword.WebApi;
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.AccessRecorder.Dispatcher
{
    /// <summary>
    /// 用于测试记录访问接口
    /// </summary>
    public class ApiAccessRecorder : IApiAccessRecorder
    {
        /// <summary>
        /// 我们不采取构造函数注入，采取属性注入，构造函数注入会引起循环依赖问题
        /// http://docs.autofac.org/en/latest/advanced/circular-dependencies.html
        /// </summary>
        public IEnumerable<IApiAccessRecorder> ApiAccessRecorders { get; set; }

        /// <summary>
        /// 默认初始化空的日志接口
        /// </summary>
        public ApiAccessRecorder()
        {
            this.ApiAccessRecorders = new List<IApiAccessRecorder>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public void Record(ApiAccessRecorderArgs args)
        {
            this.ApiAccessRecorders.CheckNullThrowArgumentNullException(nameof(ApiAccessRecorders));

            //循环所有注册的记录器，排除掉当前自己
            foreach (var item in this.ApiAccessRecorders.Where(x => !(x is ApiAccessRecorder)))
            {
                item.Record(args);
            }
        }
    }
}