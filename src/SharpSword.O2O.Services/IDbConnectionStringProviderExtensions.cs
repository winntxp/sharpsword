/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/12/2017 10:06:42 AM
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 
    /// </summary>
    public static class IDbConnectionStringProviderExtensions
    {
        /// <summary>
        /// 根据name前缀获取集合，找不到返回空集合
        /// </summary>
        /// <param name="dbConnectionStringProvider"></param>
        /// <param name="startStr">前缀</param>
        /// <returns></returns>
        public static IEnumerable<ConnectionStringSetting> GetDbConnectionStringsByStartsWith(this IDbConnectionStringProvider dbConnectionStringProvider, string startStr)
        {
            return dbConnectionStringProvider.GetDbConnectionStrings()
                                                            .Where(x => x.Name.StartsWith(startStr, StringComparison.OrdinalIgnoreCase))
                                                            .ToList();
        }
    }
}
