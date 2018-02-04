/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/17/2016 2:45:09 PM
 * ****************************************************************/
using SharpSword.Commands;

namespace SharpSword.Auditing.Commands
{
    /// <summary>
    /// 系统命令行帮助命令
    /// </summary>
    public class AuditingCommand : CommandHandlerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ICommandManager _commandManager;
        private readonly IIocResolver _iocResolver;
        private readonly ITypeFinder _typeFinder;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandManager">命令管理器</param>
        /// <param name="iocResolver">IOC反转服务</param>
        /// <param name="typeFinder">类型查找器</param>
        public AuditingCommand(ICommandManager commandManager, IIocResolver iocResolver, ITypeFinder typeFinder)
        {
            this._commandManager = commandManager;
            this._iocResolver = iocResolver;
            this._typeFinder = typeFinder;
        }
    }
}
