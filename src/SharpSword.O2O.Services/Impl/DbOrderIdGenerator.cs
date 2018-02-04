/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 5:11:14 PM
 * ****************************************************************/
using Dapper;
using SharpSword.O2O.Data.Entities;
using System.Linq;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 默认使用数据库订单ID管理器(基于数据库的ID生成方案，在做数据扩容的时候，
    /// 我们需要注意一点：扩容完成后，每个库的订单流水ID需要使用多个库最大的那个值+1，防止扩容后数据重新分布后出现流水重复的情况，切记，切记)
    /// </summary>
    public class DbOrderIdGenerator : OrderIdGeneratorBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IUserOrderDbConnectionFactory _userOrderDbConnectionFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userOrderDbConnectionFactory"></param>
        public DbOrderIdGenerator(IUserOrderDbConnectionFactory userOrderDbConnectionFactory)
        {
            this._userOrderDbConnectionFactory = userOrderDbConnectionFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        protected override string GetSequenceId(Order order)
        {
            long id = 0;

            //先根据用户编号获取数据库拆分因子(插入自增表获取自增编号)
            using (var conn = this._userOrderDbConnectionFactory.Create(OrderSplitFactorServices.Instance.GetUserFactor(order.UserId)))
            {
                id = conn.Query<long>(@"INSERT INTO OrderIdSevice(States) VALUES(0);SELECT SCOPE_IDENTITY();")
                           .FirstOrDefault();
            }

            return id.ToString();
        }
    }
}
