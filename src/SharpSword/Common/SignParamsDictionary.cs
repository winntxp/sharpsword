/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 4:13:10 PM
 * ****************************************************************/
using System.Collections.Generic;
using System.Linq;

namespace SharpSword
{
    /// <summary>
    /// 数据签名字典类；内部使用排序字段，方便安装参数名称排序
    /// </summary>
    public class SignParamsDictionary<TKey, TValue>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly SortedDictionary<TKey, TValue> _dit;

        /// <summary>
        /// 数据签名字典类；内部使用排序字段，方便安装参数名称排序
        /// </summary>
        public SignParamsDictionary()
        {
            this._dit = new SortedDictionary<TKey, TValue>();
        }

        /// <summary>
        /// 方便链式调用，添加一个键值对
        /// </summary>
        public SignParamsDictionary<TKey, TValue> Append(TKey key, TValue value)
        {
            this._dit.Add(key, value);
            return this;
        }

        /// <summary>
        /// 获取所有的键信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TKey> GetAllKeys()
        {
            return this._dit.Select(item => item.Key).ToList();
        }

        /// <summary>
        /// 根据键获取值信息
        /// </summary>
        /// <returns></returns>
        public TValue this[TKey key]
        {
            get
            {
                return this._dit[key];
            }
        }

        /// <summary>
        /// 获取所有的键值信息
        /// </summary>
        public IEnumerable<KeyValuePair<TKey, TValue>> Values
        {
            get
            {
                return this._dit.Select(item => new KeyValuePair<TKey, TValue>(item.Key, item.Value)).ToList();
            }
        }
    }
}