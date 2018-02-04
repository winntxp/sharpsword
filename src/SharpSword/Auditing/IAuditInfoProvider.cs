/* ****************************************************************
 * SharpSword zhangliang4629@163.com 12/7/2016 10:32:21 AM
 * ****************************************************************/

namespace SharpSword.Auditing
{
    /// <summary>
    /// 审计信息提供者（用于填充审计信息）
    /// </summary>
    public interface IAuditInfoProvider
    {
        /// <summary>
        /// 填充审计信息
        /// </summary>
        /// <param name="auditInfo"></param>
        void Fill(AuditInfo auditInfo);
    }
}
