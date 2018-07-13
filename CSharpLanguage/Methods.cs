using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpLanguage
{
    class Methods
    {
        [ThreadStatic]
        public static int? NowI;
        public static ThreadLocal<int?> NowLocal;
    }

    /* http://www.cnblogs.com/michaelxu/archive/2007/03/29/693401.html*/
    static class StaticClass
    {
        public static readonly string str;
        /*最多只运行一次  第一次访问静态变量str时调用*/
        static StaticClass()
        {
            str = "S123";
        }
    }

    class DynamicClass
    {
        public static readonly string str;
        static DynamicClass()
        {
            str = "D123";
        }
    }
}
