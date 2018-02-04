/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 7/25/2017 2:37:20 PM
 * ****************************************************************/
using MongoDB.Driver;

namespace SharpSword.AccessRecorder.MongoDB
{
    /// <summary>
    /// 我们继承出一个客户端，方便其他地方直接使用
    /// </summary>
    public class AccessRecorderMongoClient : MongoClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString">mongodb数据库连接字符串</param>
        public AccessRecorderMongoClient(string connectionString) : base(connectionString) { }
    }
}
