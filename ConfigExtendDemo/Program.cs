using ConfigExtendDemo.Section_Collection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigExtendDemo
{
    /*http://www.cnblogs.com/fish-li/archive/2011/12/27/2304063.html*/
    /*引用类型变量 地址 拷贝*/
    class Program
    {
        static void Main(string[] args)
        {
            var section = ConfigurationHelper.configSectionAtt;

            var sec_Ele_Pro = ConfigurationHelper.configSectionEle;

            //var sec_Col = ConfigurationHelper.configSectionCol;
            SectionExtend3 configSectionCol = ConfigurationManager.GetSection("SectionExtend3") as SectionExtend3;

            var cdata = ConfigurationHelper.configSectionCDATA;

            Console.ReadLine();
        }
        #region
        /*
         * /// <summary>
    /// 缓存辅助类
    /// </summary>
    public class CacheManager
    {
        /// <summary>
        /// 获取数据缓存
        /// </summary>
        /// <param name="CacheKey">键</param>
        public static object GetCache(string CacheKey)
        {
            return HttpRuntime.Cache[CacheKey];
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="CacheKey">键</param>
        /// <param name="objObject">值</param>
        public static void SetCache(string CacheKey, object objObject)
        {
            HttpRuntime.Cache.Insert(CacheKey, objObject);
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="CacheKey">键</param>
        /// <param name="objObject">值</param>
        /// <param name="Timeout">滑动过期时间</param>
        public static void SetCache(string CacheKey, object objObject, TimeSpan Timeout)
        {
            HttpRuntime.Cache.Insert(CacheKey, objObject, null, DateTime.MaxValue, Timeout,
                System.Web.Caching.CacheItemPriority.NotRemovable, null);
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="CacheKey">键</param>
        /// <param name="objObject">值</param>
        /// <param name="absoluteExpiration">决定过期时间</param>
        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration)
        {
            HttpRuntime.Cache.Insert(CacheKey, objObject, null, absoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="CacheKey">键</param>
        /// <param name="objObject">值</param>
        /// <param name="absoluteExpiration">决定过期时间</param>
        public static void SetCache(string CacheKey, object objObject, string dependencyFile, DateTime absoluteExpiration)
        {
            HttpRuntime.Cache.Insert(CacheKey, objObject, new CacheDependency(dependencyFile), absoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration);
        }


        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        public static void RemoveCache(string CacheKey)
        {
            HttpRuntime.Cache.Remove(CacheKey);
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            IDictionaryEnumerator CacheEnum = HttpRuntime.Cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                HttpRuntime.Cache.Remove(CacheEnum.Key.ToString());
            }
        }
    }
         */
        #endregion
    }
}
