using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HttpClientDemo
{
    public class HttpClientHelper
    {
        /// <summary>
        /// Post请求Main方法
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content">json</param>
        /// <returns></returns>
        private static string PostJsonStr(string url, string content)
        {
            HttpContent httpContent = new StringContent(content);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient httpClient = new HttpClient();
            try
            {
                HttpResponseMessage responese = httpClient.PostAsync(url, httpContent).Result;
                if (responese.IsSuccessStatusCode)
                {
                    //var s = responese.Content;
                    //var ss= responese.Content.ReadAsStringAsync();
                    return responese.Content.ReadAsStringAsync().Result;
                }else
                {
                    //write log
                    //return string
                    return string.Empty;
                }
            }
            catch (Exception)
            {
                //write log
                //return  string
                return string.Empty;
            }
        }

        public static string PostJsonStr(string url,object content)
        {
            var contentStr = JsonHelper.ConvertObjToJsonStr(content);
            return PostJsonStr(url, contentStr);
        }

        /// <summary>
        /// Post请求，返回T结构
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">url</param>
        /// <param name="content">content</param>
        /// <returns>T</returns>
        public static  T PostJsonStr<T>(string url,object content)
        {
            var contentStr = JsonHelper.ConvertObjToJsonStr(content);
            var jsonStr = PostJsonStr(url, contentStr);
            return JsonHelper.ConvertJsonStrToT<T>(jsonStr);
        }

        /// <summary>
        /// Get请求 返回Json
        /// </summary>
        /// <param name="url"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        public static string GetJsonStr(string url,string @params)
        {
            HttpClient httpClient = new HttpClient();
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url + @params).Result;
                if(response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    //write log
                    //return 
                    return string.Empty;
                }
            }
            catch (Exception)
            {
                //write log
                //return 
                return string.Empty;
            }
        }

        /// <summary>
        /// Get请求 返回T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        public static T GetJsonStr<T>(string url,string @params)
        {
            var jsonStr = GetJsonStr(url, @params);
            return JsonHelper.ConvertJsonStrToT<T>(jsonStr);
        }
    }
}
