/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/15/2017 10:45:14 AM
 * ****************************************************************/

namespace SharpSword.O2O.Services.Domain
{
    /// <summary>
    /// 创建用户ID参数对象，我们定义成对象，防止以后使用用户不同属性作为生成因子
    /// </summary>
    public class UserIdGeneratorCreateUserInfo
    {
        /// <summary>
        /// 用户昵称，全局唯一（我们将其作为生产UserId因子，当我们需要进行按照UserName查询的时候，能立即定位存储介质位置，或者实现里不使用亦可，看具体实现）
        /// </summary>
        public string UserName { get; set; }
    }
}
