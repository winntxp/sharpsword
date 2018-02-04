/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/9/2017 2:36:11 PM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.SDK
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IHttpWebUtils
    {
        /// <summary>
        /// 
        /// </summary>
        int Timeout { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        HttpRespBody DoGet(string url, IDictionary<string, string> parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        HttpRespBody DoPost(string url, IDictionary<string, string> parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="textParams"></param>
        /// <param name="fileParams"></param>
        /// <returns></returns>
        HttpRespBody DoPost(string url, IDictionary<string, string> textParams, IDictionary<string, FileItem> fileParams);
    }
}