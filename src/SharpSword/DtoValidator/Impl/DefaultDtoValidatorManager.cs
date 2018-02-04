/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/21/2016 10:43:08 AM
 * ****************************************************************/
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.DtoValidator.Impl
{
    /// <summary>
    /// 系统框架默认实现的验证器管理类
    /// </summary>
    internal class DefaultDtoValidatorManager : IDtoValidatorManager
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IEnumerable<IDtoValidator> _dtoValidators;
        private readonly GlobalConfiguration _globalConfiguration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtoValidators">系统框架注册的所有验证器，可以为null，为null表示满意校验器</param>
        /// <param name="globalConfiguration">全局配置</param>
        public DefaultDtoValidatorManager(IEnumerable<IDtoValidator> dtoValidators, GlobalConfiguration globalConfiguration)
        {
            this._dtoValidators = dtoValidators;
            this._globalConfiguration = globalConfiguration;
        }

        /// <summary>
        /// 获取所有的RequestDto验证管理器
        /// </summary>
        public IEnumerable<IDtoValidator> DtoValidators
        {
            get { return this._dtoValidators; }
        }

        /// <summary>
        /// 校验上送参数
        /// </summary>
        /// <param name="requestDto">待校验的参数对象</param>
        /// <returns></returns>
        public virtual DtoValidatorResult Valid(object requestDto)
        {
            //如果实现了验证接口，就先在执行校验前，先执行下手工修改参数的方法
            var validatableObject = requestDto as IDtoValidatable;
            if (!validatableObject.IsNull())
            {
                validatableObject.BeforeValid();
            }

            //检测是否实现了IRequiredUserIdAndUserName接口,校验用户名和用户ID或者在接口定义了需要校验的特性
            if (requestDto is IRequiredUser)
            {
                //验证用户ID和用户名称的委托为空弹出消息
                if (this._globalConfiguration.ValidUserIdAndUserNameFun.IsNull())
                {
                    return new DtoValidatorResult(new[]
                    {
                        new DtoValidatorResultError("ValidUserIdAndUserNameFun",
                            Resource.CoreResource.ActionBase_ValidUserIdAndUserNameFun_Null_Error)
                    });
                }

                //检测用户ID和用户名称是否提交
                if (!this._globalConfiguration.ValidUserIdAndUserNameFun((IRequiredUser)requestDto))
                {
                    return new DtoValidatorResult(new[]
                    {
                        new DtoValidatorResultError("UserId,UserName",
                            Resource.CoreResource.ActionBase_RequiredUserIdAndUserName_Error)
                    });
                }
            }

            //IDtoValidatable，就先触发手工验证
            if (!validatableObject.IsNull())
            {
                var validResult = validatableObject.Valid();

                //手工校验未通过，直接返回校验错误
                if (!validResult.IsNull() && validResult.Any())
                {
                    return new DtoValidatorResult(validResult);
                }
            }

            //没有定义验证器
            if (this._dtoValidators.IsNull() || this._dtoValidators.IsEmpty())
            {
                return DtoValidatorResult.Success;
            }

            //多个验证器循环验证
            foreach (var item in this._dtoValidators.OrderByDescending(o => o.Priority))
            {
                var validResult = item.Valid(requestDto);

                //一旦某个验证器验证不通过，就直接返回了
                if (!validResult.IsValid)
                {
                    return validResult;
                }
            }

            //全部验证通过
            return DtoValidatorResult.Success;
        }
    }

}
