using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ExressionTest.FuncTest(ExressionTest.StringToBool, "200000");

            Console.ReadKey();
        }
    }

    public class ExressionTest
    {
        public static void FuncTest(Func<string, bool> func, string s)
        {
            if (func(s)) 
            {
                Console.WriteLine(s + " 是int"); 
            }            
        }

        public static bool StringToBool(string s) 
        {
            int i = 0;
            return int.TryParse(s, out i);            
        } 

        public static int Count(Expression<Func<string, bool>> predicate, List<Person> person)
        {
            return 0;
        }
    }

    public class Person 
    {
        public string Name { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
    }
}
