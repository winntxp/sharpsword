/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/8/2016 12:25:58 PM
 * ****************************************************************/
using System.Collections;

namespace SharpSword.BarCode
{
    /// <summary>
    /// 
    /// </summary>
    public class Code39 : DrawImageBord
    {
        /// <summary>
        /// 
        /// </summary>
        private Hashtable hash = new Hashtable();
        /// <summary>
        /// 
        /// </summary>
        protected override string BordRuleName
        {
            get { return "CODE39"; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public Code39(string s)
            : base(s)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        protected override Hashtable Roles
        {
            get
            {
                if (hash.Count > 0) return hash;
                hash.Add("0", "001100100");
                hash.Add("1", "100010100");
                hash.Add("2", "010010100");
                hash.Add("3", "110000100");
                hash.Add("4", "001010100");
                hash.Add("5", "101000100");
                hash.Add("6", "011000100");
                hash.Add("7", "000110100");

                hash.Add("8", "100100100");
                hash.Add("9", "010100100");
                hash.Add("A", "100010010");
                hash.Add("B", "010010010");
                hash.Add("C", "110000010");
                hash.Add("D", "001010010");
                hash.Add("E", "101000010");

                hash.Add("F", "011000010");
                hash.Add("G", "000110010");
                hash.Add("H", "100100010");
                hash.Add("I", "010100010");
                hash.Add("J", "001100010");
                hash.Add("K", "100010001");
                hash.Add("L", "010010001");

                hash.Add("M", "110000001");
                hash.Add("N", "001010001");
                hash.Add("O", "101000001");
                hash.Add("P", "011000001");
                hash.Add("Q", "000110001");
                hash.Add("R", "100100001");
                hash.Add("S", "010100001");


                hash.Add("T", "001100001");
                hash.Add("U", "100011000");
                hash.Add("V", "010011000");
                hash.Add("W", "110001000");
                hash.Add("X", "001011000");
                hash.Add("Y", "101001000");
                hash.Add("Z", "011001000");


                hash.Add("-", "000111000");
                hash.Add("%", "100101000");
                hash.Add("$", "010101000");
                hash.Add("*", "001101000");

                return hash;
            }
        }
    }
}
