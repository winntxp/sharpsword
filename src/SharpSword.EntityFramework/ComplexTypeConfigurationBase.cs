/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/30/2016 11:06:20 AM
 * ****************************************************************/
using System.Data.Entity.ModelConfiguration;

namespace SharpSword.EntityFramework
{
    /// <summary>
    /// 使用复杂类型需要注意：
    /// 1.没有主键列；
    /// 2.实例不能重复（Person类里只能有一个Address实例）；
    /// 3.只能是单实例，不能是一个集合（Person类里是Address单实例）。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ComplexTypeConfigurationBase<T> : ComplexTypeConfiguration<T> where T : class
    {
        /// <summary>
        /// 数据库与实体对象映射配置基类
        /// </summary>
        protected ComplexTypeConfigurationBase()
        {
            this.PostInitialize();
        }

        /// <summary>
        /// 初始化的时候，做一些事情；配置类里重写
        /// </summary>
        protected virtual void PostInitialize()
        {
        }
    }
}
