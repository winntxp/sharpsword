/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using EntityFramework.DynamicFilters;
using SharpSword.Data;
using SharpSword.Domain.Entitys;
using SharpSword.Domain.Uow;
using SharpSword.Events.Entitys;
using SharpSword.Runtime;
using SharpSword.Timing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SharpSword.EntityFramework
{
    /// <summary>
    /// 使用EF作为ORM工具的所有上下文，需要继承此类，实现抽象类的所有上下文系统框架会自动进行仓储注册
    /// </summary>
    public abstract class DbContextBase : DbContext, IDbContext, IShouldInitialize
    {
        /// <summary>
        /// 此ID用于调式的时候检测上下文是否是同一个
        /// </summary>
        private string _id = Guid.NewGuid().ToString();
        private bool _initialized = false;

        /// <summary>
        /// 当前使用使用
        /// </summary>
        public ISession Session { get; set; }

        /// <summary>
        /// 实体触发事件封装
        /// </summary>
        public IEntityEventHelper EntityEventHelper { get; set; }

        /// <summary>
        /// 日志记录器
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected DbContextBase(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            this.Logger = NullLogger.Instance;
            this.Session = NullSession.Instance;
            this.EntityEventHelper = NullEntityChangedEventHelper.Instance;
            //this.Configuration.ValidateOnSaveEnabled = false;

            //添加SQL记录器
            //DbInterception.Add(new EfIntercepterLogging());
        }

        /// <summary>
        /// 初始化
        /// </summary>
        void IShouldInitialize.Initialize()
        {
            if (!this._initialized)
            {
                //do other initialize......
                this.Initialize();
                this._initialized = true;
            }
        }

        /// <summary>
        /// 交给子类去实现具体的初始化
        /// </summary>
        protected virtual void Initialize()
        {
            //this.Database.Initialize(false);
        }

        /// <summary>
        /// 实体对象数据库映射所在的程序集
        /// </summary>
        protected virtual Assembly EntityTypeConfigurationMapAssembly
        {
            get
            {
                return Assembly.GetExecutingAssembly();
            }
        }

        /// <summary>
        /// 自动根据实体生成对应的数据库表映射，其他继承类重写此初始化方法
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //实体映射
            var entityTypeConfigurations = this.EntityTypeConfigurationMapAssembly.GetTypes()
                                               .Where(type => !string.IsNullOrEmpty(type.Namespace))
                                               .Where(type => type.BaseType != null
                                                              &&
                                                              type.BaseType.IsGenericType
                                                              &&
                                                              (type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfigurationBase<>)
                                                                ||
                                                               type.BaseType.GetGenericTypeDefinition() == typeof(ComplexTypeConfigurationBase<>)
                                                               )
                                                      );
            foreach (var type in entityTypeConfigurations)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            //创建数据库映射
            base.OnModelCreating(modelBuilder);

            //查询数据过滤掉软删除
            modelBuilder.Filter(DataFilters.SoftDelete, (ISoftDelete d) => d.IsDeleted, false);
        }

        /// <summary>
        /// 重写下保存更改，处理实体审计信息，软删除等
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            try
            {
                this.ApplyConcepts();
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                LogDbEntityValidationException(ex);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                this.ApplyConcepts();
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbEntityValidationException ex)
            {
                LogDbEntityValidationException(ex);
                throw;
            }
        }

        /// <summary>
        /// 触发实体Command操作事件
        /// </summary>
        protected virtual void ApplyConcepts()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    //添加的时候我们处理下创建时间和创建用户信息
                    case EntityState.Added:

                        SetCreationAuditProperties(entry);
                        EntityEventHelper.TriggerEntityCreatedEvent(entry.Entity);

                        break;

                    case EntityState.Modified:

                        SetModificationAuditProperties(entry);

                        //当我们直接修改的方式去删除的时候，我们需要处理软删除
                        if (entry.Entity is ISoftDelete && entry.Entity.As<ISoftDelete>().IsDeleted)
                        {
                            if (entry.Entity is IDeletionAudited)
                            {
                                SetDeletionAuditProperties(entry.Entity.As<IDeletionAudited>());
                            }

                            EntityEventHelper.TriggerEntityDeletedEvent(entry.Entity);
                        }
                        else
                        {
                            EntityEventHelper.TriggerEntityUpdatedEvent(entry.Entity);
                        }

                        break;

                    //删除的时候我们处理下软删除和删除用户审计信息
                    case EntityState.Deleted:
                        HandleSoftDelete(entry);
                        EntityEventHelper.TriggerEntityDeletedEvent(entry.Entity);

                        break;
                }
            }
        }

        /// <summary>
        /// 为实体设置创建用户等信息
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void SetCreationAuditProperties(DbEntityEntry entry)
        {
            if (entry.Entity is IHasCreationTime)
            {
                entry.Cast<IHasCreationTime>().Entity.CreationTime = Clock.Now;
            }

            if (entry.Entity is ICreationAudited)
            {
                var creationAuditedEntry = entry.Cast<ICreationAudited>().Entity;
                creationAuditedEntry.CreatorUserId = this.Session.UserId;
                creationAuditedEntry.CreatorUserName = this.Session.UserName;
            }
        }

        /// <summary>
        /// 设置最后修改时间和修改用户
        /// </summary>
        /// <param name="entry"></param>
        protected virtual void SetModificationAuditProperties(DbEntityEntry entry)
        {
            if (entry.Entity is IHasModificationTime)
            {
                entry.Cast<IHasModificationTime>().Entity.LastModifyTime = Clock.Now;
            }

            if (entry.Entity is IModificationAudited)
            {
                var modificatioAuditedEntry = entry.Cast<IModificationAudited>().Entity;
                modificatioAuditedEntry.LastModifyUserId = this.Session.UserId;
                modificatioAuditedEntry.LastModifyUserName = this.Session.UserName;
            }
        }

        /// <summary>
        /// 软删除处理
        /// </summary>
        /// <param name="entry"></param>
        protected virtual void HandleSoftDelete(DbEntityEntry entry)
        {
            if (!(entry.Entity is ISoftDelete))
            {
                return;
            }
            var softDeleteEntry = entry.Cast<ISoftDelete>();
            softDeleteEntry.State = EntityState.Unchanged;
            softDeleteEntry.Entity.IsDeleted = true;
            if (entry.Entity is IDeletionAudited)
            {
                SetDeletionAuditProperties(entry.Cast<IDeletionAudited>().Entity);
            }
        }

        /// <summary>
        /// 设置删除审计信息
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void SetDeletionAuditProperties(IDeletionAudited entity)
        {
            entity.DeletionTime = Clock.Now;
            entity.DeleterUserId = this.Session.UserId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        private void LogDbEntityValidationException(DbEntityValidationException exception)
        {
            foreach (var ve in exception.EntityValidationErrors.SelectMany(eve => eve.ValidationErrors))
            {
                Logger.Error(" - " + ve.PropertyName + ": " + ve.ErrorMessage);
            }
        }

        #region IDbcontext

        /// <summary>
        /// 当前数据库连接信息
        /// </summary>
        public IDbConnection Connection
        {
            get
            {
                return this.Database.Connection;
            }
        }

        /// <summary>
        /// 根据当前连接创建查询参数
        /// </summary>
        /// <returns></returns>
        public IDataParameter CreateParameter()
        {
            var dbConnection = this.Connection;
            var property = dbConnection.GetType().GetProperty("DbProviderFactory",
                                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return (property.GetValue(dbConnection) as DbProviderFactory).CreateParameter();
        }

        /// <summary>
        /// 数据库是否存在
        /// </summary>
        public bool DataBaseExists
        {
            get { return this.Database.Exists(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private EntityConnection GetEntityConnection()
        {
            return ((IObjectContextAdapter)this).ObjectContext.Connection as EntityConnection;
        }

        /// <summary>
        /// 当前数据库，如果不存在则返回null
        /// </summary>
        public string DbName
        {
            get
            {
                var connection = this.GetEntityConnection();
                if (connection.IsNull())
                {
                    return null;
                }
                return connection.StoreConnection.Database;
            }
        }

        /// <summary>
        /// 数据源
        /// </summary>
        public string DataSource
        {
            get
            {
                var connection = this.GetEntityConnection();
                if (connection.IsNull())
                {
                    return null;
                }
                return connection.StoreConnection.DataSource;
            }
        }

        /// <summary>
        /// 清理被上线问管理的所有实体对象；当出现异常的时候，需要清理下，防止重试出现错误
        /// </summary>
        public void ClearStateEntries()
        {
            var context = ((IObjectContextAdapter)this).ObjectContext;
            var addedObjects = context.ObjectStateManager.GetObjectStateEntries(EntityState.Added);
            foreach (var objectStateEntry in addedObjects)
            {
                context.Detach(objectStateEntry.Entity);
            }
            var modifyObjects = context.ObjectStateManager.GetObjectStateEntries(EntityState.Modified);
            foreach (var objectStateEntry in modifyObjects)
            {
                context.Detach(objectStateEntry.Entity);
            }
        }

        /// <summary>
        /// 获取创建数据文件脚本
        /// </summary>
        /// <returns>创建数据库脚本文件</returns>
        public string CreateDatabaseScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        }

        /// <summary>
        /// 根据SQL语句动态创建类型
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Type CreateDynamicType(string sql, params object[] parameters)
        {
            return this.Database.CreateDynamicType(sql, parameters);
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<TElement> ExecuteStoredProcedure<TElement>(string storedProcedureName, object[] parameters = null) where TElement : new()
        {
            //不存在参数，直接执行SQL语句执行
            if (parameters == null || parameters.Length <= 0)
                return this.Database.SqlQuery<TElement>(storedProcedureName, parameters ?? new object[] { }).ToList();

            //开始构造SQL语句执行存储过程
            for (int i = 0; i <= parameters.Length - 1; i++)
            {
                var parameter = parameters[i] as DbParameter;
                if (parameter.IsNull())
                {
                    throw new SharpSwordCoreException("parameters参数错误，请使用DbParameter数组");
                }

                storedProcedureName += i == 0 ? " " : ", ";

                storedProcedureName += "@" + parameter.ParameterName;
                if (parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Output)
                {
                    //output parameter
                    storedProcedureName += " output";
                }
            }

            return this.Database.SqlQuery<TElement>(storedProcedureName, parameters ?? new object[] { }).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<TElement> Query<TElement>(string sql, object[] parameters = null) where TElement : new()
        {
            return this.Database.SqlQuery<TElement>(sql, parameters ?? new object[] { });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable DynamicQuery(string sql, object[] parameters = null)
        {
            return this.Database.DynamicSqlQuery(sql, parameters ?? new object[] { });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="userTransaction"></param>
        /// <param name="timeout"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int Execute(string sql, bool userTransaction = false, int? timeout = null, object[] parameters = null)
        {
            int? previousTimeout = null;
            if (timeout.HasValue)
            {
                //store previous timeout
                previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }

            var transactionalBehavior = !userTransaction
                ? TransactionalBehavior.DoNotEnsureTransaction
                : TransactionalBehavior.EnsureTransaction;
            var result = this.Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);

            if (timeout.HasValue)
            {
                //Set previous timeout back
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
            }

            //return result
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="userTransaction"></param>
        /// <param name="timeout"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T ExecuteScalar<T>(string sql, bool userTransaction = false, int? timeout = null, object[] parameters = null)
        {
            int? previousTimeout = null;
            if (timeout.HasValue)
            {
                previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }

            var connection = this.Database.Connection;
            var command = connection.CreateCommand();
            object value;

            try
            {
                if (connection.State.Equals(ConnectionState.Closed))
                {
                    connection.Open();
                }
                command.CommandText = sql;
                command.PrepareCommandParameters(parameters);
                value = command.ExecuteScalar();
            }
            finally
            {
                if (!connection.State.Equals(ConnectionState.Closed))
                {
                    connection.Close();
                }
                if (!command.IsNull())
                {
                    command.Dispose();
                }
            }

            if (timeout.HasValue)
            {
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
            }

            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }

        #endregion

        /// <summary>
        /// 是否对实体生成代理对象（针对EF）
        /// </summary>
        internal virtual bool ProxyCreationEnabled
        {
            get
            {
                return this.Configuration.ProxyCreationEnabled;
            }
            set
            {
                this.Configuration.ProxyCreationEnabled = value;
            }
        }

        /// <summary>
        /// 是否启用自动跟踪(针对EF)
        /// </summary>
        internal virtual bool AutoDetectChangesEnabled
        {
            get
            {
                return this.Configuration.AutoDetectChangesEnabled;
            }
            set
            {
                this.Configuration.AutoDetectChangesEnabled = value;
            }
        }
    }
}
