/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/5/2017 4:57:23 PM
 * ****************************************************************/
using SharpSword.O2O.Data.Entities;
using System.Text.RegularExpressions;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class OrderIdGeneratorBase : IOrderIdGenerator
    {
        /// <summary>
        /// 实现方式，为了考虑以后的分库分表我们需要加入：用户ID，门店ID，以及其他的一些生成因子，
        /// 方便我们可以根据, 订单编号，就能立即定位订单放在哪个存储
        /// {校验码}[1]+{用户ID}[2]+{区域码}[2]+{流水号}[7] >= 13
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public string Create(Order order)
        {
            order.CheckNullThrowArgumentNullException(nameof(order));

            //用户ID后3位
            string userId = OrderSplitFactorServices.Instance.GetUserFactor(order.UserId).ToString("00");

            //区域ID后3位
            string areaId = OrderSplitFactorServices.Instance.GetAreaFactor(order.OperationAreaId.Value).ToString("00");

            //生成流水码
            string sequenceId = this.GetSequenceId(order);

            //生成订单编号
            string orderId = "{0}{1}{2}".With(userId, areaId, sequenceId);

            //生成最终的订单编号
            return "{0}{1}".With(CreateCheckCode(orderId), orderId);
        }

        /// <summary>
        /// 获取流水码
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        protected abstract string GetSequenceId(Order order);

        /// <summary>
        /// 生成校验码；
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        protected static string CreateCheckCode(string orderId)
        {
            int total = 0;
            for (var i = 0; i < orderId.Length; i++)
            {
                total += orderId[i].To<int>() * i;
            }
            return (total % 10 == 0 ? 1 : total % 10).ToString();
        }

        /// <summary>
        /// 检测订单编号是否合法
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public bool Check(string orderId)
        {
            if (orderId.IsNullOrEmpty())
            {
                return false;
            }

            if (!Regex.IsMatch(orderId, "^[0-9]{13,50}$"))
            {
                return false;
            }

            //订单编号上的原始校验码
            var rawcheckCode = orderId.Substring(0, 1);

            //重新计算下订单编号的校验码
            var newCheckCode = CreateCheckCode(orderId.Substring(1, orderId.Length - 1));

            //检测下是否一致
            return rawcheckCode == newCheckCode;
        }
    }
}
