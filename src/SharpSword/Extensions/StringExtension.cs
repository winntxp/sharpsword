/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using SharpSword.Serializers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SharpSword
{
    /// <summary>
    /// 字符类型扩展类
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 是否含有指定字符串
        /// </summary>
        /// <param name="value">当前字符串</param>
        /// <param name="searchValue">待检索的字符串</param>
        /// <param name="stringComparison">是否忽略大小写</param>
        /// <returns></returns>
        public static bool Contains(this string value, string searchValue, StringComparison stringComparison)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }
            if (searchValue.IsNullOrEmpty())
            {
                return false;
            }
            return value.IndexOf(searchValue, stringComparison) != -1;
        }

        /// <summary>
        /// 将字符串进行URL编码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UrlEncode(this string value)
        {
            return HttpUtility.UrlEncode(value);
        }

        /// <summary>
        /// 将字符串进行HTML编码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string HtmlEncode(this string value)
        {
            return HttpUtility.HtmlEncode(value);
        }

        /// <summary>
        /// 根据切分字符成int数组类型；不会返回失败信息，转型错误的直接忽略掉
        /// </summary>
        /// <param name="value">待拆分的字符串</param>
        /// <param name="splitStr">拆分字符</param>
        /// <returns></returns>
        public static T[] ToArray<T>(this string value, char splitStr) where T : struct
        {
            return value.ToArray<T>(new char[] { splitStr });
        }

        /// <summary>
        /// 将字符串按照指定的字符进行切分
        /// </summary>
        /// <typeparam name="T">转型的数据类型</typeparam>
        /// <param name="value">待拆分的字符串</param>
        /// <param name="splitStr">拆分字符数组</param>
        /// <returns></returns>
        public static T[] ToArray<T>(this string value, char[] splitStr) where T : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                return new T[0];
            }
            var strArr = value.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
            IList<T> tempList = new List<T>();
            foreach (var item in strArr)
            {
                if (item.Is<T>())
                {
                    tempList.Add(item.As<T>());
                }
            }
            return tempList.ToArray();
        }

        /// <summary>
        /// 判断一个字符串数组是否可以转型成数字数组（如果可以转型成double类型即代表可以转型成功）
        /// </summary>
        /// <param name="strArr">字符串数组</param>
        /// <returns></returns>
        public static bool IsNumericArray(this string[] strArr)
        {
            if (strArr.IsNull() || strArr.Length == 0)
            {
                return false;
            }
            return strArr.All(item => item.Is<double>() || item.Is<float>()
                                                        || item.Is<decimal>()
                                                        || item.Is<int>()
                                                        || item.Is<long>());
        }

        /// <summary>
        /// 将当前字符串重复count次
        /// </summary>
        /// <param name="value">当前字符串</param>
        /// <param name="count">重复次数</param>
        /// <returns></returns>
        public static string Replicate(this string value, int count)
        {
            if (count < 0)
            {
                throw new SharpSwordCoreException("count参数错误");
            }
            string text = value;
            for (int i = 0; i < count; i++)
            {
                text = text + value;
            }
            return text;
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        /// <param name="value">当前字符串</param>
        /// <param name="separator">连接符号</param>
        /// <param name="b">待连接的字符串</param>
        /// <returns>返回形如：value+separator+b</returns>
        public static string Join(this string value, string separator, string b)
        {
            if (value.IsNullOrEmpty() && b.IsNullOrEmpty())
                return "";

            if (value.IsNullOrEmpty())
                return b ?? "";

            if (b.IsNullOrEmpty())
                return value ?? "";

            return value + separator + b;
        }

        /// <summary>
        /// 整理换行，将换行符整理成标准的Environment.NewLine模式
        /// </summary>
        /// <param name="value">当前字符串</param>
        /// <returns></returns>
        public static string NormalizeLineEndings(this string value)
        {
            return value.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
        }

        /// <summary>
        /// 对个需要格式化的字符串格式化
        /// </summary>
        /// <param name="value">待格式化的字符串</param>
        /// <param name="args">格式化参数</param>
        /// <returns></returns>
        public static string With(this string value, params object[] args)
        {
            value.CheckNullThrowArgumentNullException(nameof(value));
            return string.Format(value, args);
        }

        /// <summary>
        /// 添加指定字符串
        /// </summary>
        /// <param name="value">待格式化的字符串</param>
        /// <param name="appendString">添加到字符串后的字符串</param>
        /// <returns></returns>
        public static string Append(this string value, string appendString)
        {
            value.CheckNullThrowArgumentNullException(nameof(value));
            return value + appendString;
        }

        /// <summary>
        /// 裁剪字符串
        /// </summary>
        /// <param name="value">当前字符串</param>
        /// <param name="length">裁剪长度</param>
        /// <param name="ellipsis">裁剪后的字符代替字符</param>
        /// <returns></returns>
        public static string CutString(this string value, int length, string ellipsis)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            if (value.Length <= length)
            {
                return value;
            }
            return value.Substring(0, length) + (ellipsis ?? string.Empty);
        }

        /// <summary>
        /// 裁剪字符
        /// </summary>
        /// <param name="value">当前字符串</param>
        /// <param name="length">需要拆解的长度</param>
        /// <returns></returns>
        public static string CutString(this string value, int length)
        {
            return CutString(value, length, string.Empty);
        }

        /// <summary>
        /// 当前字符串是否为空或者为null
        /// </summary>
        /// <param name="value">当前字符串</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// 当前字符串是否为null
        /// </summary>
        /// <param name="value">当前字符串</param>
        /// <returns></returns>
        public static bool IsNull(this string value)
        {
            return null == value;
        }

        /// <summary>
        /// 判断当前字符串是否为空，来返回实际对象
        /// </summary>
        /// <typeparam name="T">返回对象</typeparam>
        /// <param name="value">当前字符串</param>
        /// <param name="emptyFun">当前字符串为空的时候，返回默认对象</param>
        /// <param name="notEmptyFun">当前字符串不为空的时候，返回对象，输入字符串为当前字符串</param>
        /// <returns></returns>
        public static T IsNullOrEmptyForDefault<T>(this string value, Func<T> emptyFun, Func<string, T> notEmptyFun)
        {
            emptyFun.CheckNullThrowArgumentNullException(nameof(emptyFun));
            notEmptyFun.CheckNullThrowArgumentNullException(nameof(notEmptyFun));
            return value.IsNullOrEmpty() ? emptyFun() : notEmptyFun(value);
        }

        /// <summary>
        /// 转型成int
        /// </summary>
        /// <param name="value">待转型的字符串</param>
        /// <returns></returns>
        public static int AsInt(this string value)
        {
            return value.AsInt(0);
        }

        /// <summary>
        /// 转型成int，失败则使用默认值
        /// </summary>
        /// <param name="value">待转型的字符串</param>
        /// <param name="defaultValue">转型失败，返回此默认值</param>
        /// <returns></returns>
        public static int AsInt(this string value, int defaultValue)
        {
            int result;
            return int.TryParse(value, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 转型成Decimal;转型失败则返回：0m
        /// </summary>
        /// <param name="value">待转型的字符串</param>
        /// <returns></returns>
        public static decimal AsDecimal(this string value)
        {
            return value.As<decimal>(0m);
        }

        /// <summary>
        /// 转型成Decimal;转型失败则返回指定的默认值
        /// </summary>
        /// <param name="value">待转型的字符串</param>
        /// <param name="defaultValue">转型失败，返回此默认值</param>
        /// <returns></returns>
        public static decimal AsDecimal(this string value, decimal defaultValue)
        {
            return value.As(defaultValue);
        }

        /// <summary>
        /// 转换成Float;失败默认返回0
        /// </summary>
        /// <param name="value">待转型的字符串</param>
        /// <returns></returns>
        public static float AsFloat(this string value)
        {
            return value.AsFloat(0f);
        }

        /// <summary>
        /// 转换成Float;失败默认返回指定的默认值
        /// </summary>
        /// <param name="value">待转型的字符串</param>
        /// <param name="defaultValue">转型失败，返回此默认值</param>
        /// <returns></returns>
        public static float AsFloat(this string value, float defaultValue)
        {
            float result;
            return float.TryParse(value, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 转型成DateTime，转型失败默认返回：default(DateTime)=0001/1/1 0:00:00
        /// </summary>
        /// <param name="value">待转型的字符串</param>
        /// <returns></returns>
        public static DateTime AsDateTime(this string value)
        {
            return value.AsDateTime(default(DateTime));
        }

        /// <summary>
        /// 转型成DateTime
        /// </summary>
        /// <param name="value">待转型的字符串</param>
        /// <param name="defaultValue">转型失败，返回此默认值</param>
        /// <returns></returns>
        public static DateTime AsDateTime(this string value, DateTime defaultValue)
        {
#pragma warning disable IDE0018 // 内联变量声明
            DateTime result;
#pragma warning restore IDE0018 // 内联变量声明
            return DateTime.TryParse(value, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 将字符串转型成指定的基元类型
        /// </summary>
        /// <typeparam name="TValue">指定数据类型</typeparam>
        /// <param name="value">待转型的字符串</param>
        /// <returns></returns>
        public static TValue As<TValue>(this string value)
        {
            return value.As(default(TValue));
        }

        /// <summary>
        /// 字符串与指定的类型是否可以相互转换
        /// </summary>
        /// <typeparam name="TValue">指定的基元类型</typeparam>
        /// <param name="value">待转型的字符串</param>
        /// <param name="defaultValue">无法转型，就默认使用默认指定值</param>
        /// <returns></returns>
        public static TValue As<TValue>(this string value, TValue defaultValue)
        {
            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(TValue));
                if (converter.CanConvertFrom(typeof(string)))
                {
                    TValue result = (TValue)((object)converter.ConvertFrom(value));
                    return result;
                }
                converter = TypeDescriptor.GetConverter(typeof(string));
                if (converter.CanConvertTo(typeof(TValue)))
                {
                    TValue result = (TValue)((object)converter.ConvertTo(value, typeof(TValue)));
                    return result;
                }
            }
            catch
            {
            }
            return defaultValue;
        }

        /// <summary>
        /// 字符串与指定的类型是否可以相互转换
        /// </summary>
        /// <typeparam name="TValue">指定的基元类型</typeparam>
        /// <param name="value">待转型的字符串</param>
        /// <param name="defaultValue">无法转型，就默认使用委托获取默认值；key为：value字符串</param>
        /// <returns></returns>
        public static TValue As<TValue>(this string value, Func<string, TValue> defaultValue)
        {
            return value.As<TValue>(defaultValue(value));
        }

        /// <summary>
        /// 转型成bool，转型失败默认返回：false
        /// </summary>
        /// <param name="value">待转型的字符串</param>
        /// <returns></returns>
        public static bool AsBool(this string value)
        {
            return value.AsBool(false);
        }

        /// <summary>
        /// 转型成bool,转型失败则返回默认值
        /// </summary>
        /// <param name="value">待转型的字符串</param>
        /// <param name="defaultValue">转型失败，返回此默认值</param>
        /// <returns></returns>
        public static bool AsBool(this string value, bool defaultValue)
        {
#pragma warning disable IDE0018 // 内联变量声明
            bool result;
#pragma warning restore IDE0018 // 内联变量声明
            return bool.TryParse(value, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 字符串是否能转型成bool类型
        /// </summary>
        /// <param name="value">待转型的字符串</param>
        /// <returns></returns>
        public static bool IsBool(this string value)
        {
#pragma warning disable IDE0018 // 内联变量声明
            bool flag;
#pragma warning restore IDE0018 // 内联变量声明
            return bool.TryParse(value, out flag);
        }

        /// <summary>
        /// 字符串是否能转出int类型
        /// </summary>
        /// <param name="value">待转型的字符串</param>
        /// <returns></returns>
        public static bool IsInt(this string value)
        {
            int num;
            return int.TryParse(value, out num);
        }

        /// <summary>
        /// 字符串是否能转型成Decimal类型
        /// </summary>
        /// <param name="value">待转型的字符串</param>
        /// <returns></returns>
        public static bool IsDecimal(this string value)
        {
            return value.Is<decimal>();
        }

        /// <summary>
        /// 字符串是否能转型成Float类型
        /// </summary>
        /// <param name="value">待转型的字符串</param>
        /// <returns></returns>
        public static bool IsFloat(this string value)
        {
#pragma warning disable IDE0018 // 内联变量声明
            float num;
#pragma warning restore IDE0018 // 内联变量声明
            return float.TryParse(value, out num);
        }

        /// <summary>
        /// 字符串是否能转型成DateTime类型
        /// </summary>
        /// <param name="value">待转型的字符串</param>
        /// <returns></returns>
        public static bool IsDateTime(this string value)
        {
#pragma warning disable IDE0018 // 内联变量声明
            DateTime dateTime;
#pragma warning restore IDE0018 // 内联变量声明
            return DateTime.TryParse(value, out dateTime);
        }

        /// <summary>
        /// 字符串是否可以转型成指定的类型
        /// </summary>
        /// <typeparam name="TValue">指定的基元类型</typeparam>
        /// <param name="value">待转型的字符串</param>
        /// <returns></returns>
        public static bool Is<TValue>(this string value)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(TValue));
            if (null != converter)
            {
                try
                {
                    if (null == value || converter.CanConvertFrom(null, value.GetType()))
                    {
                        converter.ConvertFrom(null, CultureInfo.CurrentCulture, value);
                        return true;
                    }
                }
                catch
                {
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// 将JSON字符串转换成实体对象
        /// 注意：如果字符串不是合法的JSON字符串；无法反序列化出指定对象，则返回null，因此外部程序需要判断返回值
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="jsonValue">JSON字符串</param>
        /// <returns>返回指定的T对象</returns>
        public static T DeserializeJsonStringToObject<T>(this string jsonValue)
        {
            try
            {
                return (T)DeserializeJsonStringToObject(jsonValue, typeof(T));
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// 将JSON字符串转换成实体对象
        /// </summary>
        /// <param name="jsonValue">JSON字符串</param>
        /// <param name="type">待转换的实体类型</param>
        /// <returns></returns>
        public static object DeserializeJsonStringToObject(this string jsonValue, Type type)
        {
            try
            {
                return JsonSerializerManager.Provider.Deserialize(jsonValue, type);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 检测字符串是否未IP地址格式
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsIp(this string value)
        {
            IPAddress ip;
            return IPAddress.TryParse(value, out ip);
        }

        /// <summary>
        /// 根据指定的匹配模式校验是否符合某一匹配模式
        /// </summary>
        /// <param name="value">待匹配的字符串</param>
        /// <param name="pattern">匹配模式</param>
        /// <returns></returns>
        public static bool Valid(this string value, string pattern)
        {
            return Regex.IsMatch(value, pattern);
        }

        /// <summary>
        /// 检测字符串是否是手机号码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsMobileNumber(this string value)
        {
            return Valid(value, "^1[358]\\d{9}$");
        }

        /// <summary>
        /// 检测字符串是否是EMAIL地址
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmail(string value)
        {
            return Valid(value, @"^[\w\.]+([-]\w+)*@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]$");
        }

        /// <summary>
        /// 检测字符串是否为中文字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsChinese(this string value)
        {
            return Valid(value, @"^[\u4e00-\u9fa5]+$");
        }

        /// <summary>
        /// 对字符串进行MD5签名
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMD5(this string value)
        {
            return MD5.Encrypt(value);
        }

        /// <summary>
        /// 将字符串转化成BASE64字符串，默认采取：UTF-8编码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToBase64(this string value)
        {
            return Base64.Encrypt(value);
        }

        /// <summary>
        /// 将BASE64字符串转换成字节数组
        /// </summary>
        /// <param name="base64String">BASE64字符串</param>
        /// <returns></returns>
        public static byte[] GetBytesFromBase64String(this string base64String)
        {
            return Convert.FromBase64String(base64String);
        }

        /// <summary>
        /// 将字符串转换成字节，默认采取UTF8编码
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns></returns>
        public static byte[] GetBytes(this string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        /// <summary>
        /// 整理JSON格式，将一行JSON，整理成格式化便于阅读的JSON格式
        /// </summary>
        /// <param name="json">JSON字符串</param>
        /// <returns></returns>
        public static string FormatJsonString(this string json)
        {
            return JsonSerializerManager.Provider.FormatSerialize(json);
        }
    }
}
