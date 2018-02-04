/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/18/2016 2:33:11 PM
 * ****************************************************************/
using System;
using System.Collections.Generic;

namespace SharpSword.DtoGenerator.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class DtoViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public DtoViewModel()
        {
            this.Properties = new List<DtoProperty>();
        }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// 类名称
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 继承的类
        /// </summary>
        public string Inherit { get; set; }

        /// <summary>
        /// 待映射的SQL语句
        /// </summary>
        public string SQL { get; set; }

        /// <summary>
        /// SQL返回结果集映射的C#对象类型（动态生成）
        /// </summary>
        public Type DtoType { get; set; }

        /// <summary>
        /// SQL对象映射的对象属性集合
        /// </summary>
        public IList<DtoProperty> Properties { get; private set; }

    }

    /// <summary>
    /// 类属性字段信息
    /// </summary>
    public class DtoProperty
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="typeName">属性数据类型</param>
        /// <param name="fclTypeName">FCL数据类型</param>
        public DtoProperty(string name, string typeName, string fclTypeName)
        {
            this.Name = name;
            this.TypeName = typeName;
            this.FCLTypeName = fclTypeName;
        }

        /// <summary>
        /// 属性名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 属性数据类型
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// FCL数据类型
        /// </summary>
        public string FCLTypeName { get; set; }

    }
}