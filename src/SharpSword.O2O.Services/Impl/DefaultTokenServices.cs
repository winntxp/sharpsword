/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 5:11:14 PM
 * ****************************************************************/

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultTokenServices : ITokenServices
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IGuidGenerator _guidGenerator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guidGenerator"></param>
        public DefaultTokenServices(IGuidGenerator guidGenerator)
        {
            this._guidGenerator = guidGenerator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string Create()
        {
            return this._guidGenerator.Create().ToString("N").ToUpper();
        }
    }
}
