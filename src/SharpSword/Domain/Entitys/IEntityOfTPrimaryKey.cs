/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/15/2016 2:29:56 PM
 * ****************************************************************/

namespace SharpSword.Domain.Entitys
{
    /// <summary>
    /// 实体接口，并且带实体标识ID
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IEntity<TPrimaryKey> : IEntity
    {
        /// <summary>
        /// 实体编号
        /// </summary>
        TPrimaryKey Id { get; set; }

        /// <summary>
        /// 是否还未被持久化过
        /// </summary>
        /// <returns></returns>
        bool IsTransient();
    }
}
