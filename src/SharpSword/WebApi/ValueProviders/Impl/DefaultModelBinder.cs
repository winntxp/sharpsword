/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using System;

namespace SharpSword.WebApi.ValueProviders.Impl
{
    /// <summary>
    /// 默认的对象绑定器； 针对简易对象(属性不包含复杂对象)
    /// </summary>
    internal class DefaultModelBinder : IModelBinder
    {
        /// <summary>
        /// 此绑定器未有内部状态，我们使用全局静态
        /// </summary>
        private static DefaultModelBinder _instance = new DefaultModelBinder();

        /// <summary>
        /// 我们直接使用全局静态
        /// </summary>
        public static IModelBinder Instance => _instance;

        /// <summary>
        /// 根据值提供其，绑定对象，自动给参数赋值，使用反射方式
        /// </summary>
        /// <param name="valueProvidersManager">值提供器管理器</param>
        /// <typeparam name="T">待绑定的类型必须要有无参构造函数</typeparam>
        /// <returns></returns>
        public T Bind<T>(IValueProvidersManager valueProvidersManager) where T : new()
        {
            //参数不能为null
            valueProvidersManager.CheckNullThrowArgumentNullException(nameof(valueProvidersManager));

            //创建对象，并且对属性赋值（如果属性值在值提供器里存在并且可以转型成功）
            //T obj = Activator.CreateInstance<T>();
            T obj = new T(); //TODO:2017-08-11 

            //获取对象属性
            var propertys = obj.GetType().GetPropertiesInfo();

            //循环属性从值提供器里进行赋值
            foreach (var property in propertys)
            {
                //从对象里获取数据
                var value = valueProvidersManager.GetValue(property.Name);

                //属性是可写并且不是索引方法的并且数据不为null
                if (value.IsNull() || !property.CanWrite || property.GetIndexParameters().Length > 0)
                {
                    continue;
                }
                try
                {
                    property.SetValue(obj, Convert.ChangeType(value, property.PropertyType), null);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            return obj;
        }
    }
}