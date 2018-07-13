using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HttpClientDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            /*日志记录test*/
            //while (true)
            //{
            //    string url = "http://localhost:8088/api/Provider/ProviderInfo";

            //    var req = new RequestModel<int>() { head = new Head() { Version = "123" }, Content = 338 };

            //    //string reqJsonStr = JsonHelper.ConvertObjToJsonStr(req);
            //    req = null;
            //    var resStr = HttpClientHelper.PostJsonStr(url, req);

            //    var model = HttpClientHelper.PostJsonStr<RequestModel<ProviderInfo>>(url, req);
            //}
            
            /*Post*/

            ////string url = "http://localhost:8088/api/Provider/ProviderInfo";
            //string url = "http://localhost:8088/api/Provider/ProviderInfo";

            //var req = new RequestModel<int>() { head = new Head() { Version = "123" }, Content = 338 };

            ////string reqJsonStr = JsonHelper.ConvertObjToJsonStr(req);

            //var resStr = HttpClientHelper.PostJsonStr(url, req);

            //var model = HttpClientHelper.PostJsonStr<RequestModel<ProviderInfo>>(url, req);

            ///*Get*/
            //int id = 338;
            //string getUrl = "http://localhost:8088/api/Provider/GetProviderInfo";
            //StringBuilder getparams = new StringBuilder();
            //getparams.Append(string.Format("?id={0}", id));
            //var getRes = HttpClientHelper.GetJsonStr(url, getparams.ToString());

            //var getModel = HttpClientHelper.GetJsonStr<RequestModel<ProviderInfo>>(getUrl, getparams.ToString());


            /*1.1.25	贷款资料审核新 性能排查*/
            for (int i = 671; i < 1148; i++)
            {
                string url = "http://localhost:8085/rzr/Info/GetProductInfoNeedNew";
                var req = new RequestModel<ProductDetailRequest>() { Content = new ProductDetailRequest() { ProductID = i } };
                HttpClientHelper.PostJsonStr<RequestModel<ProviderInfo>>(url, req);
            }
                Console.ReadLine();
            
        }
    }

    public class RequestModel<T> 
    {
        public Head head { get; set; }
        public T Content { get; set; }
    }
    public class Head
    {
        public string Version { get; set; }
    }

    public class ProviderInfo
    {
        public int PrdId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string TrueName { get; set; }
        public string Telephone { get; set; }

        public string CompanyName { get; set; }
    }

    public class ProductDetailRequest
    {

        public double LoanAmount { get; set; }
        public int LoanPeriod { get; set; }
        public long ProductID { get; set; }
    }
}
