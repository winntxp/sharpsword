/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/6/12 17:51:30
 * ****************************************************************/
using Dapper;
using SharpSword.O2O.Services;
using SharpSword.Tasks;
using System;
using System.Linq;

namespace SharpSword.O2O.Tasks
{
    /// <summary>
    /// 此系统作业任务每隔30分钟执行一次ID种子表清理,防止种子表数据太过庞大(如果是基于REDIS的全局自增，此作业任务无需启动)
    /// </summary>
    [TaskScheduler("OrderIdSequenceClearTask", seconds: 60 * 1, enabled: false)]
    public class OrderIdSequenceClearTask : IBackgroundTask
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger _logger;
        private readonly IMachineNameProvider _machineNameProvider;
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IDbConnectionStringProvider _dbConnectionStringProvider;
        private readonly GlobalConfig _globalConfig;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConnectionFactory"></param>
        /// <param name="dbConnectionStringProvider"></param>
        /// <param name="globalConfig"></param>
        /// <param name="machineNameProvider"></param>
        /// <param name="logger"></param>
        public OrderIdSequenceClearTask(IDbConnectionFactory dbConnectionFactory,
                                        IDbConnectionStringProvider dbConnectionStringProvider,
                                        IMachineNameProvider machineNameProvider,
                                        GlobalConfig globalConfig,
                                        ILogger<OrderIdSequenceClearTask> logger)
        {
            this._dbConnectionFactory = dbConnectionFactory;
            this._dbConnectionStringProvider = dbConnectionStringProvider;
            this._globalConfig = globalConfig;
            this._machineNameProvider = machineNameProvider;
            this._logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskExecuteContext"></param>
        public void Execute(TaskExecuteContext taskExecuteContext)
        {
            try
            {
                //将当前<最大值的ID数据清理掉
                var dbConnectionStrings = _dbConnectionStringProvider.GetDbConnectionStringsByStartsWith(this._globalConfig.UserOrderDbSplitPrefix);

                //并行进行处理
                dbConnectionStrings.AsParallel().WithDegreeOfParallelism(dbConnectionStrings.Count()).ForAll(k =>
                {
                    using (var dbConnection = this._dbConnectionFactory.Create(k.ConnectionString))
                    {
                        //执行数据库操作，我们每次删除100条，防止每次删除过多数据，造成数据库日志膨胀(大事务)，影响其他业务
                        var result = dbConnection.Execute(@"DELETE TOP(10) FROM OrderIdSevice WHERE [ID]<(SELECT ISNULL(MAX(ID),-1) FROM OrderIdSevice)");

                        //记录到日志
                        this._logger.Information(@"清理数据库订单ID流水码成功，运行实例：{0}，数据库：{1}，本次清理数量：{2}"
                                                        .With(this._machineNameProvider.GetMachineName(), dbConnection.Database, result));
                    }
                });
            }
            catch (Exception ex)
            {
                this._logger.Error(ex, "清理数据库订单ID流水码失败");
            }
        }
    }
}
