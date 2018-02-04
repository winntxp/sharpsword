/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/5/25 17:57:44
 * ****************************************************************/
using SharpSword.WebApi;

namespace SharpSword.SdkBuilder.CSharp
{
    /// <summary>
    /// 
    /// </summary>
    public static class ActionConfigCollectionExtensions
    {
        /// <summary>
        /// 禁用所有的系统框架接口
        /// </summary>
        /// <param name="actionConfigCollection">接口配置表对象</param>
        /// <returns></returns>
        public static IActionConfigCollection ObsoleteSdkBuilderActions(this IActionConfigCollection actionConfigCollection)
        {
            var actionNames = new[] { "Api.Doc", "Api.Doc.Builder", "API.TestTool", "API.BuildSdk" };
            foreach (var current in actionNames)
            {
                actionConfigCollection.Register(current, new ActionConfigItem() { Obsolete = true });
            }
            return actionConfigCollection;
        }
    }
}
