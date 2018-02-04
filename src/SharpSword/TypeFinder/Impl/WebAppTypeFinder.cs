/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using System.Collections.Generic;
using System.Reflection;

namespace SharpSword.TypeFinder.Impl
{
    /// <summary>
    /// WEB应用程序集类型查找器
    /// </summary>
    internal class WebAppTypeFinder : AppDomainTypeFinder
    {
        #region Fields

        private bool _ensureBinFolderAssembliesLoaded = true;
        private bool _binFolderAssembliesLoaded;

        #endregion

        #region Ctor

        /// <summary>
        /// 
        /// </summary>
        public WebAppTypeFinder()
        {
            this._ensureBinFolderAssembliesLoaded = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public bool EnsureBinFolderAssembliesLoaded
        {
            get { return _ensureBinFolderAssembliesLoaded; }
            set { _ensureBinFolderAssembliesLoaded = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a physical disk path of \Bin directory
        /// </summary>
        /// <returns>The physical path. E.g. "c:\inetpub\wwwroot\bin"</returns>
        public virtual string GetBinDirectory()
        {
            return HostHelper.GetBinDirectory();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IList<Assembly> GetAssemblies()
        {
            if (this.EnsureBinFolderAssembliesLoaded && !_binFolderAssembliesLoaded)
            {
                _binFolderAssembliesLoaded = true;
                string binPath = this.GetBinDirectory();
                LoadMatchingAssemblies(binPath);
            }
            return base.GetAssemblies();
        }

        #endregion
    }
}
