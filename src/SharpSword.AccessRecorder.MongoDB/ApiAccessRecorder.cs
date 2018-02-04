/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/21 17:22:38
 * ****************************************************************/
using MongoDB.Driver;
using SharpSword.Auditing;
using SharpSword.Domain.Services;
using SharpSword.WebApi;

namespace SharpSword.AccessRecorder.MongoDB
{
    /// <summary>
    /// 将访问记录记录到数据库;方便管理统计接口访问量
    /// </summary>
    public class ApiAccessRecorder : IApiAccessRecorder, IPerLifetimeDependency
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly AccessRecorderConfig _config;
        private readonly ISession _session;
        private readonly IMongoClient _mongoClient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="accessRecorderConfig"></param>
        /// <param name="mongoClient"></param>
        public ApiAccessRecorder(ISession session, AccessRecorderConfig accessRecorderConfig, AccessRecorderMongoClient mongoClient)
        {
            this._session = session;
            this._config = accessRecorderConfig;
            this._mongoClient = mongoClient;
        }

        /// <summary>
        /// 实现API记录器
        /// 方法里尽量做到快速的记录，不要进行大的操作，从而影响到API整体框架的性能
        /// </summary>
        /// <param name="args"></param>
        public void Record(ApiAccessRecorderArgs args)
        {
            //设置为不记录日志
            if (!this._config.IsNull() && !this._config.IsEnabled)
            {
                return;
            }

            //保存审计信息
            var database = _mongoClient.GetDatabase(this._config.DataBase);
            var collection = database.GetCollection<ApiAccessRecorderArgs>(this._config.CollectionName);
            collection.InsertOneAsync(args);
        }
    }
}
