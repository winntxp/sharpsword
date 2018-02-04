/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/12 12:40:48
 * ****************************************************************/
using System;
using System.Linq;

namespace SharpSword.WebApi
{
    /// <summary>
    /// SDK代码输出器创建工厂
    /// </summary>
    public class DefaultCodeGeneratorFactory : ISdkCodeGeneratorFactory
    {
        /// <summary>
        /// 
        /// </summary>
        private ITypeFinder _typeFinder;

        /// <summary>
        ///  SDK代码输出器创建工厂
        /// </summary>
        /// <param name="typeFinder">类型查找器</param>
        public DefaultCodeGeneratorFactory(ITypeFinder typeFinder)
        {
            this._typeFinder = typeFinder;
        }

        /// <summary>
        /// 创建SDK代码生成器实例
        /// </summary>
        /// <param name="language">语言(CSharp,JAVA,PHP,Android)</param>
        /// <returns>SDK输出器实例，如果未找到则返回null，注意调用的时候判断null情况</returns>
        public SdkCodeGeneratorBase Create(string language)
        {
            //搜索所有可用的SDK代码生成器
            var codeGenerators = this._typeFinder.FindClassesOfType<CodeGeneratorBase>().ToList();

            //未找到任何注册的SDK代码生成器
            if (codeGenerators.IsNull() || codeGenerators.IsEmpty())
            {
                return null;
            }

            //未找到注册类型
            var codeGeneratorType = codeGenerators.FirstOrDefault(o => o.Name.StartsWith(language, StringComparison.OrdinalIgnoreCase));
            if (codeGeneratorType.IsNull())
            {
                return null;
            }

            //创建出SDK代码生成器
            return (SdkCodeGeneratorBase)ServicesContainer.Current.Resolve(codeGeneratorType);
        }
    }
}
