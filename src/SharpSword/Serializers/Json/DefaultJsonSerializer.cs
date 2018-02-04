/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/7/12 9:43:08
 * ****************************************************************/
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using System.Linq;

namespace SharpSword.Serializers.Json
{
    /// <summary>
    /// 默认的序列化反序列化实现(Newtonsoft.Json)
    /// </summary>
    public class DefaultJsonSerializer : IJsonSerializer
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="value">输入的JSON字符串</param>
        /// <param name="type">需要反序列化的类型</param>
        /// <returns></returns>
        public virtual object Deserialize(string value, Type type)
        {
            return JsonConvert.DeserializeObject(value, type);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">需要反序列化的类型</typeparam>
        /// <param name="value">输入的JSON字符串</param>
        /// <returns></returns>
        public virtual T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string FormatSerialize(string value)
        {
            JsonSerializer serializer = new JsonSerializer();
            TextReader textReader = new StringReader(value);
            JsonTextReader jsonTextReader = new JsonTextReader(textReader);
            object obj = serializer.Deserialize(jsonTextReader);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string FormatSerialize(object value)
        {
            var json = this.Serialize(value);
            return this.FormatSerialize(json);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="value">待序列化的对象</param>
        /// <returns>返回序列化后的JSON字符串</returns>
        public virtual string Serialize(object value)
        {
            //EF 导航属性里有个循环依赖的问题，我们将序列化忽略掉循环依赖
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                //忽略掉循环引用
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                //忽略掉可为空的属性
                NullValueHandling = NullValueHandling.Ignore,
                //允许最深的深度
                MaxDepth = 100,
                //时间格式化成指定的字符串格式
                Converters = new JsonConverter[]
                {
                    new IsoDateTimeConverter {DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff"}
                }.ToList()
            };
            return JsonConvert.SerializeObject(value, jsonSettings);
        }
    }
}
