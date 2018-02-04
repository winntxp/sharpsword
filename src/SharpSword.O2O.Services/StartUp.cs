/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/9/2017 3:31:54 PM
 * ****************************************************************/
using System;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 系统启动的时候，我们先做一些系统初始化工作，比如讲活动商品缓存到缓存
    /// </summary>
    public class StartUp : StartUpBase
    {
        /// <summary>
        /// 
        /// </summary>
        public StartUp() { }

        /// <summary>
        /// 
        /// </summary>
        public override int Priority => 0;

        /// <summary>
        /// 
        /// </summary>
        public override void Init()
        {
            //throw new NotImplementedException();
        }
    }
}
