/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/13 18:23:49
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 接口访问记录器，用于统计接口访问量和使用情况,外部实现可以将访问信息记录到数据库或者其他存储介质
    /// 注意：此接口是多接口合作接口，即：注册的多个接口都会进行接口访问记录，不会进行覆盖操作；相当于发布/订阅模式
    /// 另外，实现类里需要进行错误错误，对异常需要进行拦截处理，不能出现直接抛出异常的情况
    /// </summary>
    public interface IApiAccessRecorder
    {
        /// <summary>
        /// 记录接口访问
        /// </summary>
        /// <param name="args">接口参数</param>
        void Record(ApiAccessRecorderArgs args);
    }
}
