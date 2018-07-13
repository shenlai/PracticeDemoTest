using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonHandle
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = GetDivNumber(13);
  
            /*时间格式处理*/
            JsonSerializerSettings set = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" };
            //JsonSerializerSettings set = new JsonSerializerSettings();
            var model = new TestModel() { attdouble = 1.2323, attint = 100, attstring = "测试序列化",atttime = DateTime.Now};
            var t = JsonConvert.SerializeObject(model,set);
            //var json = Json();
            string jsonString = "{\"attstring\":\"测试序列化\",\"attint\":100,\"attdouble\":1.2323}";
            string jsonS = "{\"attstring\":\"测试序列化\",\"attint\":100,\"attdouble\":1.2323,\"atttime\":\"2015-10-28T18:10:08.6207314+08:00\"}";
            var tmodel = JsonConvert.DeserializeObject<TestModel>(jsonString);
            
            var tm = JsonConvert.DeserializeObject<TestModel>(jsonS, set);

            string stest = JsonHelper.GetJsonFromObject(model);//时间格式有问题 :"{\"attstring\":\"测试序列化\",\"attint\":100,\"attdouble\":1.2323,\"atttime\":\"2015-11-27T14:28:47.2948109+08:00\"}"
            
            /*时间格式处理*/
            var dataFormat = new IsoDateTimeConverter() { DateTimeFormat="yyyy-MM-dd"};
            string test = JsonConvert.SerializeObject(model, dataFormat);

            string s = JsonHelper.GetJsonFromObject(model);
            //Console.Write(test);






            Console.ReadLine();
        }

        public static string GetDivNumber(int number)
        {
            string twoJinzhi = Convert.ToString(number, 2);
            char[] chars = twoJinzhi.ToCharArray();
            string ret = "";
            for (int i = chars.Length - 1; i >= 0; i--)
            {
                if (twoJinzhi[i] == '1')
                    ret += Math.Pow(2, chars.Length - 1 - i);
            }

            return ret;
        }

    }
}
