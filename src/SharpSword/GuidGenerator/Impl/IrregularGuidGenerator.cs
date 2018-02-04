/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/27/2016 1:59:20 PM
 * *******************************************************/
using System;

namespace SharpSword.GuidGenerator.Impl
{
    /// <summary>
    /// 我们将此GUID注册成默认
    /// </summary>
    public class IrregularGuidGenerator : IGuidGenerator, ITransientDependency
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual Guid Create()
        {
            return Guid.NewGuid();
        }
    }
}
