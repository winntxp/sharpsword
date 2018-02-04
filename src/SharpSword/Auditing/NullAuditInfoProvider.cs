/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/7/2016 10:56:03 AM
 * ****************************************************************/

namespace SharpSword.Auditing
{
    /// <summary>
    /// 系统框架默认注册此审计填充器
    /// </summary>
    internal class NullAuditInfoProvider : IAuditInfoProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditInfo"></param>
        public void Fill(AuditInfo auditInfo) { }
    }
}
