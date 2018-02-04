/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;
using SharpSword.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Transactions;

namespace SharpSword.EntityFramework
{
    /// <summary>
    /// 使用EF的方式来实现工作单元
    /// </summary>
    internal class EfUnitOfWork : UnitOfWorkBase
    {
        //用于保存当前工作单元使用了那些数据库访问上下文，作为一个工作单元事务提交（注意在使用多数据库的情况下，即一个方法内使用了多个工作单元，需要开启远程事务协调器）
        private readonly IDictionary<Type, DbContext> _activeDbContexts;
        private readonly IIocResolver _iocResolver;
        private TransactionScope _transaction;
        private Lazy<ILifetimeScope> _scope;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iocResolver"></param>
        /// <param name="defaultOptions"></param>
        public EfUnitOfWork(IIocResolver iocResolver)
        {
            this._iocResolver = iocResolver;
            this._activeDbContexts = new Dictionary<Type, DbContext>();
            this._scope = new Lazy<ILifetimeScope>(() => iocResolver.Scope(this.Id));
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void BeginUow()
        {
            if (Options.IsTransactional == true)
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = Options.IsolationLevel.GetValueOrDefault(IsolationLevel.ReadUncommitted),
                };

                if (Options.Timeout.HasValue)
                {
                    transactionOptions.Timeout = Options.Timeout.Value;
                }

                _transaction = new TransactionScope(Options.Scope.GetValueOrDefault(TransactionScopeOption.Required), transactionOptions);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void SaveChanges()
        {
            _activeDbContexts.Values.ToList().ForEach(SaveChangesInDbContext);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void CompleteUow()
        {
            this.SaveChanges();
            if (!_transaction.IsNull())
            {
                _transaction.Complete();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <returns></returns>
        internal TDbContext GetOrCreateDbContext<TDbContext>() where TDbContext : DbContext
        {
            DbContext dbContext;
            if (!_activeDbContexts.TryGetValue(typeof(TDbContext), out dbContext))
            {
                dbContext = this._iocResolver.Resolve<TDbContext>(_scope.Value);
                _activeDbContexts[typeof(TDbContext)] = dbContext;
            }

            return (TDbContext)dbContext;
        }

        /// <summary>
        /// 释放工作单元
        /// </summary>
        protected override void DisposeUow()
        {
            _activeDbContexts.Values.ToList().ForEach(dbContext =>
            {
                dbContext.Dispose();
                _iocResolver.Release(dbContext);
            });

            if (!_transaction.IsNull())
            {
                _transaction.Dispose();
            }

            if (!_scope.Value.IsNull())
            {
                _scope.Value.Dispose();
            }
        }

        /// <summary>
        /// https://msdn.microsoft.com/en-us/data/jj592904
        /// </summary>
        /// <param name="dbContext"></param>
        protected virtual void SaveChangesInDbContext(DbContext dbContext)
        {
            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                dbContext.SaveChanges();
            }
        }
    }
}