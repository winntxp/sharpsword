/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SharpSword.OAuth
{
    /// <summary>
    /// 
    /// </summary>
    internal class HttpHelp
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> GetStrAsync(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    return await httpClient.GetStringAsync(url).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetStr(string url)
        {
            try
            {
                return GetStrAsync(url).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
