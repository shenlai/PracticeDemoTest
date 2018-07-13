using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace HttpClientDemo
{
    public class JsonHelper
    {
        public static string ConvertObjToJsonStr(object input)
        {
            string res = string.Empty;
            try
            {
                res = JsonConvert.SerializeObject(input);
            }
            catch { }
            return res;
        }

        public static T ConvertJsonStrToT<T>(string jsonstr)
        {
            T t = default(T);
            try
            {
                t = JsonConvert.DeserializeObject<T>(jsonstr);
            }
            catch { }
            return t;
        }
    }
}
