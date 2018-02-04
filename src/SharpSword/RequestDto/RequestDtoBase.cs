/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/9 20:00:35
 * ****************************************************************/
using System;
using System.Collections.Generic;

namespace SharpSword
{
    /// <summary>
    /// 请求参数传输对象抽象基类，当前操作接口的用户id和用户名称；是的实现类无需重复定义
    /// 在具体的请求实现类里，针对http请求特殊性（全部数据都是按照字符串提交的），因此定义
    /// 数据类型的时候，接口上送参数也尽量定义成简易的数据类型（含复杂对象的属性）
    /// （string,int,long,enum,decimal,list,数组）
    /// </summary>
    [Serializable]
    public abstract class RequestDtoBase : IRequestDto, IDtoValidatable
    {
        /// <summary>
        /// 此方法也许在执行Valid()方法前执行(即在此操作的时候，所有的验证特性还未曾生效)，让上送参数对象
        /// 有机会处理下自己的数据，比如设置默认值操作等 比如：当前属性UserName为空的时候，可以在重写方法
        /// 里设置：this.UserName = "system" 给予默认值(目的是为了在添加数据的时候，给予一个默认值等...)
        /// 当然，这里的作用远不止上面的，这里可以将上送参数，在创建接口实例前进行保存，然后保存到当前运行上下文
        /// 可以在注入的时候，使用上送的值，如: httpContext.Items["DATA"]=100; 这样后续再注入的服务类里获取到当前线程的值
        /// </summary>
        public virtual void BeforeValid() { }

        /// <summary>
        /// 默认实现下自定义参数业务准确性校验，具体实现类里需要可以重写此方法
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<DtoValidatorResultError> Valid()
        {
            //直接返回一个空的验证集合（无错误信息）
            return new List<DtoValidatorResultError>();
        }
    }
}
