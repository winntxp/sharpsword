/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/5/2016 9:39:06 AM
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// 把集合形式的数据转变成参数化过程，一般用在 select * from A where status IN(@p0,@p1) 这样需求的地方
        /// </summary>
        /// <example>
        /// <![CDATA[
        ///  var parametersInfo = GetParametersInfo(new List<int>());
        ///  string sql = "select * from A where status IN({0})".With(parametersInfo.Item1.Select(x => "@{0}".With(x)));
        /// ]]>
        /// </example>
        /// <typeparam name="T">参数类型：需要输入基元类型，比如：string , int ....</typeparam>
        /// <param name="params"></param>
        /// <returns>Item1:为查询参数化字符串（不带@符号），Item2:为查询参数化对应的参数值</returns>
        /// <param name="prefix">参数前缀，如果不设置系统默认生成p0,p1这样的参数</param>
        public static Tuple<string[], T[]> GetDataParametersInfo<T>(this IEnumerable<T> @params, string prefix = "")
        {
            @params.CheckNullThrowArgumentNullException(nameof(@params));

            //参数转化成数组
            var @p = @params.ToArray();

            //参数化
            IList<string> parameterNames = new List<string>();
            IList<T> parameterValues = new List<T>();
            for (int i = 0; i < @p.Length; i++)
            {
                parameterNames.Add("{0}{1}".With(prefix.IsNullOrEmptyForDefault(() => "p", key => key), i));
                parameterValues.Add(@p[i]);
            }

            //返回参数化组合信息
            return new Tuple<string[], T[]>(parameterNames.ToArray(), parameterValues.ToArray());
        }
    }
}
