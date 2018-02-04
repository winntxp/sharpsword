/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/20/2016 11:06:31 AM
 * ****************************************************************/
using SharpSword.Domain.Services;
using SharpSword.Domain.Uow;
using SharpSword.WebApi;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharpSword.Tools.Actions
{
    /// <summary>
    /// 获取所有定义的事件
    /// </summary>
    [ActionName("Api.TransUsed"), DisablePackageSdk, AllowAnonymous, EnableRecordApiLog(true), EnableAjaxRequest]
    [DisableDataSignatureTransmission, ResultCache(15)]
    //[View("SharpSword.WebApi.Views.Api.TransUsed.aspx")]
    public class TransUsedAction : ActionBase<NullRequestDto, IEnumerable<MethodInfo>>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ITypeFinder _typeFinder;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeFinder"></param>
        public TransUsedAction(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override ActionResult<IEnumerable<MethodInfo>> Execute()
        {
            //获取自定义的SQL方法
            IList<MethodInfo> responseDtos = new List<MethodInfo>();

            //获取所有程序集
            var types = this._typeFinder.FindClassesOfType<SharpSwordServicesBase>().Where(t => !t.IsProxyType() && t.IsClass).ToList(); ;

            foreach (var type in types)
            {
                type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                                   .Where(m => !m.IsSpecialName
                                                   &&
                                                   m.DeclaringType != null
                                                   &&
                                                    m.HasUnitOfWorkAttribute()
                                                   &&
                                                   m.GetSingleAttributeOrNull<UnitOfWorkAttribute>().IsTransactional.Value).ToList().ForEach(method =>
                                                   {
                                                       responseDtos.Add(method);
                                                   });
            }

            return this.SuccessActionResult(responseDtos.OrderBy(o => o.DeclaringType.Name).ThenBy(o => o.Name));
        }
    }
}
