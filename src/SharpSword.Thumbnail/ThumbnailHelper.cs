/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/8/2016 12:25:58 PM
 * ****************************************************************/
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace SharpSword.Thumbnail
{
    /// <summary>
    /// 图片处理类
    /// </summary>
    public class ThumbnailHelper
    {
        /// <summary>    
        /// 缩略图  
        /// </summary>
        /// <param name="sourceImage">原图</param>
        /// <param name="saveFileName">原图压缩图片的保存文件名 </param>
        /// <param name="r">压缩图片大小，如果为0则不压缩</param>
        /// <param name="waterString">水印文字信息</param>
        /// <param name="font">水印文字字体,如果为null则系统设置默认值</param>
        /// <param name="brush">笔刷 如果为null则系统设置为默认值</param>
        /// <param name="position">水印位置</param>
        public static void Compress(Image sourceImage, string saveFileName, int r, string waterString, Font font, Brush brush, WaterImagePosition position)
        {

            int targetWidth = r;
            int targetHeight = r;

            //当大于0的时候才压缩
            if (r > 0)
            {
                if (sourceImage.Height <= r && sourceImage.Width <= r)
                {
                    targetHeight = sourceImage.Height;
                    targetWidth = sourceImage.Width;
                }
                else if (sourceImage.Height > sourceImage.Width)
                {
                    targetWidth = sourceImage.Width * r / sourceImage.Height;
                }
                else
                {
                    targetHeight = sourceImage.Height * r / sourceImage.Width;
                }
            }
            else
            {
                targetHeight = sourceImage.Height;
                targetWidth = sourceImage.Width;
            }


            //缩放图片   
            Image targetImage = Image.FromHbitmap(new Bitmap(targetWidth, targetHeight, PixelFormat.Format32bppRgb).GetHbitmap());
            Graphics g = Graphics.FromImage(targetImage);

            //设置缩略图的平滑度及质量
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            g.DrawImage(sourceImage, 0, 0, targetWidth, targetHeight);

            //水印文字
            if (waterString != null && waterString != string.Empty)
            {

                font = (font == null ? new Font("Arial Black", 28, FontStyle.Bold) : font);
                brush = (brush == null ? Brushes.Silver : brush);
                float WaterStringLength = (float)waterString.Length * font.Size;

                switch (position)
                {
                    case WaterImagePosition.Middle:
                        g.DrawString(waterString, font, brush, (targetWidth - WaterStringLength) / 2, targetHeight / 2 - 50);
                        break;
                    case WaterImagePosition.TopCenter:
                        g.DrawString(waterString, font, brush, (targetWidth - WaterStringLength) / 2, font.Size);
                        break;
                    case WaterImagePosition.BottomCenter:
                        g.DrawString(waterString, font, brush, (targetWidth - WaterStringLength) / 2, targetHeight - font.Size * 2);
                        break;
                    case WaterImagePosition.TopLeft:
                        g.DrawString(waterString, font, brush, 2, font.Size);
                        break;
                    case WaterImagePosition.BottomLeft:
                        g.DrawString(waterString, font, brush, 2, targetHeight - font.Size * 2);
                        break;
                    default:
                        g.DrawString(waterString, font, brush, (targetWidth - WaterStringLength) / 2, targetHeight / 2 - 50);
                        break;
                }
            }

            g.Dispose();
            //设置输出格式   
            EncoderParameters encParams = new EncoderParameters(1);
            encParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 65L);
            ImageCodecInfo codeInfo = null;
            ImageCodecInfo[] codeInfos = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo info in codeInfos)
            {
                if (info.MimeType.Equals("image/jpeg"))
                {
                    codeInfo = info;
                    break;
                }
            }
            if (codeInfo != null)
            {
                targetImage.Save(saveFileName, codeInfo, encParams);
            }
            targetImage.Dispose();
            sourceImage.Dispose();
        }

        /// <summary>
        /// 缩略图片，默认水印为居中
        /// </summary>
        /// <param name="sourceImage">原图</param>
        /// <param name="saveFileName">压缩图片保存路径</param>
        /// <param name="r">压缩大小</param>
        /// <param name="waterString">水印文字</param>
        /// <param name="font">水印文字字体,如果为null则系统设置默认值</param>
        /// <param name="brush">笔刷 如果为null则系统设置为默认值</param>
        public static void Compress(Image sourceImage, string saveFileName, int r, string waterString, Font font, Brush brush)
        {
            Compress(sourceImage, saveFileName, r, waterString, font, brush, WaterImagePosition.Middle);
        }

        /// <summary>
        /// 不缩略图片，不设置水印
        /// </summary>
        /// <param name="sourceImage">原图</param>
        /// <param name="saveFileName">图片保存路径</param>
        public static void Compress(Image sourceImage, string saveFileName)
        {
            Compress(sourceImage, saveFileName, 0, null, null, null, WaterImagePosition.Middle);
        }

        /// <summary>
        /// 缩略图片，不设置水印
        /// </summary>
        /// <param name="sourceImage">原图</param>
        /// <param name="saveFileName">压缩图片保存路径</param>
        /// <param name="r">压缩大小</param>
        public static void Compress(Image sourceImage, string saveFileName, int r)
        {
            Compress(sourceImage, saveFileName, r, null, null, null, WaterImagePosition.Middle);
        }

        /// <summary>
        /// 判断图像是否带索引格式
        /// </summary>
        /// <param name="imgPixelFormat"></param>
        /// <returns></returns>
        private static bool IsPixelFormatIndexed(PixelFormat imgPixelFormat)
        {
            PixelFormat[] indexedPixelFormats = { PixelFormat.Undefined, PixelFormat.DontCare, PixelFormat.Format16bppArgb1555, PixelFormat.Format1bppIndexed, PixelFormat.Format4bppIndexed, PixelFormat.Format8bppIndexed };
            foreach (PixelFormat pf in indexedPixelFormats)
            {
                if (pf.Equals(imgPixelFormat)) return true;
            }

            return false;
        }

        /// <summary>
        /// 设置图片水印
        /// </summary>
        /// <param name="sourceImage"></param>
        /// <param name="waterImagePath"></param>
        /// <param name="saveFileName"></param>
        public static void SetWaterImages(Image sourceImage, string waterImagePath, string saveFileName)
        {
            //带索引格式
            if (IsPixelFormatIndexed(sourceImage.PixelFormat))
            {
                Bitmap bmp = new Bitmap(sourceImage.Width, sourceImage.Height, PixelFormat.Format32bppArgb);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.DrawImage(sourceImage, 0, 0);
                    Image waterImage = Image.FromFile(waterImagePath);
                    g.DrawImage(waterImage, (sourceImage.Width - waterImage.Width) / 2, (sourceImage.Height - waterImage.Height) / 2, waterImage.Width, waterImage.Height);
                    bmp.Save(saveFileName, ImageFormat.Jpeg);
                    g.Dispose();
                    waterImage.Dispose();
                }
            }
            else
            {
                using (Graphics g = Graphics.FromImage(sourceImage))
                {
                    Image waterImage = Image.FromFile(waterImagePath);
                    g.DrawImage(waterImage, (sourceImage.Width - waterImage.Width) / 2, (sourceImage.Height - waterImage.Height) / 2, waterImage.Width, waterImage.Height);
                    sourceImage.Save(saveFileName, ImageFormat.Jpeg);
                    g.Dispose();
                    waterImage.Dispose();
                }
            }
        }

        /// <summary>    
        /// 生成缩略图   
        /// </summary>
        /// <param name="sourceImage">原图</param>
        /// <param name="thumbWidth">缩略图宽度</param>
        /// <param name="thumbHeight">缩略图高度 </param>
        /// <param name="saveFileName">缩略图的保存文件名  </param>
        public static void CreateThumb(Image sourceImage, int thumbWidth, int thumbHeight, string saveFileName)
        {
            int tw = thumbWidth;
            int th = thumbHeight;

            if (sourceImage.Height < th && sourceImage.Width < tw)
            {
                th = sourceImage.Height;
                tw = sourceImage.Width;
            }
            else if (sourceImage.Height > sourceImage.Width)
            {
                tw = sourceImage.Width * th / sourceImage.Height;
            }
            else
            {
                th = sourceImage.Height * tw / sourceImage.Width;
            }

            //将图片缩放到一个空白背景的图片上，生成大小一致的缩略图   
            Image thumbImage = Image.FromHbitmap(new Bitmap(thumbWidth, thumbHeight, PixelFormat.Format32bppRgb).GetHbitmap());
            Graphics g = Graphics.FromImage(thumbImage);
            SolidBrush brush = new SolidBrush(Color.White);

            //设置缩略图的平滑度及质量
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            //绘制背景
            g.FillRectangle(brush, 0, 0, thumbWidth, thumbHeight);


            int x = (thumbWidth - tw) / 2;
            int y = (thumbHeight - th) / 2;

            //绘图
            g.DrawImage(sourceImage, x, y, tw, th);
            g.Dispose();
            //保存缩略图
            thumbImage.Save(saveFileName, ImageFormat.Jpeg);
            thumbImage.Dispose();
            sourceImage.Dispose();
        }

        /// <summary>
        /// 创建缩略图
        /// </summary>
        /// <param name="sourceImage"></param>
        /// <param name="waterImagePath"></param>
        /// <param name="thumbWidth"></param>
        /// <param name="thumbHeight"></param>
        /// <param name="saveFileName"></param>
        public static void CreateThumb(Image sourceImage, string waterImagePath, int thumbWidth, int thumbHeight, string saveFileName)
        {
            int tw = thumbWidth;
            int th = thumbHeight;

            if (sourceImage.Height < th && sourceImage.Width < tw)
            {
                th = sourceImage.Height;
                tw = sourceImage.Width;
            }
            else if (sourceImage.Height > sourceImage.Width)
            {
                tw = sourceImage.Width * th / sourceImage.Height;
            }
            else
            {
                th = sourceImage.Height * tw / sourceImage.Width;
            }

            //将图片缩放到一个空白背景的图片上，生成大小一致的缩略图   
            Image thumbImage = Image.FromHbitmap(new Bitmap(thumbWidth, thumbHeight, PixelFormat.Format32bppRgb).GetHbitmap());
            Graphics g = Graphics.FromImage(thumbImage);
            SolidBrush brush = new SolidBrush(Color.White);

            //设置缩略图的平滑度及质量
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            //绘制背景
            g.FillRectangle(brush, 0, 0, thumbWidth, thumbHeight);

            //绘图
            g.DrawImage(sourceImage, (thumbWidth - tw) / 2, (thumbHeight - th) / 2, tw, th);

            //水印
            Image waterImage = Image.FromFile(waterImagePath);
            g.DrawImage(waterImage, (thumbWidth - waterImage.Width) / 2, (thumbHeight - waterImage.Height) / 2, waterImage.Width, waterImage.Height);
            waterImage.Dispose();
            g.Dispose();

            //保存缩略图
            thumbImage.Save(saveFileName, ImageFormat.Jpeg);
            thumbImage.Dispose();
            sourceImage.Dispose();
        }

        /// <summary>
        /// 按照指定画布大小裁剪图片（缩略图）
        /// </summary>
        /// <param name="sourceImage">源图像</param>
        /// <param name="width">画布宽度</param>
        /// <param name="height">画布高度</param>
        /// <param name="saveFileName">保存路劲</param>
        public static void CreateClipThumb(Image sourceImage, int width, int height, string saveFileName)
        {
            //原图宽高均小于模版，不作处理，直接保存
            if (sourceImage.Width <= width && sourceImage.Height <= height)
            {
                sourceImage.Save(saveFileName, ImageFormat.Jpeg);
            }
            else
            {
                //模版的宽高比例
                double templateRate = double.Parse(width.ToString()) / height;

                //原图片的宽高比例
                double initRate = double.Parse(sourceImage.Width.ToString()) / sourceImage.Height;

                //原图与模版比例相等，直接缩放
                if (templateRate == initRate)
                {

                    //按模版大小生成最终图片
                    Image templateImage = new Bitmap(width, height);
                    Graphics templateG = Graphics.FromImage(templateImage);

                    templateG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    templateG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    templateG.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    templateG.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    templateG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                    templateG.Clear(Color.White);
                    templateG.DrawImage(sourceImage, new Rectangle(0, 0, width, height), new Rectangle(0, 0, sourceImage.Width, sourceImage.Height), GraphicsUnit.Pixel);
                    templateImage.Save(saveFileName, ImageFormat.Jpeg);

                }
                //原图与模版比例不等，裁剪后缩放
                else
                {
                    //裁剪对象
                    Image pickedImage = null;
                    Graphics pickedG = null;

                    //定位
                    Rectangle fromR = new Rectangle(0, 0, 0, 0);//原图裁剪定位
                    Rectangle toR = new Rectangle(0, 0, 0, 0);//目标定位

                    //宽为标准进行裁剪
                    if (templateRate > initRate)
                    {
                        //裁剪对象实例化
                        pickedImage = new Bitmap(sourceImage.Width, int.Parse(Math.Floor(sourceImage.Width / templateRate).ToString()));
                        pickedG = Graphics.FromImage(pickedImage);

                        //裁剪源定位
                        fromR.X = 0;
                        fromR.Y = int.Parse(Math.Floor((sourceImage.Height - sourceImage.Width / templateRate) / 2).ToString());
                        fromR.Width = sourceImage.Width;
                        fromR.Height = int.Parse(Math.Floor(sourceImage.Width / templateRate).ToString());

                        //裁剪目标定位
                        toR.X = 0;
                        toR.Y = 0;
                        toR.Width = sourceImage.Width;
                        toR.Height = int.Parse(Math.Floor(sourceImage.Width / templateRate).ToString());
                    }
                    //高为标准进行裁剪
                    else
                    {
                        pickedImage = new Bitmap(int.Parse(Math.Floor(sourceImage.Height * templateRate).ToString()), sourceImage.Height);
                        pickedG = Graphics.FromImage(pickedImage);

                        fromR.X = int.Parse(Math.Floor((sourceImage.Width - sourceImage.Height * templateRate) / 2).ToString());
                        fromR.Y = 0;
                        fromR.Width = int.Parse(Math.Floor(sourceImage.Height * templateRate).ToString());
                        fromR.Height = sourceImage.Height;

                        toR.X = 0;
                        toR.Y = 0;
                        toR.Width = int.Parse(Math.Floor(sourceImage.Height * templateRate).ToString());
                        toR.Height = sourceImage.Height;
                    }

                    //设置质量
                    pickedG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    pickedG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    pickedG.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    pickedG.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                    pickedG.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    pickedG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                    //裁剪
                    pickedG.DrawImage(sourceImage, toR, fromR, GraphicsUnit.Pixel);

                    //按模版大小生成最终图片
                    Image templateImage = new Bitmap(width, height);
                    Graphics templateG = Graphics.FromImage(templateImage);
                    templateG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    templateG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    templateG.Clear(Color.White);
                    templateG.DrawImage(pickedImage, new Rectangle(0, 0, width, height), new Rectangle(0, 0, pickedImage.Width, pickedImage.Height), GraphicsUnit.Pixel);
                    templateImage.Save(saveFileName, ImageFormat.Jpeg);

                    //释放资源
                    templateG.Dispose();
                    templateImage.Dispose();

                    pickedG.Dispose();
                    pickedImage.Dispose();
                }
            }
        }

        /// <summary>
        /// 图片裁剪
        /// </summary>
        /// <param name="sourceImage"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="saveFileName"></param>
        public static void Clip(Image sourceImage, int x, int y, int width, int height, string saveFileName)
        {
            Bitmap b = new Bitmap(sourceImage);
            Bitmap b1 = b.Clone(new Rectangle(x, y, width, height), PixelFormat.Format24bppRgb);
            b1.Save(saveFileName);
            b.Dispose();
            b1.Dispose();
        }
    }
}
