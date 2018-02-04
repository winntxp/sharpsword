/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using SharpSword.Domain.Uow;
using System;
using System.Data.Entity;

namespace SharpSword.EntityFramework
{
    /// <summary>
    /// ����ʵ��һ������IunitOfWork����չ����ϵͳ�����е�ʱ���Զ������ݷ����������ʱ��浽��ǰ�̼߳��ϣ���ʵ�ֿ������
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