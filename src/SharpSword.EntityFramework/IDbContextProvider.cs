/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using System.Data.Entity;

namespace SharpSword.EntityFramework
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    internal interface IDbContextProvider<out TDbContext> where TDbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        TDbContext DbContext { get; }
    }
}