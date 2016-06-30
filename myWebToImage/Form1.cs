using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace myWebToImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //WebBrowser webBrowser = new WebBrowser();  // 创建一个WebBrowser
            //webBrowser.ScrollBarsEnabled = false;  // 隐藏滚动条
            //webBrowser.Navigate("http://www.360doc.com/content/14/1021/18/19633547_418734439.shtml");  // 打开网页
            //webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser_DocumentCompleted);  // 增加网页加载完成事件处理函数

            //ConvertHTML.HTMLConvertImage("http://localhost:16321/Home/InvitationRoute?p=oldinvitenew?id=183");

            //Bitmap m_Bitmap = WebSiteThumbnail.GetWebSiteThumbnail("http://localhost:16321/Home/InvitationRoute?p=oldinvitenew?id=183", 600, 600, 600, 600);
            //MemoryStream ms = new MemoryStream();
            //m_Bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);//JPG、GIF、PNG等均可  
            //m_Bitmap.Save(@"D:\ConvertHTML111.jpg");

            var bitmap = GraphicTest.GetGraphic(640, 1008);//640, 802
            bitmap.Save(string.Format(@"D:\{0}.jpg", new Guid()));
        }

        /// <summary>
        /// 网页加载完成事件处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser webBrowser = (WebBrowser)sender;

            // 网页加载完毕才保存
            if (webBrowser.ReadyState == WebBrowserReadyState.Complete)
            {
                // 获取网页高度和宽度,也可以自己设置
                int height = webBrowser.Document.Body.ScrollRectangle.Height;
                int width = webBrowser.Document.Body.ScrollRectangle.Width;

                // 调节webBrowser的高度和宽度
                webBrowser.Height = height;
                webBrowser.Width = width;

                Bitmap bitmap = new Bitmap(width, height);  // 创建高度和宽度与网页相同的图片
                Rectangle rectangle = new Rectangle(0, 0, width, height);  // 绘图区域
                webBrowser.DrawToBitmap(bitmap, rectangle);  // 截图

                // 保存图片对话框
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JPEG (*.jpg)|*.jpg|PNG (*.png)|*.png";
                saveFileDialog.ShowDialog();

                bitmap.Save(saveFileDialog.FileName);  // 保存图片
            }
        }
    }
}
