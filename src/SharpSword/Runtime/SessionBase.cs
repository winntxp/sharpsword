/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/28/2016 11:34:21 AM
 * ****************************************************************/
using System;
using System.Collections.Generic;

namespace SharpSword
{
    /// <summary>
    /// 登录信息抽象基类
    /// </summary>
    [Serializable]
    public abstract class SessionBase : ISession
    {
        /// <summary>
        /// 
        /// </summary>
        private IDictionary<string, object> _otherDatas;

        /// <summary>
        /// 
        /// </summary>
        protected SessionBase()
        {
            _otherDatas = new Dictionary<string, object>();
        }

        /// <summary>
        /// 用于保存其他的一些数据信息
        /// </summary>
        public IDictionary<string, object> Properties
        {
            get
            {
                return _otherDatas;
            }
        }

        /// <summary>
        /// 当前登录用户编号信息
        /// </summary>
        public abstract string UserId { get; }

        /// <summary>
        /// 当前登录用户名称信息
        /// </summary>
        public abstract string UserName { get; }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
