/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/17/2017 8:52:50 AM
 * ****************************************************************/
using SharpSword.Data;
using SharpSword.DistributedLock;
using SharpSword.WebApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SharpSword.Host.Apis
{
    /// <summary>
    /// 
    /// </summary>
    [ActionName("RedisLocker"), ResponseFormat(ResponseFormat.JSON | ResponseFormat.XML), EnableRecordApiLog(false)]
    [Description("RedisLocker")]
    public class RedisLocker : ActionBase<RedisLocker.RedisLockerRequestDto, string>
    {
        /// <summary>
        /// 上送参数对象
        /// </summary>
        public class RedisLockerRequestDto : RequestDtoBase
        {
            /// <summary>
            /// 自定义校验上送参数
            /// </summary>
            /// <returns></returns>
            public override IEnumerable<DtoValidatorResultError> Valid()
            {
                return base.Valid();
            }
        }

        /// <summary>
        /// REDIS服务器设置多个单实例，这样方式单机故障
        /// </summary>
        private IDistributedLockerManager _distributedLockerManager;

        /// <summary>
        /// 
        /// </summary>
        private readonly IDbContext _dbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="distributedLockerManager"></param>
        public RedisLocker(IDbContext dbContext, IDistributedLockerManager distributedLockerManager)
        {
            this._dbContext = dbContext;
            this._distributedLockerManager = distributedLockerManager;
        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <returns></returns>
        public override ActionResult<string> Execute()
        {
            ActionResult<string> resp = null;

            //先需要获取分布式锁，相当于先拿到授权了再进行操作(5秒钟的自动解锁时间，防止IIS重启锁一直被某个用户锁住，这个时间需要根据实际业务的处理长度来计算一个比较合适的值)
            _distributedLockerManager.Lock("MyRedisLocker", new TimeSpan(0, 0, 5), () =>
            {
                //-------------------模拟实际业务的读，写------------------------------

                //模拟先从存储读取当前最大值
                var id = this._dbContext.ExecuteScalar<int>("select isnull(max(Id),0) from Nums");

                //把取出来的最大值+1，故意腾出时间差
                id++;

                //再把+1的值写进存储
                this._dbContext.Execute("insert into Nums(Id) values(@Id)", new { Id = id });

                //成功后，提交订单数据到消息队列
                resp = this.SuccessActionResult(id.ToString());

            }, () =>
            {
                //取不到锁，其实相当于我们限流了
                resp = this.ErrorActionResult("获取锁失败，说明有人已经在操作库存，这里可以采取提醒的方式，直接返回提交错误，比如：哎呀，购买的亲太多了，请稍后提交 ......");
            });

            return resp;
        }

    }
}
