/* *******************************************************
 * SharpSword zhangliang4629@163.com 10/24/2016 3:51:35 PM
 * ****************************************************************/
using System;

namespace SharpSword.Domain.Uow
{
    /// <summary>
    /// 
    /// </summary>
    public interface IActiveUnitOfWork
    {
        /// <summary>
        /// ������Ԫ�ύ��ɴ����¼�
        /// </summary>
        event EventHandler Completed;

        /// <summary>
        /// ������Ԫ�ύʧ�ܴ����¼�
        /// </summary>
        event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <summary>
        /// ��ǰ������Ԫ���ͷź󴥷��¼�
        /// </summary>
        event EventHandler Disposed;

        /// <summary>
        /// ������Ԫ����
        /// </summary>
        UnitOfWorkOptions Options { get; }

        /// <summary>
        /// ��ǰ������Ԫ�Ƿ��Ѿ��ͷ�
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// �����ֹ��ύ���ٸı䣨һ�����ڵ�������Ҫ��ʱ��ȡ���ݿ�����ֵ��ʱ�����һ�Σ�
        /// </summary>
        void SaveChanges();
    }
}