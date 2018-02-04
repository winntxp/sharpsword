/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 03/07/2017 4:31:36 PM
 * ****************************************************************/
using SharpSword.ObjectMapper;
using System;
using System.Linq.Expressions;

namespace SharpSword
{
    /// <summary>
    /// 对象映射管理器
    /// </summary>
    public class ObjectMapManager
    {
        /// <summary>
        /// 当前注册的映射器对象
        /// </summary>
        private static IObjectMapProvider _provider;

        /// <summary>
        /// 我们默认使用系统框架默认的对应映射器
        /// </summary>
        static ObjectMapManager()
        {
            _provider = new DefaultObjectMapProvider();
        }

        /// <summary>
        /// 当前注册的对应映射提供者，当然我们后期也可以在系统启动的时候，修改默认的对象映射提供者
        /// </summary>
        public static IObjectMapProvider Provider
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

        /// <summary>
        /// 对象映射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceObject">指定需要转换的类型;实体对象必须带无参构造函数</param>
        /// <param name="ignoreCase">是否忽略属性名称大小写</param>
        /// <param name="skipPropertyNames">跳过那些属性不赋值(此属性为T类型属性集合)</param>
        /// <returns></returns>
        public static T MapTo<T>(object sourceObject, bool ignoreCase = true, Expression<Func<T, dynamic>> skipPropertyNames = null) where T : new()
        {
            return Provider.MapTo<T>(sourceObject, ignoreCase, skipPropertyNames);
        }
    }
}
