/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/23/2016 5:41:39 PM
 * ****************************************************************/
using System;
using System.Collections.Generic;

namespace SharpSword
{
    /// <summary>
    /// 获取文件对应的httpcontent类型
    /// </summary>
    public class HttpContentType
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly IDictionary<string, string> HttpContentTypeMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 
        /// </summary>
        static HttpContentType()
        {
            HttpContentTypeMap.Add(".js", "application/javascript");
            HttpContentTypeMap.Add(".css", "text/css");
            HttpContentTypeMap.Add(".html", "text/html");
            HttpContentTypeMap.Add(".gif", "image/gif");
            HttpContentTypeMap.Add(".jpg", "image/jpeg");
            HttpContentTypeMap.Add(".png", "image/png");
            HttpContentTypeMap.Add(".ico", "image/x-icon");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileExtension">根据扩展名称获取文件的httpContentType类型</param>
        /// <returns></returns>
        public static string GetContentType(string fileExtension)
        {
            if (fileExtension.IsNullOrEmpty())
            {
                return null;
            }

            if (!fileExtension.StartsWith("."))
            {
                fileExtension = ".{0}".With(fileExtension);
            }

            if (HttpContentTypeMap.Keys.Contains(fileExtension))
            {
                return HttpContentTypeMap[fileExtension];
            }

            return null;
        }
    }
}
