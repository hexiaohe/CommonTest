using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using NReco.PhantomJS;

namespace PhantomJsConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
              //Console.WriteLine(new Random().Next(0,2));//输出2个随机数0，1
//            Console.WriteLine("*** Begin " + DateTime.Now + " *** ");
//            var url = "http://xiaobao1001.cn/Home/InvitationRoute?p=" + HttpUtility.UrlEncode(string.Format("oldinvitenew?id={0}&iscutpage=true", 12418));
//            //for (var i = 1; i <= 10; i++)
//            //{
//            //    Console.WriteLine(Guid.NewGuid());
//            //    Console.WriteLine(Guid.NewGuid().ToString("N"));
//            //    Console.WriteLine("*** Begin " + DateTime.Now + " 线程:" + i + " *** ");
//            //    var i1 = i;
//            //    Task.Run(() => { new NRecoPhantomJSTest().Test(i1); });
//            //}
//            //new NRecoPhantomJSTest().Test(0);
//            //Console.WriteLine("*** End " + DateTime.Now + " *** ");

//            //Console.WriteLine((DateTime.Now.AddSeconds(5) - DateTime.Now).Ticks);
//            //var ticks = (DateTime.Now.AddSeconds(5) - DateTime.Now).Ticks;
//            //var ts = new TimeSpan(ticks);
//            //Console.WriteLine(ts.TotalSeconds);
//            var html = @"<!DOCTYPE html><html><head lang=""zh-CN""><meta charset=""UTF-8""><title>生产图片</title></head><body>
//                        <div class=""A4-page-content""><div class=""A4-content-slogan""><div class=""content-slogan-tirle"">圣安东尼跆拳道</div><div class=""content-slogan-tips"">专注致力于跆拳道培训</div></div>
//                        <div class=""A4-content-code""><div class=""content-code-box""><img src=""http://greedyint-dev.oss-cn-hangzhou.aliyuncs.com/xbshow/QrCode/20160722180710-a9dbc.jpg"" /></div>
//                        <div class=""content-code-tips"">扫码免费试课</div></div><div class=""A4-content-address""><div class=""content-address-item""><h3 class=""item-address-h3"">联系方式</h3>
//                        <p>座机：0571-88888888<br/>刘老师&nbsp;电话：13733333333<br/>微信：admin007</p></div><div class=""content-address-item""><h3 class=""item-address-h3"">地址</h3><p>圣安东尼跆拳道学校宁波分校3号楼201室</p>
//                        </div></div></body></html>";
//            var ms = new MemoryStream();
//            //var html = String.Format("<body>Hello world: {0}</body>", DateTime.Now);
//            //http://xiaobao1001.cn/Home/InvitationRoute?p=oldinvitenew?id=12418&iscutpage=true
//            var resUrl = string.Format("http://{0}/Home/InvitationRoute?p={1}", "xiaobao1001.cn", HttpUtility.UrlEncode(string.Format("oldinvitenew?id={0}&iscutpage=true", 615)));
//            var htmlToImageConv = new NReco.ImageGenerator.HtmlToImageConverter();
//            //htmlToImageConv.Height = 1200;
//            //htmlToImageConv.Width = 1080;
//            //htmlToImageConv.ExecutionTimeout = new TimeSpan(DateTime.Now.AddSeconds(10).Ticks);
//            //htmlToImageConv.GenerateImageFromFile("https://detail.tmall.com/item.htm?spm=a220o.1000855.w5001-12072074390.4.4y6Jne&id=530873358617&rn=eb52ae8b524e7c1f0463cfb60c25acd0&abbucket=1&scene=taobao_shop", ImageFormat.Jpeg.ToString(), ms);
//            htmlToImageConv.GenerateImage(html, ImageFormat.Jpeg.ToString(), ms);
//            var bitMap = new Bitmap(ms);
//            var path = @"D:\work\He\CommonTest\PhantomJsConsole\bin\Debug\" + Guid.NewGuid();
//            bitMap.Save(path + ".jpg");
//            bitMap.Dispose();
            var bb = BusinessInfoHelper.Instance;
            //bb.AddQueue(0, 123, "123_openId");
            for (var k=0; k< 10; k++)
            {
                var kk = k;
                var no = bb.GetRandom();
                Task.Run(() =>
                {
                    Console.WriteLine("线程k:" + kk + ";no:" + no);
                    BusinessInfoHelper.Instance.AddQueue(no, 123 + kk, "123_" + kk + "_openId");
                }
                );
            }
            Task.Run(() =>
            {
                Thread.Sleep(2200);
                var queueInfo1 = bb.GetQueueInfo(0);
                var queueInfo2 = bb.GetQueueInfo(1);
                Console.WriteLine("queueInfo1 " + queueInfo1.Count + ";queueInfo2 " + queueInfo2.Count);
            });
            bb.Start();
            Console.ReadLine();
        }
    }

    public static class PhantomJsCloudTest
    {
        public static void Test(string url)
        {
            try
            {
                Console.WriteLine(String.Format("*** Begin Request *** {0}", DateTime.Now));
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("http://api.PhantomJScloud.com/api/browser/v2/a-demo-key-with-low-quota-per-ip-address/");
                request.ContentType = "application/json";
                request.Method = "POST";
                request.Timeout = 45000; //45 seconds
                request.KeepAlive = false;
                request.MediaType = "application/json";
                request.ServicePoint.Expect100Continue = false; //REQUIRED! or you will get 502 Bad Gateway errors
                using (var streamWriter = new System.IO.StreamWriter(request.GetRequestStream()))
                {
                    //you should look at the HTTP Endpoint docs, section about "userRequest" and "pageRequest" 
                    //for a listing of all the parameters you can pass via the "pageRequestJson" variable.
                    string pageRequestJson = @"{'url':'http://example.com','renderType':'jpeg' }";
                    streamWriter.Write(pageRequestJson);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var response = (System.Net.HttpWebResponse)request.GetResponse();
                Console.WriteLine(String.Format("HttpWebResponse.StatusDescription: {0}", response.StatusDescription));
                using (var streamReader = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    string server_reply = streamReader.ReadToEnd();
                    //Console.WriteLine(String.Format("Server Response content: {0}", server_reply));

                    var data = Convert.FromBase64String(server_reply);
                    MemoryStream ms = new MemoryStream();
                    ms.Write(data, 0, data.Length);

                    var image = System.Drawing.Image.FromStream(ms);
                    image.Save(@"D:\work\phantomjs_dev\pic\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg");
                    ms.Close();
                }
                response.Close();
                Console.WriteLine(String.Format("*** End Request *** {0}", DateTime.Now));
            }
            catch (Exception Ex)
            {
                Console.WriteLine("*** HTTP Request Error ***");
                Console.WriteLine(Ex.Message);
            }
        }

        public static void CaptureAsJPEG()
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                client.DefaultRequestHeaders.ExpectContinue = false; //REQUIRED! or you will get 502 Bad Gateway errors
                var pageRequestJson = new System.Net.Http.StringContent(File.ReadAllText("request.json"));
                var response = client.PostAsync("https://PhantomJScloud.com/api/browser/v2/a-demo-key-with-low-quota-per-ip-address/", pageRequestJson).Result;
                var responseStream = response.Content.ReadAsStreamAsync().Result;
                //using (var fileStream = new FileStream("content123455.jpg", FileMode.Create))
                //{
                //    responseStream.CopyTo(fileStream);
                //}

                using (StreamReader myStreamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8")))
                {
                    string retString = myStreamReader.ReadToEnd();
                }

                var ms = new MemoryStream();
                responseStream.CopyTo(ms);
                //responseStream.Position = 0;
                //byte[] buffer = new byte[2048];
                //int bytesRead = 0;
                //while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) != 0)
                //{
                //    ms.Write(buffer, 0, bytesRead);
                //}
                ms.Position = 0;
                Bitmap destBmp = new Bitmap(ms);//create bmp instance from stream above
                destBmp.Save(@"D:\work\He\CommonTest\PhantomJsConsole\bin\Debug\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg");//save bmp to the path you want

                //var image = System.Drawing.Image.FromStream(ms);
                //image.Save(@"D:\work\He\CommonTest\PhantomJsConsole\bin\Debug\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg");
                //ms.Close();

                Console.WriteLine("*** Headers, pay attention to those starting with 'pjsc-' ***");
                Console.WriteLine(response.Headers);
            }
        }
    }

    public class NRecoPhantomJSTest
    {
        public void Test(int i)
        {
            var phantomJS = new PhantomJS();
            //phantomJS.OutputReceived += (sender, e) =>
            //{
            //    Console.WriteLine("Got data from PhantomJS output: {0}", e.Data);
            //};
            var resUrl = string.Format("http://{0}/Home/InvitationRoute?p={1}", "xiaobao1001.cn", HttpUtility.UrlEncode(string.Format("oldinvitenew?id={0}&iscutpage=true", 615)));
            //http://xiaobao1001.cn/Home/InvitationRoute?p=oldinvitenew?id=12418&iscutpage=true
            using (var outMs = new MemoryStream())
            {
                try
                {
                    var temp = getSS();
                    phantomJS.RunScript(temp, new string[]
                    {
                        resUrl,
                        "640*1047"
                    }, null, outMs);
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    phantomJS.Abort();
                }

                var buffer = outMs.ToArray();
                var imageInfo = Encoding.UTF8.GetString(buffer);
                if (!string.IsNullOrEmpty(imageInfo))
                {
                    buffer = Convert.FromBase64String(imageInfo);
                    var ms = new MemoryStream();
                    ms.Write(buffer, 0, buffer.Length);
                    var bitMap = new Bitmap(ms);
                    var path = @"D:\work\He\CommonTest\PhantomJsConsole\bin\Debug\" + Guid.NewGuid();
                    bitMap.Save(path + ".jpg");
                    bitMap.Dispose();
                    ms.Close();
                }
            }
        }

        public string getSS()
        {
            return @"var system = require('system');
                    if (system.args.length < 3) {
                        phantom.exit();
                    } else {
                        var url = system.args[1];
                        var size = system.args[2].split('*');
                        var page = require('webpage').create();
                        page.viewportSize = { width: size[0], height: size[1] };
                        page.open(url, function (status) {
                            if (status !== 'success') {
                                phantom.exit();
                            } else {
                                waitFor(function () {
                                    return page.evaluate(function () {
                                        var loading2 = $('.lockMask-loading2').css('display');
                                        var img = typeof ($('img[isstopmove=""false""]').attr('src'));
                                        return loading2 === 'none' && img === 'string';
                                    });
                                }, function () {
                                    return page.renderBase64();                                    
                                }, system);
                            }
                        });
                    }
                    function waitFor(testFx, onReady, system, timeOutMillis) {
                        var maxtimeOutMillis = timeOutMillis ? timeOutMillis : 5000,
                        start = new Date().getTime(),
                        condition = false,
                        interval = setInterval(function () {
                            if ((new Date().getTime() - start < maxtimeOutMillis) && !condition) {
                                condition = (typeof (testFx) === 'string' ? eval(testFx) : testFx());
                            } else {
                                if (!condition) {
                                    clearInterval(interval);
                                    phantom.exit();
                                } else {
                                    var pic = typeof (onReady) === 'string' ? eval(onReady) : onReady();
                                    clearInterval(interval);
                                    system.stdout.writeLine(pic);
                                    phantom.exit();
                                }
                            }
                        }, 200);
                    }";
        }
    }
}
