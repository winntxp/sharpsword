/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/7/2016 10:12:34 AM
 * ****************************************************************/

namespace SharpSword.Auditing
{
    /// <summary>
    /// 存储审计接口
    /// </summary>
    public interface IAuditingStore
    {
        /// <summary>
        /// 存储审计
        /// </summary>
        /// <param name="auditInfo">审计对象</param>
        /// <returns></returns>
        void Save(AuditInfo auditInfo);
    }
}
