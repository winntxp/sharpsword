/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using SharpSword.Domain.Repositories;
using System;

namespace SharpSword.EntityFramework
{
    /// <summary>
    /// �������ж��DBContext�Ͷ���ִ�ʵ�ֵ�ʱ�򣬽������Զ��嵽DbContext�ϣ���ϵͳ����Զ�ѡ���Ӧ�Ĳִ�ʵ��
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRepositoryTypesAttribute : Attribute
    {
        /// <summary>
        /// ��ȡĬ�ϵĲִ��ӿں�ʵ�ֶ�Ӧ
        /// </summary>
        public static AutoRepositoryTypesAttribute Default { get; private set; }

        /// <summary>
        /// �ִ��ӿ�
        /// </summary>
        public Type RepositoryInterface { get; private set; }

        /// <summary>
        /// �ִ��ӿ�ʵ������
        /// </summary>
        public Type RepositoryImplementation { get; private set; }

        /// <summary>
        /// ע����Ĭ�ϵĲִ��ӿں�ʵ�ֶ�Ӧ��ϵ
        /// </summary>
        static AutoRepositoryTypesAttribute()
        {
            Default = new AutoRepositoryTypesAttribute(typeof(IRepository<>), typeof(EfRepository<,>));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repositoryInterface">�ִ��ӿ�</param>
        /// <param name="repositoryImplementation">�ִ��ӿڶ�Ӧ�ľ���ʵ��</param>
        public AutoRepositoryTypesAttribute(Type repositoryInterface, Type repositoryImplementation)
        {
            this.RepositoryInterface = repositoryInterface;
            this.RepositoryImplementation = repositoryImplementation;
        }
    }
}