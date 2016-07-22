using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PhantomGuardProcess
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void PhantomTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                var count = 0;
                Process[] pp = Process.GetProcesses();
                for (int i = 0; i < pp.Length - 1; i++)
                {
                    var ppp = pp[i];
                    if (ppp.ProcessName == "phantomjs")
                    {
                        count = 1;
                    }
                }

                if (count == 0)
                {
                    var currentDirectory = Environment.CurrentDirectory + "\\PhantomJs\\";
                    OpenPhantomJs(currentDirectory);
                    this.textBox1.Text += "--- " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " phantomjs重新启动 ---\r\n";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void OpenPhantomJs(string dir)
        {
            PhantomJsProcess(dir);
            var tr = new Thread(KillCmd);
            Thread.Sleep(2000);
            tr.Start();
        }

        private void PhantomJsProcess(string dir)
        {
            try
            {
                #region 启动进程
                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                if (p.Start()) //开始进程
                {
                    p.StandardInput.WriteLine(dir + "\\phantomjs.exe " + dir + "\\render.js 8000");
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool CheckPhantomJs()
        {
            Process[] pp = Process.GetProcesses();
            for (int i = 0; i < pp.Length - 1; i++)
            {
                var ppp = pp[i];
                if (ppp.ProcessName == "phantomjs")
                {
                    return true;
                }
            }
            return false;
        }

        private void KillPhantomJs()
        {
            Process[] pp = Process.GetProcesses();
            for (int i = 0; i < pp.Length - 1; i++)
            {
                var ppp = pp[i];
                if (ppp.ProcessName == "phantomjs")
                {
                    ppp.Kill();
                    return;
                }
            }
        }

        private void KillCmd()
        {
            Process[] pp = Process.GetProcesses();
            for (int i = 0; i < pp.Length - 1; i++)
            {
                var ppp = pp[i];
                if (ppp.ProcessName == "cmd")
                {
                    ppp.Kill();
                    return;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = Environment.CurrentDirectory + "\\phantomjs.exe";
            string ExcuteArg = Environment.CurrentDirectory + "\\render.js 8000";
            p.StartInfo.Arguments = string.Format(ExcuteArg);
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;//重定向标准输出
            p.StartInfo.RedirectStandardError = false;//重定向错误输出 
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            string[] result = { };
            if (!p.Start())
            {
                throw new Exception("无法启动Headless测试引擎.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var phantomJsUrl = "http://127.0.0.1:8000";
                var resUrl = string.Format("http://{0}/Home/InvitationRoute?p={1}","192.168.5.108:7001", "oldinvitenew?id=183&iscutpage=true");
                var webClient = new WebClient();
                var postValues = new NameValueCollection { { "url", resUrl }, { "size", "640*1047" } };
                byte[] data = webClient.UploadValues(phantomJsUrl, postValues);
                // 从返回的Base64编码中获取图片数据
                string imageInfo = Encoding.UTF8.GetString(data);
                if (!string.IsNullOrEmpty(imageInfo))
                {
                    data = Convert.FromBase64String(imageInfo);
                    var ms = new MemoryStream();
                    ms.Write(data, 0, data.Length);

                    var image = System.Drawing.Image.FromStream(ms);
                    image.Save(@"D:\work\phantomjs_dev\pic\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg");
                    ms.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 调用第三方PhantomJs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                client.DefaultRequestHeaders.ExpectContinue = false; //REQUIRED! or you will get 502 Bad Gateway errors
                var pageRequestJson = new System.Net.Http.StringContent(System.IO.File.ReadAllText("request.json"));
                var response = client.PostAsync("https://PhantomJScloud.com/api/browser/v2/a-demo-key-with-low-quota-per-ip-address/", pageRequestJson).Result;
                var responseStream = response.Content.ReadAsStreamAsync().Result;
                using (var fileStream = new System.IO.FileStream("content.jpg", System.IO.FileMode.Create))
                {
                    responseStream.CopyTo(fileStream);
                }
                Console.WriteLine("*** Headers, pay attention to those starting with 'pjsc-' ***");
                Console.WriteLine(response.Headers);
            }
        }

    }
}
