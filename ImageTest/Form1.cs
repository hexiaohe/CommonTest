using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;

namespace ImageTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void GetImage() 
        {
            Bitmap b1 = new Bitmap(800, 600);     //新建位图b1
            Graphics g1 = Graphics.FromImage(b1);  //创建b1的Graphics
            g1.FillRectangle(Brushes.Blue, new Rectangle(0, 0, 0, 0));   //把b1涂成红色
            b1.Save(@"D:\p1.jpg"); //把b1存到D盘
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetImage();
        }
    }
}
