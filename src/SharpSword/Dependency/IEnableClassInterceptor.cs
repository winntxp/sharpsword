/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/18/2016 1:43:28 PM
 * ****************************************************************/

namespace SharpSword
{
    /// <summary>
    /// 注册的时候注册成类代理，注意如果需要被代理，需要公开方法定义成虚方法
    /// </summary>
    public interface IEnableClassInterceptor : IEnableInterceptor { }
}
