/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 16:01:31
 * ****************************************************************/
using SharpSword.Domain.Entitys;

namespace SharpSword.AccessRecorder.DB.Domain
{
    /// <summary>
    /// 领域实体对象
    /// </summary>
    public class Response : Entity<long>
    {
        /// <summary>
        /// 上送的数据包
        /// </summary>
        public string RequestData { get; set; }

        /// <summary>
        /// 下发的数据包
        /// </summary>
        public string ResponseData { get; set; }
    }
}
