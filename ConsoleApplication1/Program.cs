using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        public const string a = "a";
        public const string b = "b";
        public const string c = "c";
        static void Main(string[] args)
        {


            var list = new List<string>() { "123", "222" };
            var str = JsonConvert.SerializeObject(list);


            //string A = "A";
            //string B = "B";
            //string C = "C";

            //var D = A + B + C;
            //var d = a + b + c;
            //Console.WriteLine("字符串d:{0}", d);

            EnumWxTradeStatus x = (EnumWxTradeStatus)EnumTradeStatus.CLOSED;

            var rr = EnumWxTradeStatus.WxERROR.Equals(EnumTradeStatus.CLOSED);

            Console.ReadLine();

        }

    }

    public enum EnumWxTradeStatus
    {
        [Description("未知")]
        WxNone = 0,
        [Description("交易成功")]
        WxSUCCESS = 1,
        [Description("等待支付")]
        WxINRROCESS = 2,
        [Description("交易关闭")]
        WxCLOSED = 3,
        [Description("交易失败")]
        WxFAIL = 4,
        [Description("系统异常")]
        WxERROR = 5
    }

    public enum EnumTradeStatus
    {
        [Description("未知")]
        None = 0,
        [Description("交易成功")]
        SUCCESS = 1,
        [Description("等待支付")]
        INRROCESS = 2,
        [Description("交易关闭")]
        CLOSED = 3,
        [Description("交易失败")]
        FAIL = 4,
        [Description("系统异常")]
        ERROR = 5
    }
}
