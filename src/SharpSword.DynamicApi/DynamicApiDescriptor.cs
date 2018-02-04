/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/7/2016 9:12:06 AM
 * ****************************************************************/
using System;
using System.Reflection;

namespace SharpSword.DynamicApi
{
    /// <summary>
    /// 动态接口描述对象
    /// </summary>
    public class DynamicApiDescriptor
    {
        /// <summary>
        /// 当前动态接口所属的类类型
        /// </summary>
        public Type DeclaringType { get; set; }

        /// <summary>
        /// 接口方法，类里的具体方法信息
        /// </summary>
        public MethodInfo MethodInfo { get; set; }

        /// <summary>
        /// 接口名称,接口名称生成规则：
        /// 1.如果待映射成API接口的方法，定义了ActionNameAttribute特性，就采取自定义的接口名称
        /// 2.如果未定义ActionNameAttribute特性，默认名称为：类名.方法名文件形式
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 上送参数类型-对应于方法的入参
        /// </summary>
        public Type RequestDtoType { get; set; }

        /// <summary>
        /// 返回数据类型-对应方法的返回值，有可能是void（无返回值）
        /// </summary>
        public Type ResponseDtoType { get; set; }
    }
}
