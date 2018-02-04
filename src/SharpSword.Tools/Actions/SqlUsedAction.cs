/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/20/2016 11:06:31 AM
 * ****************************************************************/
using SharpSword.Data;
using SharpSword.WebApi;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharpSword.Tools.Actions
{
    /// <summary>
    /// 获取所有定义的事件
    /// </summary>
    [ActionName("Api.SqlUsed"), DisablePackageSdk, AllowAnonymous, EnableRecordApiLog(true), EnableAjaxRequest]
    [DisableDataSignatureTransmission, ResultCache(15)]
    //[View("SharpSword.WebApi.Views.Api.SqlUsed.aspx")]
    public class SqlUsedAction : ActionBase<NullRequestDto, IEnumerable<MethodInfo>>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ITypeFinder _typeFinder;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeFinder"></param>
        public SqlUsedAction(ITypeFinder typeFinder)
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
            var assemblies = this._typeFinder.GetAssemblies();

            foreach (var assemblie in assemblies)
            {
                //程序集所有类型
                var types = assemblie.GetTypes().Where(t => !t.IsProxyType() && t.IsClass).ToList();
                foreach (var type in types)
                {
                    type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                                       .Where(m => !m.IsSpecialName
                                                       &&
                                                       m.DeclaringType != null
                                                       &&
                                                       m.IsDefined(typeof(SqlUsedAttribute))).ToList().ForEach(method =>
                                                       {
                                                           responseDtos.Add(method);
                                                       });
                }
            }
            return this.SuccessActionResult(responseDtos.OrderBy(o => o.DeclaringType.Name).ThenBy(o => o.Name));
        }
    }
}
