/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/25 13:23:00
 * ****************************************************************/
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// API访问记录器参数类
    /// </summary>
    [Serializable]
    public class ApiAccessRecorderArgs
    {
        /// <summary>
        /// 接口名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 数据签名
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string TimeStamp { get; set; }

        /// <summary>
        /// 接口版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 接口作者
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Action执行开始时间
        /// </summary>
        public DateTime RequestStartTime { get; set; }

        /// <summary>
        /// Action执行结束时间，直接获取当前时间
        /// </summary>
        public DateTime RequestEndTime { get; set; }

        /// <summary>
        /// Action执行总共使用的毫秒数
        /// </summary>
        public double RequestMilliseconds { get; set; }

        /// <summary>
        /// 客户端请求方式
        /// </summary>
        public string HttpMethod { get; set; }

        /// <summary>
        /// 客户端IP地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 客户端请求参数包
        /// </summary>
        public string RequestData { get; set; }

        /// <summary>
        /// 服务器输出去数据包
        /// </summary>
        public string ResponseData { get; set; }

        /// <summary>
        /// 输出格式xml/json/view
        /// </summary>
        public string ResponseFormat { get; set; }

        /// <summary>
        /// 当前操作用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 当前操作用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 此次请求的ID
        /// </summary>
        public string RequestId { get; set; }
    }
}
