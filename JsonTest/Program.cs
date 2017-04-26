using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JsonTest
{
    /// <summary>
    /// Json测试项目
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            var ii = "abcdef";
            var iii = ii.Substring(0, 5);
            var temp = string.Empty;
            var s = JsonConvert.DeserializeObject<List<ChoiceResponses>>(string.Empty);

            string josn = @"{'_answers': [{'_questionResponses': [{'_choiceResponses': [{'_selected': true},{'_selected': false},{'_selected': false},
                        {'_selected': false},
                        {'_selected': false}]}]},{'_questionResponses': [
                {'_itemResponses': {'_answer': ['','','','']}}]}]}";

            var ss = JsonConvert.DeserializeObject<SATAnswer>(" ") ?? new SATAnswer();
            //Random rd = new Random(5);
            //Console.WriteLine(rd.Next());
            //Random rd1 = new Random(100);
            //Console.WriteLine(rd1.Next());
            //由6(n-1) <= (sum-randNum) <= 12(n-1)可得sum - 12(n-1) <= randNum <= sum - 6(n-1)。
            //又由6 <= randNum <= 12计算得到红包的上下界:
            //$min = ($sum - 12 * ($i - 1))> 6 ? ($sum - 12 * ($i - 1)):6;
            //$max = ($sum - 6 * ($i - 1))< 12 ? ($sum - 6 * ($i - 1)):12;
            //红包计算
            double totalCoup = 100;//红包大小
            double minCoup = 6;//最少领取
            double MaxCoup = 12;//最大领取
            double powerNum = 10;//领取红包的人数
            for (var i = powerNum;i >= 1;i--){
                var min = (totalCoup - 12 * (i - 1)) > 6 ? (totalCoup - 12 * (i - 1)) : 6;
                var max = (totalCoup - 6 * (i - 1)) < 12 ? (totalCoup - 6 * (i - 1)) : 12;
                var random = new Random();
                var randNum = random.NextDouble() * (min - max) + min;

                totalCoup -= randNum;
                Console.WriteLine("i:"+ i + ",min:" + min.ToString("0.00") + ",max:" + max.ToString("0.00") + ",randNum:" + randNum.ToString("0.00"));
            }
            Console.ReadKey();
        }
    }

    public class JsonTest 
    {
//        public static string jsontest() 
//        {
//            string josn = @"{'_answers': [{'_questionResponses': [{'_choiceResponses': [{'_selected': true},{'_selected': false},{'_selected': false},
//                        {'_selected': false},
//                        {'_selected': false}]}]},{'_questionResponses': [
//                {'_itemResponses': {'_answer': ['','','','']}}]}]}";

//        }
    }

    public class SATAnswer
    {
        public int ii { get; set; }

        public IList<SATAnswersList> _answers { get; set; }
    }
    public class SATAnswersList
    {
        public IList<QestionResponsesList> _questionResponses { get; set; }
    }
    public class QestionResponsesList
    {
        public ItemResponses _itemResponses { get; set; }
        public IList<ChoiceResponses> _choiceResponses { get; set; }
    }

    public class ChoiceResponses
    {
        public bool _selected { get; set; }
    }
    public class ItemResponses
    {
        public IList<string> _answer { get; set; }
    }

}
