/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/7/2016 9:01:26 AM
 * ****************************************************************/
using System.Collections.Generic;
using System;
using System.Reflection;

namespace SharpSword.DynamicApi
{
    /// <summary>
    /// 动态接口查找器，动态查找所有实现了IDynamicApiService接口的类
    /// </summary>
    public interface IDynamicApiSelector
    {
        /// <summary>
        /// 获取所有合法动态API方法描述对象，一个合法的动态接口方法需要满足
        /// 
        /// 1.方法所属的类，必须实现IDynamicApiService接口（空接口）
        /// 
        /// 2.需要映射成动态API的方法，需要定义DynamicApiAttribute特性，如果
        ///   为了方便，我们可以将DynamicApiAttribute特性定义在方法所属的类上
        ///   这样类里的所有方法都会继承此特性定义
        /// 
        /// 3.方法入参必须小于等于1个参数，如果参数个数为1，那么参数必须继承RequestDtoBase抽象类
        ///   如果参数为0个，系统框架会自动将参数映射成NullRequestDto类
        /// 
        /// 4.如果方法是一个合法的动态API，但是我们不想让它对外公开成一个动态API,我们只要
        ///   在方法上定义特性NotDynamicApiAttribute即可。
        /// </summary>
        /// <param name="methodFilter">合法的动态API方法筛选器(此过滤器是在满足上面动态API搜索的情况下，二次搜索过滤器)</param>
        /// <returns>所有合法的待映射成动态API接口的方法描述对象集合，找不到则返回空集合</returns>
        IEnumerable<DynamicApiDescriptor> GetDynamicApiDescriptors(Func<MethodInfo, bool> methodFilter = null);
    }
}
