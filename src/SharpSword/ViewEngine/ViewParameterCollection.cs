/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/14 15:25:37
 * ****************************************************************/
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.ViewEngine
{
    /// <summary>
    /// 视图暴露出来的属性集合（外部注入值）
    /// </summary>
    public class ViewParameterCollection : List<ViewParameter>, IViewParameterCollection
    {
        /// <summary>
        /// 
        /// </summary>
        public ViewParameterCollection() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public ViewParameterCollection(IEnumerable<ViewParameter> collection) : base(collection) { }

        /// <summary>
        /// 根据属性名称获取视图属性对象
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <returns></returns>
        public ViewParameter this[string name]
        {
            get
            {
                return this.FirstOrDefault(viewParameter => viewParameter.Name == name);
            }
        }

        /// <summary>
        /// 添加一个视图属性对象
        /// </summary>
        /// <param name="paramName">属性类型</param>
        /// <returns></returns>
        public void Add(string paramName)
        {
            this.Add(paramName, null);
        }

        /// <summary>
        /// 添加一个视图属性对象
        /// </summary>
        /// <param name="paramName">属性名称</param>
        /// <param name="paramValue">属性值</param>
        /// <returns></returns>
        public void Add(string paramName, object paramValue)
        {
            if (!this[paramName].IsNull())
            {
                throw new System.Exception("参数:{0}已经存在".With(paramName));
            }
            this.Add(new ViewParameter(paramName, paramValue));
        }
    }
}
