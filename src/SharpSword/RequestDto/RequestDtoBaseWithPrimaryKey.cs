/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/29/2016 1:55:49 PM
 * ****************************************************************/
using System;

namespace SharpSword
{
    /// <summary>
    /// 请求类型带ID编号
    /// </summary>
    [Serializable]
    public abstract class RequestDtoBaseWithPrimaryKey<TPrimaryKey> : RequestDtoBase, IRequiredPrimaryKey<TPrimaryKey>
    {
        /// <summary>
        /// ID编号
        /// </summary>
        public TPrimaryKey Id { get; set; }
    }

}
