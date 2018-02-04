/* ***************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/29/2016 1:39:39 PM
 * ***************************************************************/
using System;
using System.Collections.Generic;

namespace SharpSword.Notifications
{
    /// <summary>
    /// 用于封装Notification Definnition 的信息。注意和Notification 的区别，如果把Notification看成是具体的消息内容，NotificationDefinition则是对这个消息自身的定义（可理解为消息的类型）。
    /// </summary>
    public class NotificationDefinition
    {
        /// <summary>
        /// 消息类型名称，必须全局唯一
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 如果是基于实体的消息，实体类型。可以为null
        /// </summary>
        public Type EntityType { get; private set; }

        /// <summary>
        /// 用于外部显示的显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 消息类型描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 根据索引key获取到Attributes属性指定的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get { return Attributes.GetValueOrDefault(key); }
            set { Attributes[key] = value; }
        }

        /// <summary>
        /// 自定义的一些属性信息
        /// </summary>
        public IDictionary<string, object> Attributes { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="entityType"></param>
        /// <param name="displayName"></param>
        /// <param name="description"></param>
        public NotificationDefinition(string name, Type entityType = null, string displayName = null, string description = null)
        {
            name.CheckNullThrowArgumentNullException(nameof(name));
            this.Name = name;
            this.EntityType = entityType;
            this.DisplayName = displayName;
            this.Description = description;
            this.Attributes = new Dictionary<string, object>();
        }
    }
}
