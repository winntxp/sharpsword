/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using SharpSword.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;

namespace SharpSword.EntityFramework
{
    /// <summary>
    /// 
    /// </summary>
    internal static class DbContextExtensions
    {
        /// <summary>
        /// 根据数据访问上下文DbContext的类型，获取所有的实体信息
        /// </summary>
        /// <param name="dbContextType"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetEntityTypes(this Type dbContextType)
        {
            return
                from property in dbContextType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                where
                    ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(IDbSet<>)) ||
                    ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(DbSet<>))
                select property.PropertyType.GenericTypeArguments[0];
        }

        /// <summary>
        /// 根据泛型的TDbContext获取定义的DbSet对应的数据表信息
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <returns></returns>
        public static IEnumerable<Type> GetEntityTypes<TDbContext>() where TDbContext : DbContextBase
        {
            return GetEntityTypes(typeof(TDbContext));
        }

        /// <summary>
        /// 获取实体对应的表映射名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetTableName<T>(this IDbContext context)
        {
            if (!(context is IObjectContextAdapter))
            {
                throw new SharpSwordCoreException("参数:{0}必须实现:IObjectContextAdapter", nameof(context));
            }
            var adapter = ((IObjectContextAdapter)context).ObjectContext;
            var storageModel = (StoreItemCollection)adapter.MetadataWorkspace.GetItemCollection(DataSpace.SSpace);
            var containers = storageModel.GetItems<EntityContainer>();
            var entitySetBase = containers.SelectMany(c => c.BaseEntitySets.Where(bes => bes.Name == typeof(T).Name)).First();
            return entitySetBase.MetadataProperties.First(p => p.Name == "Table").Value.ToString();
        }
    }
}