/* ****************************************************************
 * SharpSword zhangliang4629@163.com 8/22/2016 3:27:23 PM
 * ****************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SharpSword
{
    /// <summary>
    /// Object类型数组列表
    /// </summary>
    [Serializable]
    public class NamedList : NamedList<object> { }

    /// <summary>
    /// 名称对象泛型集合，非线程安全
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    [Serializable]
    public class NamedList<T>
    {
        /// <summary>
        /// 用于保存名值对的数组列表
        /// </summary>
        protected readonly IList NameValuePairs = new ArrayList();

        /// <summary>
        /// 数量
        /// </summary>
        public int Count
        {
            get
            {
                return this.NameValuePairs.Count >> 1;
            }
        }

        /// <summary>
        /// 获取名称
        /// </summary>
        /// <param name="index">位置</param>
        /// <returns>名称</returns>
        public string GetName(int index)
        {
            return (string)this.NameValuePairs[index << 1];
        }

        /// <summary>
        /// 获取指定名称元素位置位置
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="start">起始位置</param>
        /// <returns>搜索到的第一个name索引号</returns>
        public int IndexOf(string name, int start)
        {
            for (int i = start; i < this.Count; i++)
            {
                string j = this.GetName(i);
                if (name == null)
                {
                    if (j == null)
                    {
                        return i;
                    }
                }
                else
                {
                    if (name.Equals(j, StringComparison.CurrentCulture))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// 添加元素
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        public void Add(string name, T value)
        {
            this.NameValuePairs.Add(name);
            this.NameValuePairs.Add(value);
        }

        /// <summary>
        /// 添加迭代元素
        /// </summary>
        /// <param name="args">迭代元素</param>
        /// <returns>添加元素数量是否大于0</returns>
        public bool Add(IEnumerable<KeyValuePair<string, T>> args)
        {
            foreach (KeyValuePair<string, T> pair in args)
            {
                this.Add(pair.Key, pair.Value);
            }
            return args.Count<KeyValuePair<string, T>>() > 0;
        }

        /// <summary>
        /// 添加名称集合
        /// </summary>
        /// <param name="namedList">名称集合</param>
        /// <returns>名称计划数量</returns>
        public bool Add(NamedList<T> namedList)
        {
            foreach (object obj in namedList.NameValuePairs)
            {
                this.NameValuePairs.Add(obj);
            }
            return namedList.Count > 0;
        }

        /// <summary>
        /// 设置名称
        /// </summary>
        /// <param name="index">位置</param>
        /// <param name="name">名称</param>
        public void SetName(int index, string name)
        {
            this.NameValuePairs[index << 1] = name;
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="index">位置</param>
        /// <param name="value">值</param>
        /// <returns>原有元素值</returns>
        public T SetValue(int index, T value)
        {
            int idx = (index << 1) + 1;
            T oldValue = (T)this.NameValuePairs[idx];
            this.NameValuePairs[idx] = value;
            return oldValue;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="index">位置</param>
        /// <returns>值</returns>
        public T GetValue(int index)
        {
            return (T)this.NameValuePairs[(index << 1) + 1];
        }

        /// <summary>
        /// 获取指定名称元素值
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>元素值</returns>
        public T GetValue(string name)
        {
            return this.GetValue(name, 0);
        }

        /// <summary>
        /// 从指定位置起获取指定名称元素
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="start">其实位置</param>
        /// <returns>元素值</returns>
        public T GetValue(string name, int start)
        {
            for (int i = start; i < this.Count; i++)
            {
                string j = this.GetName(i);
                if (name == null)
                {
                    if (j == null)
                    {
                        return this.GetValue(i);
                    }
                }
                else
                {
                    if (name.Equals(j))
                    {
                        return this.GetValue(i);
                    }
                }
            }
            return default(T);
        }

        /// <summary>
        /// 获取所有指定名称的元素
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>符合条件的元素集合</returns>
        public IEnumerable<T> GetValues(string name)
        {
            IList<T> result = new List<T>();
            for (int i = 0; i < this.Count; i++)
            {
                string j = this.GetName(i);
                if (name == j || (name != null && name.Equals(j)))
                {
                    result.Add(this.GetValue(i));
                }
            }
            return result;
        }

        /// <summary>
        /// 移除指定名称元素
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>移除元素</returns>
        public T Remove(string name)
        {
            int idx = this.IndexOf(name, 0);
            if (idx != -1)
            {
                return this.Remove(idx);
            }
            return default(T);
        }

        /// <summary>
        /// 移除元素
        /// </summary>
        /// <param name="index">位置</param>
        /// <returns>移除元素值</returns>
        public T Remove(int index)
        {
            int idx = index << 1;
            this.NameValuePairs.Remove(idx);
            T val = (T)this.NameValuePairs[idx];
            this.NameValuePairs.Remove(idx);
            return val;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.NameValuePairs.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(NamedList<T>))
            {
                return false;
            }
            NamedList<T> nl = (NamedList<T>)obj;
            return this.NameValuePairs.Equals(nl.NameValuePairs);
        }
    }
}