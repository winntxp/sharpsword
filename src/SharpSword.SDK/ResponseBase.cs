/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/03/2015 8:23:18 PM
 * ****************************************************************/
using System;
using System.Collections.Generic;

namespace SharpSword.SDK
{
    /// <summary>
    /// 返回消息基类
    /// </summary>
    [Serializable]
    public class ResponseBase
    {
        /// <summary>
        /// 
        /// </summary>
        public ResponseBase()
        {
            this.Resp_Headers = new Dictionary<string, string>();
        }

        /// <summary>
        /// 消息代码:0代表成功，其他值代码失败（1系统错误，2密钥验证失败，3系统错误，4连接超时）
        /// </summary>
        public int Flag { get; set; }

        /// <summary>
        /// 消息代码Flag描述，比如：SUCCESS，FAIL，EXCEPTION，TIMEOUT
        /// </summary>
        public string FlagDescription { get; set; }

        /// <summary>
        /// 返回信息，有可能是正确的消息，有可能是错误消息；
        /// 具体是错误信息还是仅仅是返回消息看Flag代码值,0为正确的消息，其他值为错误消息
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// 响应原始内容，方便调试(请不要使用此返回数据作为格式化依据，而应该根据SDK提供的返回对象)
        /// </summary>
        public string Resp_Body { get; set; }

        /// <summary>
        /// HTTP GET请求的所有参数信息，方便调试
        /// </summary>
        public string Resp_ReqData { get; set; }

        /// <summary>
        /// 服务器返回的Httpheader头信息，方便调试
        /// </summary>
        public Dictionary<string, string> Resp_Headers { get; set; }

    }
}
