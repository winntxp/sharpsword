/* ****************************************************************
 * SharpSword zhangliang4629@163.com 10/24/2016 3:51:35 PM
 * ****************************************************************/
using System;

namespace SharpSword.Domain.Uow
{
    /// <summary>
    /// ������Ԫ�ӿ�
    /// </summary>
    public interface IUnitOfWork : IActiveUnitOfWork, IUnitOfWorkCompleteHandle
    {
        /// <summary>
        /// ������Ԫ���
        /// </summary>
        string Id { get; }

        /// <summary>
        /// ������Ԫ�������ڹ�����ԪǶ�׹�������ȳ���
        /// </summary>
        IUnitOfWork Outer { get; set; }

        /// <summary>
        /// ����������Ԫ
        /// </summary>
        /// <param name="options"></param>
        void Begin(UnitOfWorkOptions options);
    }
}