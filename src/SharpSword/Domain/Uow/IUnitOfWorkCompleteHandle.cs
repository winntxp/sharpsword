/* *******************************************************
 * SharpSword zhangliang4629@163.com 10/24/2016 3:51:35 PM
 * ****************************************************************/
using System;

namespace SharpSword.Domain.Uow
{
    /// <summary>
    /// �Թ�����Ԫ�����ύ
    /// ע�⣺�˽ӿڲ��ܽ���IOCע��
    /// </summary>
    public interface IUnitOfWorkCompleteHandle : IDisposable
    {
        /// <summary>
        /// �ύ���б�����Լ������������������
        /// </summary>
        void Complete();
    }
}