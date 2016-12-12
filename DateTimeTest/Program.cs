using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DateTimeTest
{
    /// <summary>
    /// DateTime 测试项目
    /// </summary>
    public class Program
    {
        /// <summary>
        /// access
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Test t = new Test();

            //获得上周、下周日期
            //DateTime dt = t.GetWeekUpOfDate(DateTime.Now, DayOfWeek.Monday, -1);
            //Console.WriteLine("上周一" + dt + "--" + dt.ToString("yyyy-MM-dd 00:00:00"));
            //Console.WriteLine("上周日" + dt.AddDays(6));

            //DateTime dt2 = t.GetWeekUpOfDate(DateTime.Now, DayOfWeek.Monday, -2);
            //Console.WriteLine("上上周一" + dt2);

            //DateTime dt3 = t.GetWeekUpOfDate(DateTime.Now, DayOfWeek.Monday, 1);
            //Console.WriteLine("下周一" + dt3);

            //DateTime dt4 = t.GetWeekUpOfDate(DateTime.Now, DayOfWeek.Monday, 0);
            //Console.WriteLine("本周一" + dt4);

            //Console.WriteLine("Days：时间差" + (DateTime.Parse("2016-12-31 00:00:00") - DateTime.Parse("2015-10-30 23:05:28")).Days);

            //Console.WriteLine("VIP时间：" + DateTime.Parse("2016-07-12 13:44:49").Date.AddDays(310));//.AddMinutes(-1));

            //Test.AddDaysMonths();

            //Test.IntToDateTime();

//            逻辑加法通常用符号“+”或“∨”来表示。逻辑加法运算规则如下：
//0+0=0， 0∨0=0
//0+1=1， 0∨1=1
//1+0=1， 1∨0=1
//1+1=1， 1∨1=1

//    逻辑乘法通常用符号“×”或“∧”或“·”来表示。逻辑乘法运算规则如下：
//0×0=0， 0∧0=0， 0·0=0
//0×1=0， 0∧1=0， 0·1=0
//1×0=0， 1∧0=0， 1·0=0
//1×1=1， 1∧1=1， 1·1=1
            Console.WriteLine("测试未使用FlagsAttribute属性");
            Color1 MyColor1 = Color1.Red | Color1.Blue & Color1.Green;
            Console.WriteLine("MyColor1={0}", MyColor1);

            Color1 MyColor_1 = Color1.Red | Color1.Blue;
            Console.WriteLine("MyColor_1={0}", MyColor_1);
            Console.WriteLine("MyColor_2={0}", Color1.Red & Color1.Blue & Color1.Green);

            Console.WriteLine("测试使用FlagsAttribute属性");
            Color2 MyColor2 = Color2.Red | Color2.Blue;
            Console.WriteLine("MyColor2={0}", MyColor2);

            Console.WriteLine("MyColor2_1={0}", (short)(Color2.Red | Color2.Blue));
            Console.WriteLine("MyColor2_2={0}", Color2.Red & Color2.Blue);

            Console.ReadKey();
        }
    }

    public class Test
    {
        /// <summary>
        /// 根据指定日期，获得上周、下周等日期
        /// </summary>
        /// <param name="dt">指定日期</param>
        /// <param name="weekday">要获得星期几，如周一、周二等</param>
        /// <param name="Number">指定差值（-1：上周，-2：上上周，1：下周，0：本周）</param>
        /// <returns></returns>
        public DateTime GetWeekUpOfDate(DateTime dt, DayOfWeek weekday, int Number)
        {
            int gainWD = (int)weekday;
            int assignWD = (int)dt.DayOfWeek;
            return assignWD == gainWD ? dt.AddDays(7 * Number) : dt.AddDays(7 * Number - assignWD + gainWD);
        }

        /// <summary>
        /// 添加天数和月
        /// </summary>
        public static void AddDaysMonths()
        {
            Console.WriteLine(DateTime.Parse("2015-12-28").AddMonths(1));

            Console.WriteLine("AddDays：当前时间" + DateTime.Now.AddDays(30) + " --- 指定时间：" + DateTime.Parse("2016-03-13").AddDays(30));
        }

        /// <summary>
        /// unix时间戳和DateTime互转
        /// </summary>
        public static void IntToDateTime()
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

            // unix时间戳 to 时间
            Console.WriteLine("IntToDateTime:" + startTime.AddSeconds(1395658984));

            // 当前时间 to unix时间戳
            Console.WriteLine("IntToDateTime1:" + (DateTime.Now - startTime).TotalSeconds);
        }

        /// <summary>
        /// TotalSeconds时间相差的秒数，TotalMilliseconds时间相差的毫秒数
        /// </summary>
        public static void Tee()
        {
            Console.WriteLine("TEE1:" + (DateTime.Now - DateTime.Parse("0001-01-01 00:00:00")).TotalSeconds);
            Console.WriteLine("TEE2:" + (2592000 - 86400));
        }
    }

    enum Color1
    {
        Black = 0,
        Red = 1,
        Green = 2,
        Blue = 4
    };

    [FlagsAttribute]
    enum Color2
    {
        Black = 0,
        Red = 1,
        Green = 2,
        Blue = 4
    };
}
