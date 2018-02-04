/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 14:47:16
 * ****************************************************************/

namespace SharpSword.Data
{
    /// <summary>
    /// 用于定义表的前缀信息
    /// </summary>
    public interface IDataTablePrefix
    {
        /// <summary>
        /// 表前缀名称
        /// </summary>
        string TablePrefix { get; set; }
    }
}
