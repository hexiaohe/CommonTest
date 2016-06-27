using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebToImage
{
    class Program
    {
        static void Main(string[] args)
        {
            OLayer ow = new OLayer();
            ow.CaptureImage("http://bbs.csdn.net/topics/190175917");
        }
    }
}
