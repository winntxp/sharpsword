/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/7/2016 10:09:40 AM
 * ****************************************************************/
using SharpSword.Domain.Entitys;
using SharpSword.Timing;
using System;

namespace SharpSword.Auditing
{
    /// <summary>
    /// 审计对象信息
    /// </summary>
    public class AuditInfo : Entity<string>
    {
        /// <summary>
        /// 初始化一下ID和时间
        /// </summary>
        public AuditInfo()
        {
            this.Id = Guid.NewGuid().ToString("N");
            this.ExecutionTime = Clock.Now;
        }

        /// <summary>
        /// 所属程序集
        /// </summary>
        public string Assembly { get; set; }

        /// <summary>
        /// 当前操作用户名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 调用的方法名称
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// 方法入参
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// 方法开始执行时间
        /// </summary>
        public DateTime ExecutionTime { get; set; }

        /// <summary>
        /// 方法执行总时间
        /// </summary>
        public double ExecutionDuration { get; set; }

        /// <summary>
        /// 客户端IP
        /// </summary>
        public string ClientIpAddress { get; set; }

        /// <summary>
        /// 客户端名称
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 客户端浏览器信息
        /// </summary>
        public string BrowserInfo { get; set; }

        /// <summary>
        /// 执行异常信息
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// 当前审计方法执行的线程ID
        /// </summary>
        public string ThreadId { get; set; }

        /// <summary>
        /// 执行用户ID
        /// </summary>
        public string ExecutionUserId { get; set; }

        /// <summary>
        /// 执行用户名称
        /// </summary>
        public string ExecutionUserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "审计信息: 用户:{0} 执行了方法：{1}.{2}，客户端：{3}，共耗时：{4} ms"
                .With(ExecutionUserId, ServiceName, MethodName, ClientIpAddress, ExecutionDuration);
        }
    }
}
