/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/22 13:04:45
 * ****************************************************************/
using System;
using System.Runtime.InteropServices;

namespace SharpSword.GuidGenerator.Impl
{
    /// <summary>
    /// 有序GUID生成器；用于有可能需要排序保存的场景下使用
    /// </summary>
    public class UuidGuidGenerator : IGuidGenerator
    {
        /// <summary>
        /// 导入系统提供输出有序GUID方法
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [DllImport("rpcrt4.dll", SetLastError = true)]
        private static extern int UuidCreateSequential(out Guid guid);

        /// <summary>
        /// 创建一个有序的GUID
        /// </summary>
        /// <returns>返回一个有序的GUID</returns>
        public Guid Create()
        {
            Guid result;
            UuidCreateSequential(out result);
            return result;
        }
    }
}
