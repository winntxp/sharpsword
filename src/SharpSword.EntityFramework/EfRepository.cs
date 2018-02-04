/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 14:47:30
 * ****************************************************************/
using SharpSword.Domain.Entitys;
using SharpSword.Domain.Repositories;
using System.Data.Entity;
using System.Linq;

namespace SharpSword.EntityFramework
{
    /// <summary>
    /// EF仓储实现
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    internal class EfRepository<TDbContext, TEntity> : RepositoryBase<TEntity>
        where TDbContext : DbContext
        where TEntity : Entity
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IDbContextProvider<TDbContext> _dbContextProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public EfRepository(IDbContextProvider<TDbContext> dbContextProvider)
        {
            dbContextProvider.CheckNullThrowArgumentNullException(nameof(dbContextProvider));
            this._dbContextProvider = dbContextProvider;
        }

        /// <summary>
        /// 获取数据访问上下文
        /// </summary>
        protected TDbContext Context => this._dbContextProvider.DbContext;

        /// <summary>
        /// 获取实体对象存储映射查询接口
        /// </summary>
        protected virtual IDbSet<TEntity> Entities => this.Context.Set<TEntity>();

        /// <summary>
        /// 根据实体主键ID获取实体对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override TEntity Get(object id)
        {
            return this.Entities.Find(id);
        }

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity"></param>
        public override void Add(TEntity entity)
        {
            this.Entities.Add(entity);
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        public override void Update(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        public override void Remove(TEntity entity)
        {
            this.AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Deleted;
            //this.Entities.Remove(entity);
        }

        /// <summary>
        /// 获取一个映射的上下文查询对象
        /// </summary>
        public override IQueryable<TEntity> Table
        {
            get { return this.Entities; }
        }

        /// <summary>
        /// 如果是只读情况，请使用此属性，EF不会跟踪对象实体到本地上下文，因此能大大提高访问速度
        /// </summary>
        public override IQueryable<TEntity> TableNoTracking
        {
            get { return this.Entities.AsNoTracking(); }
        }

        /// <summary>
        /// 本地不存在跟踪，就直接附加到跟踪上下文，便于修改，删除的时候不需要查询
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void AttachIfNot(TEntity entity)
        {
            if (!this.Entities.Local.Contains(entity))
            {
                this.Entities.Attach(entity);
            }
        }
    }
}