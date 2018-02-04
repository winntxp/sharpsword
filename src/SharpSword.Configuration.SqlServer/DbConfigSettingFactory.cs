/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/21/2016 10:52:45 AM
 * ****************************************************************/
using SharpSword.Configuration.JsonConfig;
using SharpSword.Configuration.SqlServer.Domain;
using SharpSword.Domain.Repositories;
using SharpSword.Domain.Uow;
using SharpSword.Serializers;
using System;
using System.Linq;
using System.Transactions;

namespace SharpSword.Configuration.SqlServer
{
    /// <summary>
    /// 基于数据库配置参数获取工厂(我们数据库里保存配置参数，参数JSON格式进行存储)
    /// </summary>
    public class DbConfigSettingFactory : JsonConfigSettingFactory
    {
        private readonly IRepository<ConfigurationEntity> _configurationRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonJosnSerializer"></param>
        public DbConfigSettingFactory(IJsonSerializer jsonJosnSerializer,
                                      IConfigurationReader configurationReader,
                                      IRepository<ConfigurationEntity> configurationRepository,
                                      IUnitOfWorkManager unitOfWorkManager) : base(jsonJosnSerializer, configurationReader)
        {
            this._configurationRepository = configurationRepository;
            this._unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSetting"></typeparam>
        /// <returns></returns>
        protected override string GetSettingJsonString<TSetting>()
        {
            using (var uow = this._unitOfWorkManager.Begin(new UnitOfWorkOptions()
            {
                IsTransactional = false,
                Scope = TransactionScopeOption.Suppress
            }))
            {
                var config = this._configurationRepository.TableNoTracking
                                                          .Where(o => o.Id == typeof(TSetting).FullName).FirstOrDefault();
                if (config.IsNull())
                {
                    return string.Empty;
                }
                return config.Value;
            }
        }

        /// <summary>
        /// 工厂支持处理的数据类型
        /// </summary>
        public override Type Supported
        {
            get { return typeof(IDbConfiguration); }
        }
    }
}
