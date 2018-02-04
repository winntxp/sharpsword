/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/7/12 9:43:08
 * ****************************************************************/
using SharpSword.Serializers.Json;

namespace SharpSword.Serializers
{
    /// <summary>
    /// JSON序列化管理器
    /// </summary>
    public class JsonSerializerManager
    {
        /// <summary>
        /// 当前注册的序列化对象
        /// </summary>
        private static IJsonSerializer _provider;

        /// <summary>
        /// 系统框架默认注册一个序列化对象
        /// </summary>
        static JsonSerializerManager()
        {
            _provider = new DefaultJsonSerializer();
        }

        /// <summary>
        /// 获取或设置序列化器
        /// </summary>
        public static IJsonSerializer Provider
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
