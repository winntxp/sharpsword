/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/12/2016 2:05:16 PM
 * ****************************************************************/
using Castle.DynamicProxy;
using SharpSword.Localization;
using System.Linq;
using System.Reflection;

namespace SharpSword.Domain.Services
{
    /// <summary>
    /// 服务类入参校验;注意需要进行参数校验的入参DTO必须是继承自：RequestDtoBase的类
    /// </summary>
    public class DtoValidInterceptor : IInterceptor
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IDtoValidatorManager _requestDtoValidatorManager;

        /// <summary>
        /// 日志记录器
        /// </summary>
        public ILogger<DtoValidInterceptor> Logger { get; set; }

        /// <summary>
        /// 本地化器
        /// </summary>
        public Localizer L { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestDtoValidatorManager">参数验证管理类</param>
        public DtoValidInterceptor(IDtoValidatorManager requestDtoValidatorManager)
        {
            this._requestDtoValidatorManager = requestDtoValidatorManager;
            this.Logger = GenericNullLogger<DtoValidInterceptor>.Instance;
            this.L = NullLocalizer.Instance;
        }

        /// <summary>
        /// 获取代理方法名称
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        private static string GetInvokerMethodName(MethodInfo methodInfo)
        {
            return "{0}.{1}".With(methodInfo.DeclaringType.FullName, methodInfo.Name);
        }

        /// <summary>
        /// 执行方法代理
        /// </summary>
        /// <param name="invocation"></param>
        /// <exception cref="SharpSwordCoreException">参数校验不通过</exception>
        public void Intercept(IInvocation invocation)
        {
            //不校验参数
            if (invocation.Method.IsDefined(typeof(DisableValidationAttribute)))
            {
                invocation.Proceed();
                return;
            }

            //方法名称
            var methodName = GetInvokerMethodName(invocation.Method);

            //入参DTO集合
            var parametersData = invocation.Method.GetParameters()
                                           .Select((p, index) => new { p.Name, Value = invocation.Arguments[index] })
                                           .ToDictionary(kv => kv.Name, kv => kv.Value);

            //校验下参数是否定义了不能为空的特性
            //............


            //循环校验入参
            foreach (var request in parametersData.Values)
            {
                //基元类型不判断
                if (request.GetType().IsPrimitive)
                {
                    continue;
                }

                //校验参数
                var requestDtoValidatorResult = this._requestDtoValidatorManager.Valid(request);

                //参数校验失败，直接抛出异常
                if (requestDtoValidatorResult.IsValid) continue;

                //构造校验错误信息
                string errorMessage = requestDtoValidatorResult.Errors.Select(o => o.ErrorMessage).JoinToString(" ");

                //记录下日志
                this.Logger.Warning(errorMessage);

                //抛出异常
                throw new SharpSwordCoreException(errorMessage);
            }

            //执行方法或者继续下一个横切点
            invocation.Proceed();
        }
    }
}
