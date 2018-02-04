/* *******************************************************
 * SharpSword zhangliang4629@163.com 9/26/2016 11:44:34 AM
 * ****************************************************************/
using System;

namespace SharpSword.Data
{
    /// <summary>
    /// 指定一个方法是否使用了自定义SQL语句来进行数据存储，
    /// 在使用了自定义SQL的方法里我们加上此特性，
    /// 我们再后期就可以根据特性来搜索出所有自定义SQL的地方，
    /// 这样方便我们后期的维护的升级
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class SqlUsedAttribute : Attribute { }
}
