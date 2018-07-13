using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log4NetDemo
{
    public class LogHelper
    {
        public static readonly ILog loginfo = LogManager.GetLogger("loginfo");
        public static readonly ILog logerror = LogManager.GetLogger("logerror");
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(WebApiLog));

        public static void WriteLog(string info)
        {
            loginfo.Error("---------------测试---------------\n");
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info("---------------测试---------------\n");
            }
 
        }
    }
}
