/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 16:01:31
 * ****************************************************************/
using SharpSword.Domain.Entitys;
using System;

namespace SharpSword.AccessRecorder.DB.Domain
{
    /// <summary>
    /// 领域实体对象
    /// </summary>
    public class AccessRecorder : Entity<long>
    {
        /// <summary>
        /// 接口名称
        /// </summary>
        public string ApiName { get; set; }

        /// <summary>
        /// 访问方法
        /// </summary>
        public string HttpMethod { get; set; }

        /// <summary>
        /// 输出格式化
        /// </summary>
        public string ResponseFormat { get; set; }

        /// <summary>
        /// 上送的数据签名
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 上送的时间戳
        /// </summary>
        public string TimeStamp { get; set; }

        /// <summary>
        /// 客户端IP地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 版本信息
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 接口作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 执行花费的时间
        /// </summary>
        public double UsedTime { get; set; }

        /// <summary>
        /// API访问时间
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// 操作接口用户
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 操作用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 请求ID
        /// </summary>
        public string RequestId { get; set; }
    }
}
