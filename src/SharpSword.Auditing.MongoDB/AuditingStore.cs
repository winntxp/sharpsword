/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/22/2016 10:11:53 AM
 * *******************************************************/
using MongoDB.Driver;

namespace SharpSword.Auditing.MongoDB
{
    /// <summary>
    /// MongoDB审计存储实现
    /// </summary>
    internal class AuditingStore : IAuditingStore
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly AuditingStoreConfig _auditingStoreConfig;
        private readonly ISession _session;
        private readonly IMongoClient _mongoClient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="auditingStoreConfig"></param>
        /// <param name="mongoClient"></param>
        public AuditingStore(ISession session, AuditingStoreConfig auditingStoreConfig, AuditingStoreMongoClient mongoClient)
        {
            this._session = session;
            this._auditingStoreConfig = auditingStoreConfig;
            this._mongoClient = mongoClient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditInfo"></param>
        /// <returns></returns>
        public void Save(AuditInfo auditInfo)
        {
            if (!this._auditingStoreConfig.IsEnabled)
            {
                return;
            }

            //保存审计信息
            var database = this._mongoClient.GetDatabase(this._auditingStoreConfig.DataBase);
            var collection = database.GetCollection<AuditInfo>(this._auditingStoreConfig.CollectionName);
            //var document = new BsonDocument(auditInfo.GetAttributes());
            collection.InsertOneAsync(auditInfo);
        }
    }
}
