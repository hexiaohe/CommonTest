using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;

namespace QrCodeNetTest
{
    /// <summary>
    /// QrCode二维码生成器 测试项目
    /// 参考：http://www.tuicool.com/articles/f6ruei
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            Test.CreateQrCode();
        }
    }

    public class Test
    {
        public static void CreateQrCode()
        {
            using (var ms = new MemoryStream())
            {
                string stringtest = "http://www.baidu.com";
                GetQRCode(stringtest, ms);

                string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg";
                using (FileStream fs = new FileStream(filename, System.IO.FileMode.Create))
                {
                    Bitmap bitmap = new Bitmap(ms);
                    bitmap.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
        }

        public static bool GetQRCode(string strContent, MemoryStream ms)
        {
            ErrorCorrectionLevel Ecl = ErrorCorrectionLevel.M; //误差校正水平   
            string Content = strContent;//待编码内容  
            QuietZoneModules QuietZones = QuietZoneModules.Two;  //空白区域   
            int ModuleSize = 8;//大小  
            var encoder = new QrEncoder(Ecl);
            QrCode qr;
            if (encoder.TryEncode(Content, out qr))//对内容进行编码，并保存生成的矩阵  
            {
                var render = new GraphicsRenderer(new FixedModuleSize(ModuleSize, QuietZones), Brushes.Red, Brushes.White);
                render.WriteToStream(qr.Matrix, System.Drawing.Imaging.ImageFormat.Png, ms);
            }
            else
            {
                return false;
            }
            return true;
        }   
    }
}
