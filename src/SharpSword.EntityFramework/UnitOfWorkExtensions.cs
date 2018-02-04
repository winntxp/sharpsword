/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using SharpSword.Domain.Uow;
using System;
using System.Data.Entity;

namespace SharpSword.EntityFramework
{
    /// <summary>
    /// 我们实现一个基于IunitOfWork的扩展，让系统在运行的时候，自动将数据访问上线下问保存到当前线程集合，来实现跨库事务
    /// </summary>
    internal static class UnitOfWorkExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        public static TDbContext GetDbContext<TDbContext>(this IUnitOfWork unitOfWork) where TDbContext : DbContext
        {
            unitOfWork.CheckNullThrowArgumentNullException(nameof(unitOfWork));
            if (!(unitOfWork is EfUnitOfWork))
            {
                throw new ArgumentException("unitOfWork is not type of " + typeof(EfUnitOfWork).FullName, "unitOfWork");
            }

            return (unitOfWork as EfUnitOfWork).GetOrCreateDbContext<TDbContext>();
        }
    }
}