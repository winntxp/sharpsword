/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/4/2017 12:42:20 PM
 * ****************************************************************/

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class OrderDbTableFinderBase
    {
        /// <summary>
        /// 拆分成多少个表（默认拆分成8张表），下次如果需要扩容，需要按照成倍增加，如：256
        /// </summary>
        protected virtual int TableNumber => 8;

        /// <summary>
        /// 表后缀，格式如：{0:000}，最终形成的表分表名称为：如：Orders00 Orders01 Orders02
        /// </summary>
        protected virtual string TableSuffixFormat => "{0:00}";
    }
}
