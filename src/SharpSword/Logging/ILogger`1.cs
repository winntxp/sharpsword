/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/15/2016 10:36:30 AM
 * ****************************************************************/

namespace SharpSword
{
    /// <summary>
    /// 泛型日志记录器，可以省略掉ILoggerFactory接口实现，在需要记录日志的类里，直接定义产生日志的TServiceType
    /// </summary>
    /// <typeparam name="TServiceType">记录日志的当前类型</typeparam>
    public interface ILogger<out TServiceType> : ILogger where TServiceType : class { }
}
