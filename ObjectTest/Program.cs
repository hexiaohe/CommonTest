using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ObjectTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectTest obj = new ObjectTest();
            obj.Test();

            Console.ReadLine();
        }
    }

    public class ObjectTest 
    {
        public void Test() 
        {
            var result = new PushResult
            {
                SuccessCount = 0,
                FailList = new List<PushResultItem>()
            };

            var data = GetIMTemplateData();

            if (data != null)
            {
                result.FailList.Add(new PushResultItem
                {
                    OpenId = "test",
                    TemplateData = data,
                    UserId = 1,
                    ErrorCode = "false",
                    ErrorMsg = "errorMsg"
                });
            }

            Console.WriteLine(JsonConvert.SerializeObject(result.FailList));
        }

        private TemplateData GetIMTemplateData()
        {
            var ApplyCount = 1;
            var ApplyPhone = "18888888888";
            var ApplyTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");

            return new IMTemplateData
            {
                first = new TemplateDataItem(string.Format("您好，有{0}人通过您的微活动或者微官网报名！\n", ApplyCount)),
                keyword1 = new TemplateDataItem(ApplyPhone, "#FF0000"),
                keyword2 = new TemplateDataItem(ApplyTime, "#FF0000"),
                keyword3 = new TemplateDataItem(ApplyTime, "#FF0000"),
                keyword4 = new TemplateDataItem(ApplyTime, "#FF0000"),
                remark = new TemplateDataItem("\n点击查看详情；\n不想收到提醒？请在用户中心中设置。")
            };
        }
    }

    public class TemplateData
    {
        public TemplateDataItem first { get; set; }

        public TemplateDataItem keyword1 { get; set; }

        public TemplateDataItem keyword2 { get; set; }

        public TemplateDataItem remark { get; set; }
    }

    public class IMTemplateData : TemplateData
    {
        public TemplateDataItem keyword3 { get; set; }

        public TemplateDataItem keyword4 { get; set; }

    }

    public class TemplateDataItem
    {
        public TemplateDataItem(string v, string c = "#173177")
        {
            value = v;
            color = c;
        }
        public string value { get; set; }

        public string color { get; set; }
    }

    public class PushResult
    {
        public int TotalCount { get; set; }

        public int SuccessCount { get; set; }

        public int FailCount
        {
            get
            {
                return TotalCount - SuccessCount;
            }
        }

        public List<PushResultItem> FailList { get; set; }
    }

    public class PushResultItem
    {
        public long UserId { get; set; }

        public string OpenId { get; set; }

        public TemplateData TemplateData { get; set; }

        public string ErrorMsg { get; set; }

        public string ErrorCode { get; set; }
    }
}
