/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/17/2016 2:31:31 PM
 * ****************************************************************/
using Autofac;

namespace SharpSword
{
    /// <summary>
    /// 分片段IOC注册继承基类
    /// </summary>
    public abstract class DependencyRegistarBase : IDependencyRegistar
    {
        /// <summary>
        /// 系统框架级DependencyRegistar注册顺序
        /// </summary>
        protected int DefaultPriority => int.MinValue + 9999;

        /// <summary>
        /// 注册先后顺序，优先级越高数字越大(具体到注册就越后注册，因为注册后注册会覆盖前面注册的)
        /// 我们约定：系统框架级别的注册，使用此默认值，无需重写；扩展模块级别的注册，请从：0 开始
        /// </summary>
        public virtual int Priority => this.DefaultPriority;

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="typeFinder"></param>
        /// <param name="globalConfiguration"></param>
        public abstract void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration);
    }
}
