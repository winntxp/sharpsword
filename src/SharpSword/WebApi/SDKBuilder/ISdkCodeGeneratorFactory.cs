/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/12 12:42:51
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISdkCodeGeneratorFactory
    {
        /// <summary>
        /// 创建客户端SDK输出器工厂
        /// </summary>
        /// <param name="language">语言(CSharp,JAVA,PHP,Android)</param>
        /// <returns></returns>
        SdkCodeGeneratorBase Create(string language);
    }
}
