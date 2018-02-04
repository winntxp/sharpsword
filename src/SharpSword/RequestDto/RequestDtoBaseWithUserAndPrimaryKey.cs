/* ****************************************************************
 * SharpSword zhangliang4629@163.com 11/29/2016 1:55:49 PM
 * ****************************************************************/
using System;

namespace SharpSword
{
    /// <summary>
    /// 请求类型带ID编号
    /// </summary>
    [Serializable]
    public abstract class RequestDtoBaseWithUserAndPrimaryKey<TPrimaryKey> : RequestDtoBaseWithUser, IRequiredPrimaryKey<TPrimaryKey>
    {
        /// <summary>
        /// ID编号
        /// </summary>
        public TPrimaryKey Id { get; set; }
    }

}
