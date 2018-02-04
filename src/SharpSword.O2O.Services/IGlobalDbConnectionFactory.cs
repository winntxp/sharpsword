/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/5/2017 12:42:43 PM
 * ****************************************************************/
using System.Data;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 此接口是为方便编程定义的接口，获取全局数据库连接对象
    /// </summary>
    public interface IGlobalDbConnectionFactory
    {
        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <returns></returns>
        IDbConnection Create();
    }
}
