/****************************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/28/2015 1:08:56 PM
 * ****************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SharpSword
{
    /// <summary>
    /// <![CDATA[
    /// T为枚举类型；调用方式： t = new Enum<HttpMethod>();
    /// ]]>
    /// </summary>
    public class Enum<T> : IEnumerable<T> where T : struct
    {
        /// <summary>
        /// 缓存下，防止下次再次反射调用描述(此缓存是一个公共大缓存，即将所有操作过的枚举都会安装枚举类型作为主键就行存储)
        /// </summary>
        private static readonly IDictionary<Type, IEnumerable<EnumDescriptor>> CachedEnumDescriptors = new Dictionary<Type, IEnumerable<EnumDescriptor>>();
        private static readonly object Locker = new object();

        /// <summary>
        /// 构造函数里会进行T数据类型校验,非枚举类型直接抛出异常
        /// </summary>
        public Enum()
        {
            this.CheckT();
        }

        /// <summary>
        /// 检测T类型是否是枚举类型
        /// </summary>
        private void CheckT()
        {
            if (typeof(T).BaseType != typeof(Enum))  //!typeof(T).IsEnum
            {
                throw new SharpSwordCoreException("类型T必须为枚举类型");
            }
        }

        /// <summary>
        /// 不区分大小写
        /// </summary>
        /// <param name="enumStr">不区分大小写</param>
        /// <param name="defaultFun">当指定值，在枚举里不存在的时候，使用委托返回一个指定的枚举值</param>
        /// <returns></returns>
        public T GetItem(string enumStr, Func<T> defaultFun)
        {
            if (enumStr.IsNullOrEmpty())
            {
                return defaultFun();
            }

            foreach (var item in this.Where(item => item.ToString().Equals(enumStr, StringComparison.OrdinalIgnoreCase)))
            {
                return item;
            }

            return defaultFun();
        }

        /// <summary>
        /// 根据值名称获取
        /// </summary>
        /// <param name="enumValue">枚举值</param>
        /// <param name="defaultFun">当指定值，在枚举里不存在的时候，使用委托返回一个指定的枚举值</param>
        /// <returns></returns>
        public T GetItem(int enumValue, Func<T> defaultFun)
        {
            foreach (var item in this.Where(item => enumValue == (int)Convert.ChangeType(item, typeof(int))))
            {
                return item;
            }

            return defaultFun();
        }

        /// <summary>
        /// 是否包含指定枚举字符串，忽略大小写
        /// </summary>
        /// <param name="enumStr">枚举字符串</param>
        /// <returns></returns>
        public bool Contains(string enumStr)
        {
            return this.Any(item => item.ToString().Equals(enumStr, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 是否包含指定的枚举值
        /// </summary>
        /// <param name="enumValue">枚举值</param>
        /// <returns></returns>
        public bool Contains(int enumValue)
        {
            return this.Any(item => enumValue == (int) Convert.ChangeType(item, typeof (int)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 枚举值上面需要加DescriptionAttribute特性标签
        /// </summary>
        /// <returns>key:枚举值，value:枚举描述,未定义枚举描述，返回枚举字符串</returns>
        public IEnumerable<EnumDescriptor> GetDescriptor()
        {
            //当前T枚举的类型
            Type enumType = typeof(T);

            //缓存里存在，直接返回缓存数据
            if (CachedEnumDescriptors.ContainsKey(enumType))
            {
                return CachedEnumDescriptors[enumType];
            }

            //不存在就读取数据
            lock (Locker)
            {
                IList<EnumDescriptor> enumDescriptors = new List<EnumDescriptor>();
                foreach (var item in this)
                {
                    //描述默认枚举字符串
                    string description = item.ToString();
                    //获取描述特性信息
                    var descriptionAttribute = (DescriptionAttribute[])item.GetType().GetField(item.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
                    //存在描述特性返回自定义的描述特性
                    if (descriptionAttribute.Length > 0)
                    {
                        description = descriptionAttribute[0].Description;
                    }
                    enumDescriptors.Add(new EnumDescriptor() { Key = Convert.ToInt32(item), Value = item.ToString(), Description = description });
                }

                //再次判断下缓存里是否已经存在了，不存在再赋值给缓存
                if (!CachedEnumDescriptors.ContainsKey(enumType))
                {
                    CachedEnumDescriptors.Add(enumType, enumDescriptors);
                }

                //返回枚举描述信息
                return enumDescriptors;
            }
        }
    }
}