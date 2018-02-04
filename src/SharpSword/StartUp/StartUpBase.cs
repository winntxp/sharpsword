/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/16 15:23:12
 * ****************************************************************/
using System;

namespace SharpSword
{
    /// <summary>
    /// 系统初始化启动抽象类，
    /// </summary>
    public abstract class StartUpBase : IStartUp
    {
        /// <summary>
        /// 我们默认将优先级设置成最大-999
        /// </summary>
        public virtual int Priority => int.MaxValue - 9999;

        /// <summary>
        /// 启动执行业务逻辑
        /// </summary>
        public abstract void Init();
    }
}
