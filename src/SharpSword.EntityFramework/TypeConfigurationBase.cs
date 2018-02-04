/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 14:47:18
 * ****************************************************************/
using System.Data.Entity.ModelConfiguration;

namespace SharpSword.EntityFramework
{
    /// <summary>
    /// 数据库与实体对象映射配置基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EntityTypeConfigurationBase<T> : EntityTypeConfiguration<T> where T : class
    {
        /// <summary>
        /// 数据库与实体对象映射配置基类
        /// </summary>
        protected EntityTypeConfigurationBase()
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
