/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/29/2016 1:55:49 PM
 * ****************************************************************/
using System;
using System.ComponentModel.DataAnnotations;

namespace SharpSword
{
    /// <summary>
    /// 带了当前操作用户
    /// </summary>
    [Serializable]
    public abstract class RequestDtoBaseWithUser : RequestDtoBase, IRequiredUser
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        [MaxLength(100)]
        public string UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [MaxLength(100)]
        public string UserName { get; set; }
    }

}
