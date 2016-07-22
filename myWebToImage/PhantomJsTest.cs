using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace myWebToImage
{
    public class PhantomJsTest
    {
        public static void TestPhantomJsImgBase()
        {
            try
            {
                var phantomJsUrl = "http://localhost:8000";
                //var resUrl = "http://xiaobao1001.cn/Home/InvitationRoute?p=oldinvitenew%3fid%3d615";
                var resUrl = "http://xiaobao1001.cn/Home/InvitationRoute?p=oldinvitenew?id=615";
                WebClient webClient = new WebClient();
                NameValueCollection postValues = new NameValueCollection();
                postValues.Add("url", resUrl);
                byte[] data = webClient.UploadValues(phantomJsUrl, postValues);
                // 从返回的Base64编码中获取图片数据
                string imageInfo = Encoding.UTF8.GetString(data);
                if (!String.IsNullOrEmpty(imageInfo))
                {
                    data = Convert.FromBase64String(imageInfo);
                    MemoryStream ms = new MemoryStream();
                    ms.Write(data, 0, data.Length);

                    var image = System.Drawing.Image.FromStream(ms);
                    image.Save(@"D:\work\phantomjs_dev\pic\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg");
                    ms.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void PhantomJsProcess()
        {
            try
            {
                var seconds = 0;
                var currentDirectory = "D:\\project\\phantomjs";//Environment.CurrentDirectory
                #region 启动进程
                Process p = new Process();
                //p.StartInfo.FileName = currentDirectory + "\\phantomjs.exe";
                p.StartInfo.FileName = "cmd.exe";
                //p.StartInfo.WorkingDirectory = currentDirectory;
                //p.StartInfo.Arguments = string.Format("--ignore-ssl-errors=yes --load-plugins=yes " + currentDirectory + "//server.js 8000");
                //p.StartInfo.Arguments = currentDirectory + "\\phantomjs.exe " + currentDirectory + "\\server.js 8000";
                p.StartInfo.UseShellExecute = false;


                p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;//重定向标准错误输出


                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                if (p.Start()) //开始进程 
                {
                    p.StandardInput.WriteLine(currentDirectory + "\\phantomjs.exe " + currentDirectory + "\\server.js 8000");
                    //if (seconds == 0)
                    //{
                    //    p.WaitForExit(); //这里无限等待进程结束 
                    //}
                }             


                //if (!p.Start())
                //    throw new Exception("无法Headless浏览器.");

                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
