/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/7/2016 10:35:57 AM
 * ****************************************************************/
using Nito.AsyncEx;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace SharpSword.Threading
{
    /// <summary>
    /// 
    /// </summary>
    public static class AsyncHelper
    {
        /// <summary>
        /// 判断一个方法是否是异步方法
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static bool IsAsyncMethod(MethodInfo method)
        {
            return (method.ReturnType == typeof(Task) ||
                    (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>)));
        }

        /// <summary>
        /// 执行异步方法，将异步转换成同步执行
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return AsyncContext.Run(func);
        }

        /// <summary>
        /// 执行异步方法，将异步转换成同步执行
        /// </summary>
        /// <param name="action"></param>
        public static void RunSync(Func<Task> action)
        {
            AsyncContext.Run(action);
        }
    }
}
