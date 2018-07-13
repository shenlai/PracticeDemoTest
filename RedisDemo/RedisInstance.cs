using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisDemo
{
    /// <summary>
    /// reidsClient 对象实例
    /// </summary>
    public sealed class RedisInstance
    {
        /// <summary>
        /// redis服务器IP
        /// </summary>
        private static readonly string serviceIp = "127.0.0.1";
        /// <summary>
        /// redis服务器端口号
        /// </summary>
        private static readonly int servicePort = 6379;

        /// <summary>
        /// 单例实例
        /// static readonly：运行时常量，只能在静态构造函数中修改
        /// </summary>
        private static readonly RedisClient redisClient = new RedisClient(serviceIp, servicePort);

        /// <summary>
        /// 构造函数私有化
        /// </summary>
        private RedisInstance() { }
        
        /// <summary>
        /// 取redisClient实例
        /// </summary>
        /// <returns></returns>
        public static RedisClient GetRedisClient()
        {
            return redisClient;
        }
    }
}
