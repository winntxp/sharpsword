using Autofac;
/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/26 11:59:37
 * ****************************************************************/

namespace SharpSword.RazorEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class DependencyRegistar : IDependencyRegistar
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="typeFinder"></param>
        /// <param name="globalConfiguration">系统框架配置信息</param>
        public void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            containerBuilder.RegisterType<RazorEngine>()
                            .AsImplementedInterfaces()
                            .SingleInstance();
        }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority
        {
            get { return 0; }
        }
    }
}
