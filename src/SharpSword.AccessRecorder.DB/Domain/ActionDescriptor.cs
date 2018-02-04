/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/3 18:45:31
 * ****************************************************************/
using SharpSword.Domain.Entitys;
using System;

namespace SharpSword.AccessRecorder.DB.Domain
{
    /// <summary>
    /// API接口描述信息对象
    /// </summary>
    public class ActionDescriptor : Entity<long>
    {
        /// <summary>
        /// 接口名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 接口支持的http方式POST/GET；
        /// </summary>
        public string HttpMethod { get; set; }

        /// <summary>
        /// 是否需要https安全连接
        /// </summary>
        public bool IsRequireHttps { get; set; }

        /// <summary>
        /// 接口是否已经取消
        /// </summary>
        public bool IsObsolete { get; set; }

        /// <summary>
        /// 当前接口版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 接口描述信息
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 接口作者是谁
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// 是否需要判断当前操作用户ID和用户名是否未空
        /// </summary>
        public bool RequiredUserIdAndUserName { get; set; }

        /// <summary>
        /// 访问次数
        /// </summary>
        public int AccessCount { get; set; }

        /// <summary>
        /// 最后访问时间(默认时间未当前添加时间)
        /// </summary>
        public DateTime? LastAccessTime { get; set; }
    }
}