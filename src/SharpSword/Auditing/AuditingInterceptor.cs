/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/7/2016 10:30:34 AM
 * ****************************************************************/
using Castle.DynamicProxy;
using SharpSword.Domain.Uow;
using SharpSword.Runtime;
using SharpSword.Serializers;
using SharpSword.Timing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace SharpSword.Auditing
{
    /// <summary>
    /// 审计方法拦截器
    /// </summary>
    public class AuditingInterceptor : IInterceptor
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly AuditingConfiguration _configuration;
        private readonly IAuditInfoProvider _auditInfoProvider;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IJsonSerializer _jsonSerializer;

        /// <summary>
        /// 当前操作用户信息
        /// </summary>
        public ISession Session { get; set; }

        /// <summary>
        /// 日志记录器
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 审计存储器
        /// </summary>
        public IAuditingStore AuditingStore { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="auditInfoProvider"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="jsonSerializer"></param>
        public AuditingInterceptor(
            AuditingConfiguration configuration,
            IAuditInfoProvider auditInfoProvider,
            IUnitOfWorkManager unitOfWorkManager,
            IJsonSerializer jsonSerializer)
        {
            this._configuration = configuration;
            this._auditInfoProvider = auditInfoProvider;
            this._unitOfWorkManager = unitOfWorkManager;
            this._jsonSerializer = jsonSerializer;
            this.Session = NullSession.Instance;
            this.Logger = NullLogger.Instance;
            this.AuditingStore = NullAuditingStore.Instance;
        }

        /// <summary>
        /// 执行代理
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            if (!AuditingHelper.ShouldSaveAudit(invocation.MethodInvocationTarget, _configuration, Session))
            {
                invocation.Proceed();
                return;
            }

            //创建审计对象
            var auditInfo = this.CreateAuditInfo(invocation);

            //持久化
            this.SaveAuditing(invocation, auditInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        private AuditInfo CreateAuditInfo(IInvocation invocation)
        {
            var auditInfo = new AuditInfo
            {
                ExecutionUserId = Session.UserId,
                ExecutionUserName = Session.UserName,
                Assembly = invocation.MethodInvocationTarget.DeclaringType?.Assembly.FullName,
                ServiceName = invocation.MethodInvocationTarget.DeclaringType?.FullName,
                MethodName = invocation.MethodInvocationTarget.Name,
                Parameters = ConvertArgumentsToJson(invocation),
                ExecutionTime = Clock.Now,
                ThreadId = Thread.CurrentThread.ManagedThreadId.ToString()
            };

            _auditInfoProvider.Fill(auditInfo);

            return auditInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invocation"></param>
        /// <param name="auditInfo"></param>
        private void SaveAuditing(IInvocation invocation, AuditInfo auditInfo)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                auditInfo.Exception = ex;
                throw;
            }
            finally
            {
                stopwatch.Stop();

                auditInfo.ExecutionDuration = stopwatch.Elapsed.TotalMilliseconds;

                //存储审计信息
                this.AuditingStore.Save(auditInfo);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        private string ConvertArgumentsToJson(IInvocation invocation)
        {
            try
            {
                var parameters = invocation.MethodInvocationTarget.GetParameters();
                if (parameters.IsEmpty())
                {
                    return "{}";
                }

                var dictionary = new Dictionary<string, object>();
                for (int i = 0; i < parameters.Length; i++)
                {
                    var parameter = parameters[i];
                    var argument = invocation.Arguments[i];
                    dictionary[parameter.Name] = argument;
                }

                return this._jsonSerializer.Serialize(dictionary);
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex.ToString(), ex);
                return "{}";
            }
        }
    }
}
