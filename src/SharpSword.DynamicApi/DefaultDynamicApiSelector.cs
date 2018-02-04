/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/7/2016 11:18:50 AM
 * ****************************************************************/
using SharpSword.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharpSword.DynamicApi
{
    /// <summary>
    /// 默认的动态接口查找器，便于根据不同的项目，实现不同的动态API接口查找方式
    /// </summary>
    public class DefaultDynamicApiSelector : IDynamicApiSelector
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ITypeFinder _typeFinder;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeFinder">类型查找器</param>
        public DefaultDynamicApiSelector(ITypeFinder typeFinder)
        {
            typeFinder.CheckNullThrowArgumentNullException(nameof(typeFinder));
            this._typeFinder = typeFinder;
        }

        /// <summary>
        /// 获取接口请求上送参数类型
        /// </summary>
        /// <param name="methodInfo">待生成接口的方法信息</param>
        /// <returns>Type，方法的参数类型</returns>
        private Type GetRequestDtoType(MethodInfo methodInfo)
        {
            //获取接口所有参数
            var parameters = methodInfo.GetParameters();

            //方法不带参数，默认给一个系统框架自带的NullRequestDto参数
            if (!parameters.Any())
            {
                return typeof(NullRequestDto);
            }

            //含有参数，获取第一个参数类型
            // ReSharper disable once PossibleNullReferenceException
            return parameters.FirstOrDefault().ParameterType;
        }

        /// <summary>
        /// 获取接口返回对象数据类型,内部做了处理，如果无返回值，则默认输出NullResponseDto
        /// </summary>
        /// <param name="methodInfo">待生成接口的方法信息</param>
        /// <returns>Type 方法的返回值类型</returns>
        private Type GetResponseDtoType(MethodInfo methodInfo)
        {
            //无返回值
            //if (typeof(void).Equals(methodInfo.ReturnType))
            //{
            //    return typeof(NullResponseDto);
            //}

            var returnType = methodInfo.ReturnType;

            //泛型集合
            //if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            //{
            //    return typeof(IList<>).MakeGenericType(returnType.GetGenericArguments()[0]);
            //}

            //泛型字典

            //有返回值
            return returnType;
        }

        /// <summary>
        /// 公开的，并且参数只有一个，并且入参继承RequestDtoBase基类的，我们认为是一个需要公开的接口
        /// </summary>
        /// <param name="methodInfo">待生成接口的方法信息</param>
        /// <returns>true/false</returns>
        protected virtual bool IsDynamicApi(MethodInfo methodInfo)
        {
            methodInfo.CheckNullThrowArgumentNullException(nameof(methodInfo));

            //IsSpecialName：是否是属性 ，属性会自动生成：get_属性名称这样的方法，因此需要排除掉
            if (methodInfo.IsSpecialName)
            {
                return false;
            }

            //指定不为动态API接口
            if (methodInfo.IsDefined(typeof(NotDynamicApiAttribute), false))
            {
                return false;
            }

            //类是否定义了动态接口特性，如果定义了，那么下面的所有实例方法都变成了动态接口
            // ReSharper disable once PossibleNullReferenceException
            if (!methodInfo.DeclaringType.IsDefined(typeof(DynamicApiAttribute), false)
                && !methodInfo.IsDefined(typeof(DynamicApiAttribute), false))
            {
                return false;
            }

            //不能是泛型方法(在业务逻辑层，定义成泛型方法的做法不是很好，因为业务领域了一般都是针对领域分析的结果
            //一般不会出现通用的业务逻辑，就算是有，那么肯定可以放在其他层，而不应该放在领域业务层里面)
            if (methodInfo.IsGenericMethod)
            {
                return false;
            }

            //获取接口所有参数
            var parameters = methodInfo.GetParameters();

            //如果接口参数大于1，我们不认为是动态接口
            if (parameters.Length > 1)
            {
                return false;
            }

            //如果参数数量==1，但是参数没有继承RequestDtoBase我们也不认为是合法的动态接口
            if (parameters.Length == 1 && !typeof(RequestDtoBase).IsAssignableFrom(parameters[0].ParameterType))
            {
                return false;
            }

            //方法是合法的动态接口
            return true;
        }

        /// <summary>
        /// 获取所有带生成的合法的服务层方法接口
        /// </summary>
        /// <param name="methodFilter">接口方法过滤器</param>
        /// <returns>获取当前应用程序域里的所有合法的待生成动态API接口的描述对象集合</returns>
        public virtual IEnumerable<DynamicApiDescriptor> GetDynamicApiDescriptors(Func<MethodInfo, bool> methodFilter = null)
        {
            //用于临时保存接口描述对象集合
            var dynamicApiDescriptors = new List<DynamicApiDescriptor>();

            //查找所有制定定义了动态API接口的services类
            var dynamicApiServiceTypes =
                this._typeFinder.FindClassesOfType<IDynamicApiService>()
                    //过滤掉AOP生成的代理类
                    .Where(o => !o.IsProxyType()).ToList();

            //获取所有的符合接口的方法
            foreach (var dynamicApiServiceType in dynamicApiServiceTypes)
            {
                //所有的方法集合(必须为公开且为实例方法)
                var methods = dynamicApiServiceType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                                   .Where(m => this.IsDynamicApi(m));

                //自定义了过滤器，但是也必须在框架定义的基础之上，再次进行过滤
                if (!methodFilter.IsNull())
                {
                    methods = methods.Where(methodFilter);
                }

                //获取合法的方法
                foreach (var method in methods)
                {
                    //默认使用类名+方法名称作为接口名称
                    var actionName = "{0}.{1}".With(dynamicApiServiceType.Name, method.Name);

                    //方法定义了接口名称
                    var actionNameAttributeType = typeof(ActionNameAttribute);
                    if (method.IsDefined(actionNameAttributeType, false))
                    {
                        actionName = method.GetCustomAttributes(actionNameAttributeType, false)
                                           .Cast<ActionNameAttribute>().FirstOrDefault().Name;
                    }

                    //构造动态API描述对象
                    var dynamicApiDescriptor = new DynamicApiDescriptor()
                    {
                        DeclaringType = dynamicApiServiceType,
                        RequestDtoType = this.GetRequestDtoType(method),
                        ResponseDtoType = this.GetResponseDtoType(method),
                        MethodInfo = method,
                        ActionName = actionName
                    };

                    //将找到的可以映射动态api接口的方法描述对象保存到集合
                    dynamicApiDescriptors.Add(dynamicApiDescriptor);
                }
            }

            //返回所有找到的动态API描述对象
            return dynamicApiDescriptors;
        }
    }
}
