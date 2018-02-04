/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/14/2016 10:38:07 AM
 * ****************************************************************/
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SharpSword.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class IDbContextExtensions
    {
        /// <summary>
        /// 创建IDataParameter参数信息
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="anonymousObject">匿名对象new{Author="sharpsword"}或者POCO对象</param>
        /// <returns></returns>
        private static IDataParameter[] BuilderDataParameter(this IDbContext dbContext, object anonymousObject)
        {
            IList<IDataParameter> dataParameters = new List<IDataParameter>();
            var objectAttributes = anonymousObject.GetAttributes();
            foreach (var item in objectAttributes)
            {
                var dataParameter = dbContext.CreateParameter();
                dataParameter.ParameterName = item.Key;
                dataParameter.Value = item.Value;
                dataParameters.Add(dataParameter);
            }
            return dataParameters.ToArray();
        }

        /// <summary>
        /// 使用示例：
        /// context.Execute("SELECT * FROM dbo.Posts WHERE Author = @Author", new{Author="sharpsword"});
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="dbContext"></param>
        /// <param name="sql"></param>
        /// <param name="anonymousObject"></param>
        /// <returns></returns>
        public static IEnumerable<TElement> Query<TElement>(this IDbContext dbContext, string sql, object anonymousObject) where TElement : new()
        {
            var dataParameters = dbContext.BuilderDataParameter(anonymousObject);
            return dbContext.Query<TElement>(sql, dataParameters);
        }

        /// <summary>
        /// 使用示例：
        /// 1.context.Execute("UPDATE dbo.Posts SET Rating = 5 WHERE Author = @p0",new object[]{ userSuppliedAuthor});
        /// 2.context.Execute("UPDATE dbo.Posts SET Rating = 5 WHERE Author = @author", new object[]{ new SqlParameter("@author",userSuppliedAuthor)});
        /// 说明：如果使用第二种方式，请不要在代码里直接构造类型的Parameter，而应该使用方法IDbContext.CreateParameter()来创建一个新的参数，系统会根据当前数据库类型
        /// 创建对应的数据库类型查询参数
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static int Execute(this IDbContext dbContext, string sql, object[] parameters)
        {
            return dbContext.Execute(sql, false, null, parameters);
        }

        /// <summary>
        /// 使用示例：
        /// context.Execute("UPDATE dbo.Posts SET Rating = 5 WHERE Author = @Author", new{Author="sharpsword"});
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="sql"></param>
        /// <param name="anonymousObject">匿名对象或者POCO对象</param>
        /// <returns></returns>
        public static int Execute(this IDbContext dbContext, string sql, object anonymousObject)
        {
            var dataParameters = dbContext.BuilderDataParameter(anonymousObject);
            return dbContext.Execute(sql, dataParameters);
        }

        /// <summary>
        /// 使用示例：
        /// 1.context.ExecuteScalar{int}("SELECT COUNT(*) FROM dbo.Posts WHERE Author = @p0", new object[]{ userSuppliedAuthor});
        /// 2.context.ExecuteScalar{int}("SELECT COUNT(*) FROM dbo.Posts WHERE Author = @author", new object[]{ new SqlParameter("@author",userSuppliedAuthor)});
        /// 说明：如果使用第二种方式，请不要在代码里直接构造类型的Parameter，而应该使用方法IDbContext.CreateParameter()来创建一个新的参数，系统会根据当前数据库类型
        /// 创建对应的数据库类型查询参数
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static T ExecuteScalar<T>(this IDbContext dbContext, string sql, object[] parameters)
        {
            return dbContext.ExecuteScalar<T>(sql, false, null, parameters);
        }

        /// <summary>
        /// 使用示例：
        /// context.Execute("SELECT * FROM dbo.Posts  WHERE Author = @Author", new{Author="sharpsword"});
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbContext"></param>
        /// <param name="sql"></param>
        /// <param name="useTransaction"></param>
        /// <param name="timeout"></param>
        /// <param name="anonymousObject"></param>
        /// <returns></returns>
        public static T ExecuteScalar<T>(this IDbContext dbContext, string sql, object anonymousObject, bool useTransaction = false, int? timeout = null)
        {
            var dataParameters = dbContext.BuilderDataParameter(anonymousObject);
            return dbContext.ExecuteScalar<T>(sql, useTransaction, timeout, dataParameters);
        }
    }
}
