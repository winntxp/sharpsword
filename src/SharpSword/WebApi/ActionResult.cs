/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using Newtonsoft.Json;
using SharpSword.Timing;
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 强类型的action执行返回数据，系统框架会直接以XML或者JSON序列化，输出给客户端
    /// </summary>
    /// <typeparam name="TResponseDto">输出的数据类型</typeparam>
    [Serializable]
    public class ActionResult<TResponseDto>
    {
        /// <summary>
        /// 用于保存创建此返回对象的时间
        /// </summary>
        private readonly string _cacheTime;

        /// <summary>
        /// 设置下默认的缓存时间，方便前端调式
        /// </summary>
        public ActionResult()
        {
            this._cacheTime = Clock.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="flag"></param>
        /// <param name="info"></param>
        public ActionResult(TResponseDto data, ActionResultFlag flag, string info) : this()
        {
            this.Data = data;
            this.Flag = flag;
            this.Info = info;
        }

        /// <summary>
        /// 相关值：ActionResultFlag枚举
        /// </summary>
        [JsonProperty("Flag")]
        public ActionResultFlag Flag { get; set; }

        /// <summary>
        /// ActionResultFlag枚举，字符串形式
        /// </summary>
        [JsonProperty("FlagDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string FlagDescription
        {
            get
            {
                return this.Flag.ToString();
            }
        }

        /// <summary>
        /// ActionResult对象被创建的时间(也即当前ActionResult被实例化的时间)
        /// </summary>
        public string CachedTime { get { return this._cacheTime; } }

        /// <summary>
        /// 返回的待序列化的JSON对象数据
        /// </summary>
        [JsonProperty("Data", NullValueHandling = NullValueHandling.Ignore)]
        public TResponseDto Data { get; set; }

        /// <summary>
        /// 成功返回OK，失败返回错误消息
        /// </summary>
        [JsonProperty("Info", NullValueHandling = NullValueHandling.Ignore)]
        public string Info { get; set; }
    }

    /// <summary>
    /// 系统默认object类型
    /// </summary>
    [Serializable]
    public class ActionResult : ActionResult<object>
    {
        /// <summary>
        /// 
        /// </summary>
        public ActionResult() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="flag"></param>
        /// <param name="info"></param>
        public ActionResult(object data, ActionResultFlag flag, string info) : base(data, flag, info) { }
    }
}