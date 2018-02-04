/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/21 13:27:14
 * ****************************************************************/
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 分组信息；相同的一组接口可以定义到一起，方便归类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public sealed class ActionGroupAttribute : Attribute
    {
        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupName">分组名称</param>
        public ActionGroupAttribute(string groupName)
        {
            this.GroupName = groupName;
        }
    }
}
