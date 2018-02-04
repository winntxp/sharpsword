/* ****************************************************************
 * SharpSword zhangliang4629@sharpsword.com.cn 11/22/2016 1:54:01 PM
 * ****************************************************************/
using System;
using System.Web;

namespace SharpSword.Localization
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class LocalizedString : MarshalByRefObject, IHtmlString
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly string _localized;
        private readonly object[] _args;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localized">已经格式化的数据</param>
        public LocalizedString(string localized)
        {
            this._localized = localized;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localized">已经格式化的数据</param>
        /// <param name="args">参数</param>
        public LocalizedString(string localized, object[] args)
        {
            _localized = localized;
            _args = args;
        }

        /// <summary>
        /// 
        /// </summary>
        public object[] Args
        {
            get { return _args; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            get { return _localized; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _localized;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string IHtmlString.ToHtmlString()
        {
            return _localized;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localizedText"></param>
        public static implicit operator LocalizedString(string localizedText)
        {
            return new LocalizedString(localizedText);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localizedString"></param>
        public static implicit operator string (LocalizedString localizedString)
        {
            return localizedString.ToString();
        }
    }
}
