/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/21 12:50:27
 * ****************************************************************/
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SharpSword.SDK
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApiRespBaseExtensions
    {
        /// <summary>
        /// <![CDATA[
        /// JSON直接转化成动态类型
        /// 调用方式：resp.ToDynamicObject()["Info"]
        /// 
        /// JSON:字符串
        /// var obj = new
        ///    {
        ///        a = 1,
        ///        b = "Hello, World!",
        ///        c = new[] { 1, 2, 3 },
        ///        d = new Dictionary<string, int> { { "x", 1 }, { "y", 2 } }
        ///     };
        ///     
        ///  调用后使用方式：
        ///     Console.WriteLine((int)o["a"]);
        ///     Console.WriteLine((string)o["b"]);
        ///     Console.WriteLine(o["c"].Values().Count());
        ///     Console.WriteLine((int)o["d"]["y"]);
        /// ]]>
        /// </summary>
        /// <returns>返回动态对象；无需定义实体，直接使用索引访问方式，尽量少用，因为一旦这样定义获取数据，其他人在使用SDK的时候，将无法理解</returns>
        public static JObject ToDynamicObject(this ResponseBase response)
        {
            return (JObject)JsonConvert.DeserializeObject(response.Resp_Body);
        }
    }
}
