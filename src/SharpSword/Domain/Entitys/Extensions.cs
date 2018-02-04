/* *******************************************************
 * SharpSword zhangliang4629@163.com 8/25/2016 2:47:20 PM
 * ****************************************************************/
using System;
using System.Reflection;
//using System.Data.Entity.Core.Objects;

namespace SharpSword.Domain.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 实体是否已经被标记为删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool IsNullOrDeleted(this ISoftDelete entity)
        {
            return entity == null || entity.IsDeleted;
        }

        /// <summary>
        /// <![CDATA[
        /// 
        /// 获取动态代理类的原始基类型，因为EF在运行时会为POCO类自动生成代理类，有
        /// 时候我们需要在运行时知道代理类的真实类型
        /// 在EF里我们可以去看下EntityProxyFactory这个类的源代码，POCO类代理类生成器
        /// 
        /// public static Type GetObjectType(Type type)
        ///    {
        ///        Check.NotNull<Type>(type, "type");
        ///        if (!EntityProxyFactory.IsProxyType(type))
        ///        {
        ///            return type;
        ///        }
        ///        return type.BaseType();
        ///    }
        /// 
        /// ]]>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Type GetUnproxiedEntityType(this IEntity entity)
        {
            // return ObjectContext.GetObjectType(entity.GetType());
            // 我们对EF的ORM会自动生成代理类做一些出来，当然如果我们切换了ORM工具，
            // 可能其他ORM也会生成代理类，那么我们需要再增加判断
            // 上面标注掉的代码是EF源代码里的处理方式,EF删除代理类的类型名称为：
            // System.Data.Entity.DynamicProxies.{POCO类型名称}_{随机字符或者字母}
            if (!entity.GetType().IsProxyType())
            {
                return entity.GetType();
            }

            //返回代理类的基类
            return entity.GetType().GetTypeInfo().BaseType;
        }
    }
}
