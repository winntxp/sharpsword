/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/3/9 13:47:34
 * ****************************************************************/
using SharpSword.EntityFramework;
using System;
using System.Data.Entity;

namespace SharpSword.DtoGenerator
{
    /// <summary>
    /// 数据库访问上下文
    /// </summary>
    public class DtoGeneratorDbContext : DbContextBase
    {
        /// <summary>
        /// 初始化数据访问上下文对象
        /// </summary>
        /// <param name="getNameOrConnectionString">数据库连接名</param>
        public DtoGeneratorDbContext(Func<string> getNameOrConnectionString)
            : base(getNameOrConnectionString())
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}