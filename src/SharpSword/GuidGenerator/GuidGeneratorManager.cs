/* **************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/27/2016 1:58:26 PM
 * **************************************************************/
using SharpSword.GuidGenerator.Impl;

namespace SharpSword.GuidGenerator
{
    /// <summary>
    /// GUID生成器全局管理器，开发过程中，尽量使用此管理器，获取生成器提供者
    /// </summary>
    public class GuidGeneratorManager
    {
        /// <summary>
        /// 当前注册的映射器对象
        /// </summary>
        private static IGuidGenerator _provider;

        /// <summary>
        /// 我们默认使用系统框架默认的对应映射器
        /// </summary>
        static GuidGeneratorManager()
        {
            _provider = SequentialGuidGenerator.Instance;
        }

        /// <summary>
        /// 当前注册的对应映射提供者，当然我们后期也可以在系统启动的时候，修改默认的对象映射提供者
        /// </summary>
        public static IGuidGenerator Provider
        {
            get
            {
                return _provider;
            }
            set
            {
                value.CheckNullThrowArgumentNullException(nameof(Provider));
                _provider = value;
            }
        }

    }
}
