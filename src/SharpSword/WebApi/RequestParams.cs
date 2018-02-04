/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/31/2015 11:04:21 PM
 * ****************************************************************/
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 接口客户端提交的核心参数包
    /// </summary>
    [Serializable]
    public class RequestParams
    {
        /// <summary>
        /// 客户端ID：比如：20009等
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// 从请求的信息里获取到请求的接口名称，比如：API.Help
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 客户端指定接口服务器返回数据的格式化方式 XML/JSON/VIEW
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// 上传的JSON数据；就算是不需上送参数，也需要上送"{}"字符串
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// 上传时间戳（服务器与服务端到时候进行时间比对）;格式为：yyyy/MM/dd HH:mm:ss
        /// 应用场景：比如，一个接口如果不加这个时间戳的时候，只要有人截获了提交参数以及知道了URL
        /// 那么截获访问消息的人，完全可以重复提交接口数据，这尤其在针对数据操作的时候，影响比较大，
        /// 因此加上次时间戳，让调用客户端上送客户端时间，然后服务器比对时间戳与服务器时间，
        /// 如果相差时间间隔比较大（比如1分钟），那么不允许提交
        /// </summary>
        public string TimeStamp { get; set; }

        /// <summary>
        /// 接口版本（在有多个接口名称一致的情况下；可以根据指定接口版本来选择特定的版本接口）
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 客户端数据签名（具体的数据签名方式需要在实际业务场景里约定）
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 请求ID，用于跟踪
        /// </summary>
        public string RequestId { get; set; }
    }
}
