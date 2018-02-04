/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/5/3 10:56:46
 * ****************************************************************/
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 对象类型，属性描述对象
    /// </summary>
    public class ComplexObjPropertyTypeDescriptor
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// 属性值类型
        /// </summary>
        public Type MemberType { get; set; }

        /// <summary>
        /// 属性类型对外展示的类型
        /// </summary>
        public string DisplayType { get; set; }

        /// <summary>
        /// 属性描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否是必填
        /// </summary>
        public bool IsRequire { get; set; }
    }
}
