using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Test t = new Test();

            //获得上周、下周日期
            DateTime dt = t.GetWeekUpOfDate(DateTime.Now, DayOfWeek.Monday, -1);
            Console.WriteLine("上周一"+dt +"--"+dt.ToString("yyyy-MM-dd 00:00:00"));
            Console.WriteLine("上周日" + dt.AddDays(6));

            DateTime dt2 = t.GetWeekUpOfDate(DateTime.Now, DayOfWeek.Monday, -2);
            Console.WriteLine("上上周一" + dt2);

            DateTime dt3 = t.GetWeekUpOfDate(DateTime.Now, DayOfWeek.Monday, 1);
            Console.WriteLine("下周一" + dt3);

            DateTime dt4 = t.GetWeekUpOfDate(DateTime.Now, DayOfWeek.Monday, 0);
            Console.WriteLine("本周一" + dt4);

            Console.WriteLine("Days：时间差" + (DateTime.Parse("2016-12-31 00:00:00") - DateTime.Parse("2015-10-30 23:05:28")).Days);

            Test.AddDaysMonths();

            Test.IntToDateTime();

            Console.ReadLine();
        }
    }

    public class Test 
    { 
        public DateTime GetWeekUpOfDate(DateTime dt,DayOfWeek weekday,int Number)
        {
            int wd1=(int)weekday;
            int wd2=(int)dt.DayOfWeek;
            return wd2==wd1?dt.AddDays(7*Number):dt.AddDays(7*Number-wd2+wd1);
        }

        /// <summary>
        /// 添加天数和月
        /// </summary>
        public static void AddDaysMonths() 
        {
            Console.WriteLine(DateTime.Parse("2015-12-28").AddMonths(1));

            Console.WriteLine("AddDays：当前时间" + DateTime.Now.AddDays(30) + " --- 指定时间：" + DateTime.Parse("2016-03-13").AddDays(30));
        }

        public static void IntToDateTime()
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

            // unix时间戳 to 时间
            Console.WriteLine("IntToDateTime:" + startTime.AddSeconds(1395658984));

            // 当前时间 to unix时间戳
            Console.WriteLine("IntToDateTime1:" + (DateTime.Now - startTime).TotalSeconds);
        }

        public static void Tee() 
        { 
            Console.WriteLine("TEE1:" + (DateTime.Now - DateTime.Parse("0001-01-01 00:00:00")).TotalSeconds);
            Console.WriteLine("TEE2:" + (2592000 - 86400));
        }
    }
}
