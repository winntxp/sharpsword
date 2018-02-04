﻿/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/9/2017 8:41:45 AM
 * ****************************************************************/
using Dapper;
using SharpSword.O2O.Data.Entities;
using SharpSword.O2O.Services.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// DB实现自动完成管理器
    /// </summary>
    public class DbOrderFinishedManager : OrderFinishedManagerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IDbConnectionStringProvider _dbConnectionStringProvider;
        private readonly GlobalConfig _globalConfig;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConnectionFactory"></param>
        /// <param name="dbConnectionStringProvider"></param>
        /// <param name="globalConfig"></param>
        public DbOrderFinishedManager(IDbConnectionFactory dbConnectionFactory,
                                      IDbConnectionStringProvider dbConnectionStringProvider,
                                      GlobalConfig globalConfig)
        {
            this._dbConnectionFactory = dbConnectionFactory;
            this._dbConnectionStringProvider = dbConnectionStringProvider;
            this._globalConfig = globalConfig;
        }

        /// <summary>
        /// 空实现，因为数据库无需进行添加管理，我们直接对订单进行操作
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="finishedTime"></param>
        protected override void Add(string orderId, DateTime finishedTime) { }

        /// <summary>
        /// 空实现，因为数据库无需进行添加管理，我们直接对订单进行操作
        /// </summary>
        /// <param name="orderId"></param>
        protected override void Remove(string orderId) { }

        /// <summary>
        /// 获取数据库里待完成的订单集合
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<FinishedOrderInfo> GetFinishedOrders()
        {
            try
            {
                //检索出多少分钟未自动完成的订单
                int orderFinishedTime = this._globalConfig.OrderFinishedTime;

                //获取所有用户维度分库连接信息
                var dbConnectionStrings = _dbConnectionStringProvider.GetDbConnectionStringsByStartsWith(this._globalConfig.UserOrderDbSplitPrefix);

                //并行计算出每个库的最大订单编号(并行计算不要超过 512)
                return dbConnectionStrings.AsParallel().WithDegreeOfParallelism(dbConnectionStrings.Count()).Select(k =>
                {
                    return this._dbConnectionFactory.Create(k.ConnectionString).Query<Order>(@"SELECT TOP 50 OrderId,
                                                                                                             OrderDate,
                                                                                                             PayDate
                                                                                                FROM [Orders] WHERE OrderStatus=2 AND DATEADD(MI,@MI,OrderDate)<GETDATE() --ORDER BY PayDate ASC",
                                                                                                new { MI = orderFinishedTime }).ToList();
                })
                .Select(x => x.Select(k => new FinishedOrderInfo() { OrderId = k.OrderId, Ticks = k.PayDate.Value.AddMinutes(orderFinishedTime).Ticks }))
                .SelectMany(x => x);
            }
            catch (Exception ex)
            {
                //记录下日志
                this.Logger.Error(ex);

                //触发报警器
                this.WarningTrigger.Warning(this, ex.Message, ex);

                //返回空集合
                return new List<FinishedOrderInfo>();
            }

        }
    }
}