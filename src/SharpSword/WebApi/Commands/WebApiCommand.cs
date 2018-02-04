/* *******************************************************
 * SharpSword zhangliang4629@163.com 11/17/2016 2:45:09 PM
 * ****************************************************************/
using SharpSword.Commands;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.WebApi.Commands
{
    /// <summary>
    /// 系统命令行帮助命令
    /// </summary>
    public class WebApiCommand : CommandHandlerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IActionSelector _actionSelector;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionSelector"></param>
        public WebApiCommand(IActionSelector actionSelector)
        {
            this._actionSelector = actionSelector;
        }

        /// <summary>
        /// 命令行显示出所有合法接口
        /// </summary>
        [CommandName("help apis")]
        [CommandHelp("help apis\r\n\t" + "显示所有系统API接口信息")]
        public void ShowActions()
        {
            var actions = this._actionSelector.GetActionDescriptors().OrderBy(o => o.ActionType.Assembly.GetName().Name).ThenBy(o => o.ActionName).ToList();
            this.Context.Output.WriteLine("ApiName---------Version---------Assembly----------");
            foreach (var item in actions)
            {
                this.Context.Output.WriteLine("{0} \t {1} \t {2}".With(item.ActionName, item.Version, item.ActionType.Assembly.GetName().Name));
            }
        }

        /// <summary>
        /// 命令行显示接口描述信息
        /// </summary>
        /// <param name="apiName"></param>
        [CommandName("help api")]
        [CommandHelp("help api [apiName] \r\n\t" + "显示接口描述信息")]
        public void ShowAction(string apiName)
        {
            //获取接口描述对象
            var actionDescriptors = this._actionSelector.GetActionDescriptors(apiName);

            foreach (var actionDescriptor in actionDescriptors)
            {
                this.Context.Output.WriteLine("Name：{0} , Version：{1}".With(actionDescriptor.ActionName, actionDescriptor.Version));

                //描述信息
                var attrs = actionDescriptor.GetAttributes();

                this.Context.Output.WriteLine("-------------------------------------------");

                foreach (var attr in attrs)
                {
                    //缓存，特殊处理
                    if (attr.Key == "Cache" && !attr.Value.IsNull())
                    {
                        this.Context.Output.WriteLine("\t{0}={{{1}}}".With(attr.Key, attr.Value.GetAttributes().Where(x => x.Key != "TypeId").Select(x => "{0}={1}".With(x.Key, x.Value)).JoinToString(",")));
                        continue;
                    }

                    //集合类型
                    if (!attr.Value.IsNull())
                    {
                        var type = attr.Value.GetType();
                        if (type.IsGenericType && type.GenericTypeArguments.Length == 1 && typeof(IEnumerable).IsAssignableFrom(type.GetGenericTypeDefinition()))
                        {
                            var actionFilters = attr.Value as IEnumerable<object>;
                            this.Context.Output.WriteLine("\t{0}=[{1}]".With(attr.Key, actionFilters.Select(x => x.ToString()).JoinToString(",")));
                            continue;
                        }
                    }

                    //其他类型的
                    this.Context.Output.WriteLine("\t{0}={1}".With(attr.Key, attr.Value));
                }
            }
        }
    }
}
