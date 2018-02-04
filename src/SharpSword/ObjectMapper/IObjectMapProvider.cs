/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 7/13/2016 4:31:36 PM
 * ****************************************************************/
using System;
using System.Linq.Expressions;

namespace SharpSword
{
    /// <summary>
    /// 对象映射接口，方便后期对接口进行优化
    /// </summary>
    public interface IObjectMapProvider
    {
        /// <summary>
        /// 将匿名的所有对象映射到指定的对象；映射过程中，只要数据类型键可以相互转换，无需2个转换对象属性类型完全一致
        /// </summary>
        /// <typeparam name="T">指定需要转换的类型;实体对象必须带无参构造函数（我们限定为无参构造函数是因为让创建对象的时候，无需反射，实际当中，我们也是针对DTO进行映射）</typeparam>
        /// <param name="sourceObject">是否忽略属性名称大小写</param>
        /// <param name="ignoreCase">待转换类型anonymousObject=null的时候返回null，创建T类型的时候失败也会返回null，请注意转换结果null的判断</param>
        /// <param name="skipPropertyNames">跳过那些属性不赋值(此属性为T类型属性集合)</param>
        /// <returns></returns>
        T MapTo<T>(object sourceObject, bool ignoreCase = true, Expression<Func<T, dynamic>> skipPropertyNames = null) where T : new();
    }
}
