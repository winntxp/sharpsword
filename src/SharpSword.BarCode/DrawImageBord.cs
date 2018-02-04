/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/8/2016 12:25:58 PM
 * ****************************************************************/
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SharpSword.BarCode
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DrawImageBord {
        /// <summary>
        /// 
        /// </summary>
        protected virtual string BordRuleName {
            get { return string.Empty; }
        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual Hashtable Roles {
            get { return new Hashtable(); }
        }
        /// <summary>
        /// 
        /// </summary>
        string drawString;
        int width = 300;     //画布的宽度(可计算) 
        int height = 36;     //1CM 
        int unitWidth = 1;   //窄条的宽度像素数
        int rate = 3; //条码宽条与窄条宽度之比
        int currentLocation = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public DrawImageBord(string s) {
            drawString = s;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        public virtual void Draw(Stream target) {
            //画布宽度
            width = (drawString.Length * (3 * rate + 7) * unitWidth);
            Bitmap bm = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bm);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            //画布和边的设定   
            g.Clear(Color.White);
            g.DrawRectangle(Pens.White, 0, 0, width, height);
            for (int i = 0; i < drawString.Length; i++) {
                this.DrawString(drawString[i].ToString(), g);
            }
            bm.Save(target, ImageFormat.Jpeg);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="g"></param>
        protected virtual void DrawString(string s, Graphics g) {
            Hashtable hash = this.Roles;
            object o = hash[s];
            if (o == null) return;
            char[] chars = o.ToString().ToCharArray();
            if (chars.Length > 9) return;
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            SolidBrush witeBrush = new SolidBrush(Color.White);

            for (int i = 0; i < 5; i++) {
                //画第一个   0   黑条 
                if (chars[i] == '0') {
                    Rectangle re1 = new Rectangle(currentLocation, 0, unitWidth, height);
                    g.FillRectangle(blackBrush, re1);
                    currentLocation += unitWidth;
                }
                else {
                    Rectangle re1 = new Rectangle(currentLocation, 0, rate * unitWidth, height);
                    g.FillRectangle(blackBrush, re1);
                    currentLocation += 3 * unitWidth;
                }
                //画第6个     5   白条 
                if ((i + 5) < 9) {
                    if (chars[i + 5] == '0') {
                        Rectangle re1 = new Rectangle(currentLocation, 0, unitWidth, height);
                        g.FillRectangle(witeBrush, re1);
                        currentLocation += unitWidth;
                    }
                    else {
                        Rectangle re1 = new Rectangle(currentLocation, 0, rate * unitWidth, height);
                        g.FillRectangle(witeBrush, re1);
                        currentLocation += 3 * unitWidth;
                    }
                }
            }
            Rectangle re2 = new Rectangle(currentLocation, 0, unitWidth, height);
            g.FillRectangle(witeBrush, re2);
            currentLocation += unitWidth;
        }
    }
}
