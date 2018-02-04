using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System;
using System.ComponentModel;

namespace SharpSword
{
    /// <summary>
    /// Container class for the ASP.NET application startup method.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class PreApplicationStartCode
    {
        /// <summary>
        /// 
        /// </summary>
        private static bool _startWasCalled;

        /// <summary>
        /// Performs ASP.NET application startup logic early in the pipeline.
        /// </summary>
        public static void Start()
        {
            if (PreApplicationStartCode._startWasCalled)
            {
                return;
            }
            //PreApplicationStartCode._startWasCalled = true;
            //DynamicModuleUtility.RegisterModule(typeof(RequestLifetimeHttpModule));
        }
    }
}

