using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JsonTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string josn = @"{'_answers': [{'_questionResponses': [{'_choiceResponses': [{'_selected': true},{'_selected': false},{'_selected': false},
                        {'_selected': false},
                        {'_selected': false}]}]},{'_questionResponses': [
                {'_itemResponses': {'_answer': ['','','','']}}]}]}";

            var ss = JsonConvert.DeserializeObject<SATAnswer>(josn);
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
