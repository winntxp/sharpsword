/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 14:47:16
 * ****************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace SharpSword.Data
{
    /// <summary>
    /// 方便直接使用SQL语句，在实际的开发中，根据实际需要进行使用
    /// </summary>
    public interface IDbContext : IDisposable
    {
        /// <summary>
        /// 返回当前的数据库连接信息，为第三方针对ADO.NET扩展提供接口(比如:Dapper等)
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// 创建一个新的查询参数
        /// </summary>
        /// <returns></returns>
        IDataParameter CreateParameter();

        /// <summary>
        /// 对应的数据库是否存在(定义此属性主要是为了，我们在系统初始化，升级，需要更新表结构或者新增字段等等需求)
        /// </summary>
        bool DataBaseExists { get; }

        /// <summary>
        /// 获取数据库名称
        /// </summary>
        string DbName { get; }

        /// <summary>
        /// 数据源，数据库服务器连接字符串，如：localhost
        /// </summary>
        string DataSource { get; }

        /// <summary>
        /// 使用示例：
        /// 1.context.Query{Post}("SELECT * FROM dbo.Posts WHERE Author = @p0",new object[]{ userSuppliedAuthor})
        /// 2.context.Query{Post}("SELECT * FROM dbo.Posts WHERE Author = @author",new object[]{ new SqlParameter("@author",userSuppliedAuthor)})
        /// 说明：如果使用第二种方式，请不要在代码里直接构造类型的Parameter，而应该使用方法IDbContext.CreateParameter()来创建一个新的参数
        /// </summary>
        /// <typeparam name="TElement">用于接收查询返回值的DTO对象</typeparam>
        /// <param name="sql">sql查询语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>返回接收对象集合</returns>
        IEnumerable<TElement> Query<TElement>(string sql, object[] parameters = null) where TElement : new();

        /// <summary>
        /// 使用示例：
        /// 1.context.DynamicQuery("SELECT * FROM dbo.Posts WHERE Author = @p0", new object[]{ userSuppliedAuthor})
        /// 2.context.DynamicQuery("SELECT * FROM dbo.Posts WHERE Author = @author",new object[]{ new SqlParameter("@author",userSuppliedAuthor)}))
        /// 说明：如果使用第二种方式，请不要在代码里直接构造类型的Parameter，而应该使用方法IDbContext.CreateParameter()来创建一个新的参数
        /// </summary>
        /// <param name="sql">sql查询语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>返回基于动态定义的类型可枚举集合</returns>
        IEnumerable DynamicQuery(string sql, object[] parameters = null);

        /// <summary>
        /// 使用示例：
        /// 1.context.Execute("UPDATE dbo.Posts SET Rating = 5 WHERE Author = @p0", new object[]{ userSuppliedAuthor}); Alternatively,
        /// 2.context.Execute("UPDATE dbo.Posts SET Rating = 5 WHERE Author = @author", new SqlParameter("@author",userSuppliedAuthor));
        /// 说明：如果使用第二种方式，请不要在代码里直接构造类型的Parameter，而应该使用方法IDbContext.CreateParameter()来创建一个新的参数
        /// </summary>
        /// <param name="sql">sql查询语句</param>
        /// <param name="useTransaction">true - 使用事务; false - 不使用事务</param>
        /// <param name="timeout">SQL语句执行超时时间</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>返回操作受影响的数据库行数</returns>
        int Execute(string sql, bool useTransaction = false, int? timeout = null, object[] parameters = null);

        /// <summary>
        /// 使用示例：
        /// 1.context.ExecuteScalar{int}("SELECT COUNT(*) FROM dbo.Posts WHERE Author = @p0", new object[]{ userSuppliedAuthor});
        /// 2.context.ExecuteScalar{int}("SELECT COUNT(*) FROM dbo.Posts WHERE Author = @author", new SqlParameter("@author",userSuppliedAuthor));
        /// 说明：如果使用第二种方式，请不要在代码里直接构造类型的Parameter，而应该使用方法IDbContext.CreateParameter()来创建一个新的参数
        /// </summary>
        /// <typeparam name="T">基元类型，如：string,int,long,datetime</typeparam>
        /// <param name="sql">sql查询语句</param>
        /// <param name="useTransaction">true - 使用事务; false - 不使用事务</param>
        /// <param name="timeout">SQL语句执行超时时间</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>获取单个值(对于数据查询就是第一行第一列)</returns>
        T ExecuteScalar<T>(string sql, bool useTransaction = false, int? timeout = null, object[] parameters = null);

        /// <summary>
        /// 执行存储过程，在执行此方法的时候，我们需要配合IDataProvider接口一起使用，业务不代表所有的数据库都
        /// 支持存储过程，此方法跟多的是在我们技术选型确定的情况下，或者开发通用产品的时候根据IDataProvider.StoredProceduredSupported属性来判断使用。慎用。
        /// </summary>
        /// <typeparam name="TElement">用于接收查询返回值的DTO对象</typeparam>
        /// <param name="storedProcedureName">存储过程名称</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>返回接收对象集合</returns>
        IEnumerable<TElement> ExecuteStoredProcedure<TElement>(string storedProcedureName, object[] parameters = null) where TElement : new();
    }
}
