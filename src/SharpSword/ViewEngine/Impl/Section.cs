/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/12 10:19:07
 * ****************************************************************/

namespace SharpSword.ViewEngine.Impl
{
    /// <summary>
    /// 源代码代码块对象；比如代码块，文档常量，视图参数接口对象，命名空间
    /// </summary>
    internal class Section
    {
        /// <summary>
        /// 代码块索引
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 代码块源码
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 代码块类型
        /// </summary>
        public SectionType Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DirectiveValues Values { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="text"></param>
        /// <param name="type"></param>
        public Section(int index, string text, SectionType type)
            : this(index, text, type, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="text"></param>
        /// <param name="type"></param>
        /// <param name="values"></param>
        public Section(int index, string text, SectionType type, DirectiveValues values)
        {
            Index = index;
            Text = text;
            Type = type;
            Values = values;
        }
    }
}
