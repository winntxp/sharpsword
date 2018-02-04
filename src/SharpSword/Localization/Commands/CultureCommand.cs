/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/22/2016 3:12:50 PM
 * ****************************************************************/
using SharpSword.Commands;
using System.Threading;

namespace SharpSword.Localization.Commands
{
    /// <summary>
    /// 设置区域命令行
    /// </summary>
    public class CultureCommand : CommandHandlerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly LocalizationConfiguration _localizationConfig;
        private static readonly ReaderWriterLockSlim Locker = new ReaderWriterLockSlim();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localizationConfig"></param>
        public CultureCommand(LocalizationConfiguration localizationConfig)
        {
            this._localizationConfig = localizationConfig;
        }

        /// <summary>
        /// 
        /// </summary>
        [CommandHelp("cultures get culture \r\n\t 获取系统设置的区域信息")]
        [CommandName("cultures get culture")]
        public void GetCulture()
        {
            Context.Output.WriteLine(this.L("系统设置的区域为： {0}", this._localizationConfig.CultureName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cultureName"></param>
        [CommandHelp("cultures set culture [culture-name] \r\n\t 设置系统设置的区域信息")]
        [CommandName("cultures set culture")]
        public void SetCulture(string cultureName)
        {
            using (new WriteLockDisposable(Locker))
            {
                this._localizationConfig.CultureName = cultureName;
            }

            Context.Output.WriteLine(this.L("设置区域 {0} 成功", cultureName));
        }
    }
}
