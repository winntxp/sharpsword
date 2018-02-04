/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/16/2016 9:47:33 AM
 * ****************************************************************/

namespace SharpSword.Domain.Services
{
    /// <summary>
    /// 标注一个类是一个服务类，标注有次接口的实现类所有方法都会是工作单元方法
    /// 实现此接口的类，系统框架将会自动进行注册，注册行为为：线程单例
    /// </summary>
    public interface ISharpSwordServices : IEnableInterfaceInterceptor, IPerLifetimeDependency { }
}
