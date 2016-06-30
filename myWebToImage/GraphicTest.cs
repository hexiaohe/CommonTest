using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace myWebToImage
{
    public class GraphicTest
    {
        public GraphicTest()
        {
        }

        public static Bitmap GetGraphic(int width, int height)
        {
            var file = new FileStream(@"D:\templateimg.jpg", FileMode.Open, FileAccess.Read);
            var br = new BinaryReader(file);
            byte[] byteImg = br.ReadBytes((int)file.Length);
            var bitmap = new Bitmap(file);

            var gfx = Graphics.FromImage(bitmap);
            //graphics.FillEllipse(brush, 0, 0, width, height);
            // 设置画布的描绘质量
            gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gfx.CompositingQuality = CompositingQuality.HighQuality;

            //var webClient = new WebClient();
            //byte[] headBackImgBytes = webClient.DownloadData("https://ss0.bdstatic.com/5aV1bjqh_Q23odCf/static/superman/img/logo_top_ca79a146.png");
            //var headBackImgStream = new MemoryStream(headBackImgBytes);
            //var headBackImgBitmap = new Bitmap(headBackImgStream);

            //byte[] headImgBytes = webClient.DownloadData("https://ss0.bdstatic.com/5aV1bjqh_Q23odCf/static/superman/img/logo_top_ca79a146.png");
            //var headImgStream = new MemoryStream(headImgBytes);
            //var headImgBitmap = new Bitmap(headImgStream);

            //string str = "邀请您一起关注校宝秀"; //写什么字？
            //Font font = new Font("宋体",30f); //字是什么样子的？
            //Brush brush = Brushes.Wheat; //用红色涂上我的字吧；
            //PointF point = new PointF(10f,10f); //从什么地方开始写字捏？
            ////横着写还是竖着写呢？
            //System.Drawing.StringFormat sf = new StringFormat();
            ////还是竖着写吧
            //sf.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            ////开始写咯
            ////g.DrawString(str,font,brush,point,sf);
            /// 
            WebClient webClient = new WebClient();
            #region 下载头像
            byte[] bytesHeardImg = webClient.DownloadData("http://cdn.schoolpal.cn/shiningstar/Weixin/20160427211937-67257");
            MemoryStream msHeardImg = new MemoryStream(bytesHeardImg);
            var heardImg = new Bitmap(msHeardImg);
            //var heardImg = heardsourceImg.Clone(new Rectangle(0, 0, heardsourceImg.Width, heardsourceImg.Width), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            #endregion

            #region 下载二维码
            byte[] bytes = webClient.DownloadData("http://greedyint-dev.oss-cn-hangzhou.aliyuncs.com/xbshow/QrCode/20160628201338-0d5d9.jpg");
            MemoryStream msQrcode = new MemoryStream(bytes);
            var qrCodeImg = new Bitmap(msQrcode);
            #endregion

            var nickName = "15611222211";

            GraphicsPath iconPath = null;
            GraphicsPath iconClipPath = null;
            GraphicsPath borderPath = null;
            float iconDestWidth = 0, iconDestHeight = 0, iconX = 0, iconY = 0;
            int iconSizePercent = 20;

            #region heardImg 按比例缩放
            int destHeight = 102;//压缩高度
            int destWidth = 102;//压缩宽度
            int sW = 0, sH = 0;
            int sWidth = heardImg.Width;
            int sHeight = heardImg.Height;
            if (sHeight > destHeight || sWidth > destWidth)
            {
                if ((sWidth * destHeight) > (sHeight * destWidth))
                {
                    sW = destWidth;
                    sH = (destWidth * sHeight) / sWidth;
                }
                else
                {
                    sH = destHeight;
                    sW = (sWidth * destHeight) / sHeight;
                }
            }
            else
            {
                sW = sWidth;
                sH = sHeight;
            }
            #endregion

            #region qrCodeImg 按比例缩放
            int qDestHeight = 380;//压缩高度
            int qDestWidth = 380;//压缩宽度
            int qSW = 0, qSH = 0;
            int qSWidth = qrCodeImg.Width;
            int qSHeight = qrCodeImg.Height;
            if (qSHeight > qDestHeight || qSWidth > qDestWidth)
            {
                if ((qSWidth * qDestHeight) > (qSHeight * qDestWidth))
                {
                    qSW = qDestWidth;
                    qSH = (qDestWidth * qSHeight) / qSWidth;
                }
                else
                {
                    qSH = qDestHeight;
                    qSW = (qSWidth * qDestHeight) / qSHeight;
                }
            }
            else
            {
                qSW = qSWidth;
                qSH = qSHeight;
            }
            #endregion

            #region
            //RectangleF r = new RectangleF(iconX - iconBorderWidth, iconY - iconBorderWidth, iconDestWidth + iconBorderWidth * 2, iconDestHeight + iconBorderWidth * 2);
            //RectangleF r = new RectangleF(270, 30, 102, 102);
            //gfx.FillEllipse(Brushes.White, r);//椭圆形白底
            
            //gfx.DrawPath(new Pen(Brushes.Black, 2), borderPath);

            //RectangleF iconDestRect = new RectangleF(270, 10, heardImg.Width, heardImg.Height);

            
            Font myFont = new Font("黑体", 24, FontStyle.Bold);
            Brush myBrush = new SolidBrush(Color.White);//画刷

            SizeF sizeF = gfx.MeasureString(nickName.Trim(), myFont);
            var fontX = (bitmap.Width - sizeF.Width) / 2;

            gfx.DrawString(nickName, myFont, myBrush, fontX, 164);

            //Bitmap bmp = qrCodeImg.Clone(new Rectangle(0, 0, qrCodeImg.Width, qrCodeImg.Height), PixelFormat.Format24bppRgb);
            var qrCodeImgX = (bitmap.Width - qSW) / 2;
            qrCodeImg.MakeTransparent(Color.White);
            gfx.DrawImage(qrCodeImg, new Rectangle(qrCodeImgX, 340, qSW, qSH), 0, 0, qrCodeImg.Width, qrCodeImg.Height, GraphicsUnit.Pixel);

            //画为圆
            var heardImagX = (bitmap.Width - sW) / 2;
            RectangleF ellipseDest = new RectangleF(heardImagX, 30, 102, 102);
            iconClipPath = new GraphicsPath();
            iconClipPath.AddEllipse(ellipseDest);
            gfx.SetClip(iconClipPath);
            gfx.DrawImage(heardImg, new Rectangle(heardImagX, 30, sW, sH), 0, 0, heardImg.Width, heardImg.Height, GraphicsUnit.Pixel);
            #endregion

            gfx.Save();
            return bitmap;
        }

        /// <summary>
        /// 创建圆角矩形
        /// </summary>
        /// <param name="rect">区域</param>
        /// <param name="cornerRadius">圆角角度</param>
        /// <returns></returns>
        private static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            //下午重新整理下，圆角矩形
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }

        /// <summary>
        /// 创建缩略图
        /// </summary>
        /// <param name="b">Bitmap</param>
        /// <param name="destWidth">压缩宽度</param>
        /// <param name="destHeight">压缩高度</param>
        /// <returns></returns>
        public static Bitmap GetThumbnail(Bitmap bitmap, int destWidth, int destHeight)
        {
            Image imgSource = bitmap;
            ImageFormat thisFormat = imgSource.RawFormat;
            int sW = 0, sH = 0;
            // 按比例缩放 
            int sWidth = imgSource.Width;
            int sHeight = imgSource.Height;
            if (sHeight > destHeight || sWidth > destWidth)
            {
                if ((sWidth * destHeight) > (sHeight * destWidth))
                {
                    sW = destWidth;
                    sH = (destWidth * sHeight) / sWidth;
                }
                else
                {
                    sH = destHeight;
                    sW = (sWidth * destHeight) / sHeight;
                }
            }
            else
            {
                sW = sWidth;
                sH = sHeight;
            }
            Bitmap outBmp = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.Transparent);
            // 设置画布的描绘质量 
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(imgSource, new Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
            g.Dispose();
            // 以下代码为保存图片时，设置压缩质量 
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;
            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;
            imgSource.Dispose();
            return outBmp;
        }
    }
}
