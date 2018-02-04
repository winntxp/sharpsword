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
    /// EF�ִ�ʵ��
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
        /// ��ȡ���ݷ���������
        /// </summary>
        protected TDbContext Context => this._dbContextProvider.DbContext;

        /// <summary>
        /// ��ȡʵ�����洢ӳ���ѯ�ӿ�
        /// </summary>
        protected virtual IDbSet<TEntity> Entities => this.Context.Set<TEntity>();

        /// <summary>
        /// ����ʵ������ID��ȡʵ�����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override TEntity Get(object id)
        {
            return this.Entities.Find(id);
        }

        /// <summary>
        /// ����ʵ��
        /// </summary>
        /// <param name="entity"></param>
        public override void Add(TEntity entity)
        {
            this.Entities.Add(entity);
        }

        /// <summary>
        /// ����ʵ��
        /// </summary>
        /// <param name="entity"></param>
        public override void Update(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// ɾ��ʵ��
        /// </summary>
        /// <param name="entity"></param>
        public override void Remove(TEntity entity)
        {
            this.AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Deleted;
            //this.Entities.Remove(entity);
        }

        /// <summary>
        /// ��ȡһ��ӳ��������Ĳ�ѯ����
        /// </summary>
        public override IQueryable<TEntity> Table
        {
            get { return this.Entities; }
        }

        /// <summary>
        /// �����ֻ���������ʹ�ô����ԣ�EF������ٶ���ʵ�嵽���������ģ�����ܴ����߷����ٶ�
        /// </summary>
        public override IQueryable<TEntity> TableNoTracking
        {
            get { return this.Entities.AsNoTracking(); }
        }

        /// <summary>
        /// ���ز����ڸ��٣���ֱ�Ӹ��ӵ����������ģ������޸ģ�ɾ����ʱ����Ҫ��ѯ
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