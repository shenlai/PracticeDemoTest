using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace JsonHandle
{
    public class JsonHelper
    {
        public static string GetJsonFromObject(object obj)
        {
            /*
             * string.Empty ： ""
             * default(string) :null
             */
            string jsonStr = string.Empty;
            try
            {
                var dateFormatConverter = new IsoDateTimeConverter()
                {
                    DateTimeFormat = "yyyy-MM-dd"
                };
                jsonStr = JsonConvert.SerializeObject(obj, dateFormatConverter);
            }
            catch { }

            return jsonStr;
        }
    }
}
