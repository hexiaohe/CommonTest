using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebToImage
{
    public class Program
    {
        static void Main(string[] args)
        {
            //OLayer ow = new OLayer();
            //ow.CaptureImage("http://bbs.csdn.net/topics/190175917");
            A a = new B();
            Console.WriteLine(a.GetSomeThing());
            Console.ReadKey();
        }
    }

    public class A
    {
        public virtual int GetSomeThing()
        {
            return 0;
        }
    }

    public class B : A
    {
        public override int GetSomeThing()
        {
            return 1;
        }
    }
}
