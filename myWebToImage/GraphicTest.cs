using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace myWebToImage
{
    public class GraphicTest
    {
        public static Bitmap GetGraphic(int width, int height)
        {
            //HttpClient h = new HttpClient();
            
            var webClient = new WebClient();
            byte[] headBackImgBytes = webClient.DownloadData("https://ss0.bdstatic.com/5aV1bjqh_Q23odCf/static/superman/img/logo_top_ca79a146.png");
            var headBackImgStream = new MemoryStream(headBackImgBytes);
            var headBackImgBitmap = new Bitmap(headBackImgStream);

            byte[] headImgBytes = webClient.DownloadData("https://ss0.bdstatic.com/5aV1bjqh_Q23odCf/static/superman/img/logo_top_ca79a146.png");
            var headImgStream = new MemoryStream(headImgBytes);
            var headImgBitmap = new Bitmap(headImgStream);

            string str = "邀请您一起关注校宝秀"; //写什么字？
            Font font = new Font("宋体",30f); //字是什么样子的？
            Brush brush = Brushes.Wheat; //用红色涂上我的字吧；
            PointF point = new PointF(10f,10f); //从什么地方开始写字捏？
            //横着写还是竖着写呢？
            System.Drawing.StringFormat sf = new StringFormat();
            //还是竖着写吧
            sf.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            //开始写咯
            g.DrawString(str,font,brush,point,sf);


            //var brush = new SolidBrush(Color.Black);
            var bitmap = new Bitmap(width, height);
            var graphics = Graphics.FromImage(bitmap);
            //graphics.FillEllipse(brush, 0, 0, width, height);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.Clear(Color.Azure);

            graphics.Save();
            return bitmap;
        }
    }
}
