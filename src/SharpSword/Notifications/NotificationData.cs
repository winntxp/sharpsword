/* ***************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/27/2016 10:06:08 AM
 * ***************************************************************/
using System;
using System.Collections.Generic;

namespace SharpSword.Notifications
{
    /// <summary>
    /// 消息数据对象，一般来说，需要扩展此数据对象，来实现不同的消息类型
    /// </summary>
    [Serializable]
    public class NotificationData
    {
        /// <summary>
        /// 
        /// </summary>
        private IDictionary<string, object> _properties;

        /// <summary>
        /// 初始化属性对象
        /// </summary>
        public NotificationData()
        {
            this.Properties = new Dictionary<string, object>();
        }

        /// <summary>
        /// 当前消息通知类型（获取当前类型，方便消息通讯的时候，客户端对消息类型进行判断来进行不同的处理）
        /// </summary>
        public virtual string Type
        {
            get { return this.GetType().FullName; }
        }

        /// <summary>
        /// 根据键获取到对应的数据对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get { return Properties[key]; }
            set { Properties[key] = value; }
        }

        /// <summary>
        /// 包含的数据集
        /// </summary>
        public IDictionary<string, object> Properties
        {
            get { return _properties; }
            set
            {
                value.CheckNullThrowArgumentNullException(nameof(value));
                _properties = value;
            }
        }

        /// <summary>
        /// 我们重写下ToString方法，返回当前数据对象的JSON序列化字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Serialize2Josn();
        }
    }
}
