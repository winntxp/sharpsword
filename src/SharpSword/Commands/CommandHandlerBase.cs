/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/17/2016 2:38:46 PM
 * ****************************************************************/
using SharpSword.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharpSword.Commands
{
    /// <summary>
    /// 所有扩展的命令行类需要继承此抽象基类
    /// </summary>
    public abstract class CommandHandlerBase : ICommandHandler
    {
        /// <summary>
        /// 
        /// </summary>
        protected CommandHandlerBase()
        {
            this.L = NullLocalizer.Instance;
        }

        /// <summary>
        /// 本地化器
        /// </summary>
        public Localizer L { get; set; }

        /// <summary>
        /// 命令行执行上下文
        /// </summary>
        public CommandContext Context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Execute(CommandContext context)
        {
            this.SetSwitchValues(context);
            this.Invoke(context);
        }

        /// <summary>
        /// 命令行参数赋值
        /// </summary>
        /// <param name="context"></param>
        private void SetSwitchValues(CommandContext context)
        {
            if (!context.Switches.IsNull() && context.Switches.Any())
            {
                foreach (var commandSwitch in context.Switches)
                {
                    this.SetSwitchValue(commandSwitch);
                }
            }
        }

        /// <summary>
        /// 命令行参数赋值
        /// </summary>
        /// <param name="commandSwitch"></param>
        private void SetSwitchValue(KeyValuePair<string, string> commandSwitch)
        {
            PropertyInfo propertyInfo = this.GetType().GetProperty(commandSwitch.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            if (propertyInfo.IsNull())
            {
                throw new InvalidOperationException(L("可选参数 \"{0}\" 未定义", commandSwitch.Key));
            }
            if (propertyInfo.GetCustomAttributes(typeof(CommandSwitchAttribute), false).Length == 0)
            {
                throw new InvalidOperationException(L("属性 \"{0}\" 存在，但是为定义特性： \"{1}\"", commandSwitch.Key, typeof(CommandSwitchAttribute).Name));
            }

            // Set the value
            try
            {
                object value = Convert.ChangeType(commandSwitch.Value, propertyInfo.PropertyType);
                propertyInfo.SetValue(this, value, null/*index*/);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="context"></param>
        private void Invoke(CommandContext context)
        {
            this.CheckMethodForSwitches(context.CommandDescriptor.MethodInfo, context.Switches);

            var arguments = (context.Arguments ?? Enumerable.Empty<string>()).ToArray();
            object[] invokeParameters = GetInvokeParametersForMethod(context.CommandDescriptor.MethodInfo, arguments);
            if (invokeParameters.IsNull())
            {
                throw new InvalidOperationException(L("命令行参数 \"{0}\" 与定义不匹配", string.Join(" ", arguments)).ToString());
            }

            this.Context = context;
            var result = context.CommandDescriptor.MethodInfo.Invoke(this, invokeParameters);
            if (result is string)
            {
                context.Output.Write(result);
            }
        }

        /// <summary>
        /// 根据参数获取到方法需要的入参值
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        private static object[] GetInvokeParametersForMethod(MethodInfo methodInfo, IList<string> arguments)
        {
            var invokeParameters = new List<object>();
            var args = new List<string>(arguments);
            var methodParameters = methodInfo.GetParameters();
            bool methodHasParams = false;

            if (0 == methodParameters.Length)
            {
                if (0 == args.Count)
                    return invokeParameters.ToArray();
                return null;
            }

            if (methodParameters[methodParameters.Length - 1].ParameterType.IsAssignableFrom(typeof(string[])))
            {
                methodHasParams = true;
            }

            if (!methodHasParams && args.Count != methodParameters.Length) return null;
            if (methodHasParams && (methodParameters.Length - args.Count >= 2)) return null;

            for (int i = 0; i < args.Count; i++)
            {
                if (methodParameters[i].ParameterType.IsAssignableFrom(typeof(string[])))
                {
                    invokeParameters.Add(args.GetRange(i, args.Count - i).ToArray());
                    break;
                }
                invokeParameters.Add(Convert.ChangeType(arguments[i], methodParameters[i].ParameterType));
            }

            if (methodHasParams && (methodParameters.Length - args.Count == 1)) invokeParameters.Add(new string[] { });

            return invokeParameters.ToArray();
        }

        /// <summary>
        /// 根据属性值，校验属性是否合法
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="switches"></param>
        private void CheckMethodForSwitches(MethodInfo methodInfo, IDictionary<string, string> switches)
        {
            if (switches.IsNull() || 0 == switches.Count)
            {
                return;
            }

            var supportedSwitches = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (CommandSwitchesAttribute switchesAttribute in methodInfo.GetCustomAttributes(typeof(CommandSwitchesAttribute), false))
            {
                supportedSwitches.UnionWith(switchesAttribute.Switches);
            }

            foreach (var commandSwitch in switches.Keys)
            {
                if (!supportedSwitches.Contains(commandSwitch))
                {
                    throw new InvalidOperationException(L("方法 \"{0}\" 不支持可选参数： \"{1}\".", methodInfo.Name, commandSwitch).ToString());
                }
            }
        }
    }
}
