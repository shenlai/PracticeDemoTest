using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log4NetDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            //log4net.Config.XmlConfigurator.Configure();//初始化配置
            log4net.Config.XmlConfigurator.Configure(
                 new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\log4net.config"));
            ILog log = log4net.LogManager.GetLogger("demo");//获取记录日志的实例


            log.Debug("这是调试信息！");
            while (true)
            {
                LogHelper.WriteLog("");
            }
        }
    }
}
