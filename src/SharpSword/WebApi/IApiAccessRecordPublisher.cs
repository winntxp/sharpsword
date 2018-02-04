/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/18 11:44:59
 * ****************************************************************/
using SharpSword.Timing;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 接口日志发布器
    /// </summary>
    public interface IApiAccessRecordPublisher
    {
        /// <summary>
        /// 发布接口日志
        /// </summary>
        /// <param name="actionResultString">请求接口返回的结果字符串有可能是XML/JSON/HTML</param>
        /// <param name="requestContext">当前请求上下文</param>
        /// <param name="actionLifeTime">接口执行的生命周期时间</param>
        void Publish(string actionResultString, RequestContext requestContext, IDateTimeRange actionLifeTime);
    }
}
