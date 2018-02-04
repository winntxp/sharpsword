﻿/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/14/2016 3:18:16 PM
 * ****************************************************************/

namespace SharpSword
{
    /// <summary>
    /// TODO?类需要进行初始化操作，实现此接口的类需要注意：
    /// 在同一线程里多次创建对象的时候，虽然注册成线程单例的对象是同一个对象，但是
    /// 每次创建对象后都会触发一次初始化，一次实现类里需要注意就行是否初始化的判断
    /// 后续找到了新的解决方案后，再进行改动
    /// </summary>
    public interface IShouldInitialize
    {
        /// <summary>
        /// 当对象创建后，自动调用此方法
        /// </summary>
        void Initialize();
    }
}
