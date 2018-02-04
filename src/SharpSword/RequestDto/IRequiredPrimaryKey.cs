/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/24 9:31:54
 * ****************************************************************/

namespace SharpSword
{
    /// <summary>
    /// 入参带ID参数
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IRequiredPrimaryKey<TPrimaryKey>
    {
        /// <summary>
        ///唯一编号
        /// </summary>
        TPrimaryKey Id { get; set; }
    }
}
