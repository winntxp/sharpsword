/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using SharpSword.Domain.Repositories;
using System;

namespace SharpSword.EntityFramework
{
    /// <summary>
    /// 当我们有多个DBContext和多个仓储实现的时候，将此特性定义到DbContext上，让系统框架自动选择对应的仓储实现
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRepositoryTypesAttribute : Attribute
    {
        /// <summary>
        /// 获取默认的仓储接口和实现对应
        /// </summary>
        public static AutoRepositoryTypesAttribute Default { get; private set; }

        /// <summary>
        /// 仓储接口
        /// </summary>
        public Type RepositoryInterface { get; private set; }

        /// <summary>
        /// 仓储接口实现类型
        /// </summary>
        public Type RepositoryImplementation { get; private set; }

        /// <summary>
        /// 注册下默认的仓储接口和实现对应关系
        /// </summary>
        static AutoRepositoryTypesAttribute()
        {
            Default = new AutoRepositoryTypesAttribute(typeof(IRepository<>), typeof(EfRepository<,>));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repositoryInterface">仓储接口</param>
        /// <param name="repositoryImplementation">仓储接口对应的具体实现</param>
        public AutoRepositoryTypesAttribute(Type repositoryInterface, Type repositoryImplementation)
        {
            this.RepositoryInterface = repositoryInterface;
            this.RepositoryImplementation = repositoryImplementation;
        }
    }
}