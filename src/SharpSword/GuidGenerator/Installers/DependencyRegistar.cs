/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;

namespace SharpSword.GuidGenerator.Installers
{
    /// <summary>
    /// 注册系统框架默认的GUID生成器
    /// </summary>
    internal class DependencyRegistar : DependencyRegistarBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="typeFinder"></param>
        /// <param name="globalConfiguration"></param>
        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            containerBuilder.Register(c => GuidGeneratorManager.Provider)
                            .As<IGuidGenerator>();
        }
    }
}