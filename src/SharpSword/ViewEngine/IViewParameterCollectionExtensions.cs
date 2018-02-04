/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/17/2016 12:50:53 PM
 * ****************************************************************/

namespace SharpSword.ViewEngine
{
    /// <summary>
    /// 
    /// </summary>
    public static class IViewParameterCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewParameterCollection"></param>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        /// <returns></returns>
        public static IViewParameterCollection Append(this IViewParameterCollection viewParameterCollection, string paramName, object paramValue)
        {
            viewParameterCollection.Add(paramName, paramValue);
            return viewParameterCollection;
        }

    }
}
