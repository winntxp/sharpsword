/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/24/2016 3:51:35 PM
 * ****************************************************************/
using System;
using System.Transactions;

namespace SharpSword.Domain.Uow
{
    /// <summary>
    /// ���ڱ�ע������ķ�������������Ԫ��������
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class UnitOfWorkAttribute : Attribute
    {
        /// <summary>
        /// �Ƿ���ȫ������
        /// </summary>
        public bool? IsTransactional { get; private set; }

        /// <summary>
        /// ����ʱ����
        /// </summary>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// ָ������ĸ��뼶��
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// �ṩ���ڴ�������Χ�ĸ���ѡ��
        /// </summary>
        public TransactionScopeOption? Scope { get; set; }

        /// <summary>
        /// �Ƿ�����˹�����Ԫ
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isTransactional">Ĭ�ϲ��������񣬵������ÿһ�ε�SaveChange()��ͬ����һ�������</param>
        public UnitOfWorkAttribute(bool isTransactional = false)
        {
            this.IsTransactional = isTransactional;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeout"></param>
        public UnitOfWorkAttribute(int timeout)
        {
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isTransactional"></param>
        /// <param name="timeout"></param>
        public UnitOfWorkAttribute(bool isTransactional, int timeout)
        {
            IsTransactional = isTransactional;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isolationLevel"></param>
        public UnitOfWorkAttribute(IsolationLevel isolationLevel)
        {
            IsTransactional = false;
            IsolationLevel = isolationLevel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <param name="timeout"></param>
        public UnitOfWorkAttribute(IsolationLevel isolationLevel, int timeout)
        {
            IsTransactional = false;
            IsolationLevel = isolationLevel;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        public UnitOfWorkAttribute(TransactionScopeOption scope)
        {
            IsTransactional = false;
            Scope = scope;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="timeout"></param>
        public UnitOfWorkAttribute(TransactionScopeOption scope, int timeout)
        {
            IsTransactional = false;
            Scope = scope;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// ����������Ԫ����
        /// </summary>
        /// <returns></returns>
        internal UnitOfWorkOptions CreateOptions()
        {
            return new UnitOfWorkOptions
            {
                IsTransactional = IsTransactional,
                IsolationLevel = IsolationLevel,
                Timeout = Timeout,
                Scope = Scope
            };
        }
    }
}