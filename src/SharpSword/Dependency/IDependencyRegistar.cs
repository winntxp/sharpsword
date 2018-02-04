/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using Autofac;

namespace SharpSword
{
    /// <summary>
    /// 该接口目的让框架自动搜索实现类，然后自动进行接口实现注册到系统
    /// 除了此用法之外，还可以初始化系统是要使用的数据等；
    /// </summary>
    public interface IDependencyRegistar
    {
        /// <summary>
        /// 此方法实现模块注册，系统框架会自动调用此方法就行注册
        /// </summary>
        /// <param name="containerBuilder">IOC容器</param>
        /// <param name="typeFinder">类型查找器</param>
        /// <param name="globalConfiguration">系统框架全局配置信息</param>
        void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration);

        /// <summary>
        /// 数字越小，越先注册，这样外部实现类就可以重写覆盖掉系统默认
        /// </summary>
        int Priority { get; }
    }
}
