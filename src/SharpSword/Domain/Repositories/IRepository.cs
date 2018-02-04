/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 14:47:30
 * ****************************************************************/
using SharpSword.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SharpSword.Domain.Repositories
{
    /// <summary>
    /// 仓储标识接口
    /// </summary>
    public interface IRepository : IDependency { }

    /// <summary>
    /// Repository
    /// 里泛型仓储的约束我们没有采取泛型实体，而是直接采取空的实体基类，
    /// 是为了让我们再业务层调用仓储层的时候方便，如果我们定义成泛型实体基类，
    /// 那么我们的泛型仓储接口就必须传入2个参数，一个实体类型，
    /// 一个实体的ID值类型，这样增加了开发人员的负担
    /// </summary>
    public interface IRepository<TEntity> : IRepository where TEntity : Entity
    {
        /// <summary>
        /// 根据实体ID获取实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(object id);

        /// <summary>
        /// 根据查询条件获取实体对象
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取总记录数
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// 根据查询条件获取符合条件的对象数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 新增实体对象
        /// </summary>
        /// <param name="entity">Entity</param>
        void Add(TEntity entity);

        /// <summary>
        /// 新增实体对象
        /// </summary>
        /// <param name="entities">Entities</param>
        void Add(IEnumerable<TEntity> entities);

        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(TEntity entity);

        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="entities">Entities</param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// 删除实体对象
        /// </summary>
        /// <param name="entity">Entity</param>
        void Remove(TEntity entity);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entities">Entities</param>
        void Remove(IEnumerable<TEntity> entities);

        /// <summary>
        /// 根据主键删除实体
        /// </summary>
        /// <param name="id"></param>
        void Remove(object id);

        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="predicate"></param>
        void Remove(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取所有数据集合
        /// </summary>
        IQueryable<TEntity> Table { get; }

        /// <summary>
        /// 获取集合实体数据，此集合不会跟踪实体的变化，因此在查询的时候会效率比较高
        /// 用在我们后续不需要做任何更改的情况
        /// </summary>
        IQueryable<TEntity> TableNoTracking { get; }
    }
}
