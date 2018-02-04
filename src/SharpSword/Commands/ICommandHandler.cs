/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/17/2016 2:22:35 PM
 * ****************************************************************/

namespace SharpSword.Commands
{
    /// <summary>
    /// 所有实现命令需要继承此类，此实现类实现请注意：
    /// 所有命名方法参数或者属性，需要能够从字符串转型，业务基于控制台的命令只能输入字符串
    /// 继承IPerLifetimeDependency是为了让系统框架自动对所有命令行进行自动注册
    /// </summary>
    public interface ICommandHandler : IPerLifetimeDependency
    {
        /// <summary>
        /// 执行命令行(我们将命令的执行放在自身命令处理器上面)
        /// </summary>
        /// <param name="context"></param>
        void Execute(CommandContext context);
    }
}
