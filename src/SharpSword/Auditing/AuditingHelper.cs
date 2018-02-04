/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/7/2016 10:21:32 AM
 * ****************************************************************/
using System.Reflection;
using System.Collections.Concurrent;

namespace SharpSword.Auditing
{
    /// <summary>
    /// AOP
    /// </summary>
    internal static class AuditingHelper
    {
        /// <summary>
        /// 用来缓存所有被审计的方法是否允许审计，防止运行时重复判断方法是否允许记录审计信息
        /// </summary>
        private static ConcurrentDictionary<MemberInfo, bool> _auditMethodCache = new ConcurrentDictionary<MemberInfo, bool>();

        /// <summary>
        /// 判断一个方法是否需要记录审计
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="configuration"></param>
        /// <param name="session"></param>
        /// <param name="defaultValue">默认开启</param>
        /// <returns></returns>
        public static bool ShouldSaveAudit(MethodInfo methodInfo, AuditingConfiguration configuration, ISession session, bool defaultValue = true)
        {
            //全局审计配置优先级最高
            if (configuration.IsNull() || !configuration.IsEnabled)
            {
                return false;
            }

            if (!configuration.IsEnabledForAnonymousUsers && (session.IsNull() || session.UserId.IsNullOrEmpty()))
            {
                return false;
            }

            return _auditMethodCache.GetOrAdd(methodInfo, m =>
            {

                if (methodInfo.IsNull() || !methodInfo.IsPublic)
                {
                    return false;
                }

                //定义在方法上面的开启审计优先级高一些
                if (methodInfo.IsDefined(typeof(AuditedAttribute)))
                {
                    return true;
                }
                if (methodInfo.IsDefined(typeof(DisableAuditingAttribute)))
                {
                    return false;
                }

                //定义在类级别的审计优先级最低
                var classType = methodInfo.DeclaringType;
                if (!classType.IsNull())
                {
                    if (classType.IsDefined(typeof(AuditedAttribute)))
                    {
                        return true;
                    }

                    if (classType.IsDefined(typeof(DisableAuditingAttribute)))
                    {
                        return false;
                    }
                }

                return defaultValue;
            });
        }
    }
}
