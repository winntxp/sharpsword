/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/6/13 9:50:22
 * ****************************************************************/

namespace SharpSword
{
    /// <summary>
    /// 获取当前运行实例信息
    /// </summary>
    public interface IMachineNameProvider
    {
        /// <summary>
        /// 获取应用程序运行的实例名称
        /// </summary>
        string GetMachineName();
    }
}
