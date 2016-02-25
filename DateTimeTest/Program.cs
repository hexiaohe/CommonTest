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

            DateTime dt = t.GetWeekUpOfDate(DateTime.Now, DayOfWeek.Monday, -1);
            Console.WriteLine("上周一"+dt +"--"+dt.ToString("yyyy-MM-dd 00:00:00"));
            Console.WriteLine("上周日" + dt.AddDays(6));

            DateTime dt2 = t.GetWeekUpOfDate(DateTime.Now, DayOfWeek.Monday, -2);
            Console.WriteLine("上上周一" + dt2);

            DateTime dt3 = t.GetWeekUpOfDate(DateTime.Now, DayOfWeek.Monday, 1);
            Console.WriteLine("下周一" + dt3);

            DateTime dt4 = t.GetWeekUpOfDate(DateTime.Now, DayOfWeek.Monday, 0);
            Console.WriteLine("本周一" + dt4);

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
    }
}
