/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/18/2016 1:43:51 PM
 * ****************************************************************/

namespace SharpSword
{
    /// <summary>
    /// 实现此接口的需要注册为线程内单列
    /// </summary>
    public interface IPerLifetimeDependency : IDependency { }
}
