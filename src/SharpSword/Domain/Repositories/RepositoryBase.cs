/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 14:47:30
 * ****************************************************************/
using SharpSword.Domain.Entitys;
using SharpSword.Events.Entitys;
using SharpSword.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SharpSword.Domain.Repositories
{
    /// <summary>
    /// 泛型仓库实现基类
    /// </summary>
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// 当前登录用户信息
        /// </summary>
        public ISession Session { get; set; }

        /// <summary>
        /// 实体事件处理
        /// </summary>
        public IEntityEventHelper EntityEventHelper { get; set; }

        /// <summary>
        /// 默认构造一个空的记录器
        /// </summary>
        protected RepositoryBase()
        {
            this.Session = NullSession.Instance;
            this.EntityEventHelper = NullEntityChangedEventHelper.Instance;
        }

        /// <summary>
        /// 根据实体ID获取实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract TEntity Get(object id);

        /// <summary>
        /// 根据查询表达式获取对象
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Table.FirstOrDefault(predicate);
        }

        /// <summary>
        /// 获取总记录数
        /// </summary>
        /// <returns></returns>
        public virtual int Count()
        {
            return this.Table.Count();
        }

        /// <summary>
        /// 根据查询表达式搜索符合条件的数据条数
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Table.Where(predicate).Count();
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="entity">Entity</param>
        public abstract void Add(TEntity entity);

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="entities"></param>
        public virtual void Add(IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
            {
                this.Add(item);
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity">Entity</param>
        public abstract void Update(TEntity entity);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entities"></param>
        public virtual void Update(IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
            {
                this.Update(item);
            }
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        public abstract void Remove(TEntity entity);

        /// <summary>
        /// 批量删除实体对象
        /// </summary>
        /// <param name="entities"></param>
        public virtual void Remove(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                this.Remove(entity);
            }
        }

        /// <summary>
        /// 根据主键删除数据
        /// </summary>
        /// <param name="id"></param>
        public void Remove(object id)
        {
            var entity = this.Get(id);
            if (!entity.IsNull())
            {
                this.Remove(entity);
            }
        }

        /// <summary>
        /// 依据条件删除数据
        /// </summary>
        /// <param name="predicate"></param>
        public void Remove(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var entity in this.Table.Where(predicate).ToList())
            {
                this.Remove(entity);
            }
        }

        /// <summary>
        /// 获取当前所有实体
        /// </summary>
        public abstract IQueryable<TEntity> Table { get; }

        /// <summary>
        /// 获取当前所有实体
        /// </summary>
        public abstract IQueryable<TEntity> TableNoTracking { get; }
    }

    /// <summary>
    /// http://stackoverflow.com/questions/18976495/linq-to-entities-only-supports-casting-edm-primitive-or-enumeration-types-with-i
    /// </summary>
    sealed class EntityCastRemoverVisitor : ExpressionVisitor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Convert<T>(Expression<Func<T, bool>> predicate)
        {
            var visitor = new EntityCastRemoverVisitor();
            var visitedExpression = visitor.Visit(predicate);
            return (Expression<Func<T, bool>>)visitedExpression;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (node.NodeType == ExpressionType.Convert && node.Type == typeof(ISoftDelete))
            {
                return node.Operand;
            }
            return base.VisitUnary(node);
        }
    }
}
