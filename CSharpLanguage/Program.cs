using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpLanguage
{
    class Program
    {

        ThreadLocal<int?> NowLocal; 

        static Program()
        {
            //
            属性 = 2;
        }

        public static DateTime ConvertTime(long milliTime)
        {
            long timeTricks = new DateTime(1970, 1, 1).Ticks + milliTime * 10000 + TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours * 3600 * (long)10000000;
            return new DateTime(timeTricks);
        }

        static void Main(string[] args)
        {

            for (var i = 0; i < 100; i++)
            {
                int i1 = i;
                var t = new Task(() =>
                {
                    if (Methods.NowI == null) Methods.NowI = i1;
                    var NowLocal = Methods.NowLocal;
                    if (NowLocal == null)
                    {
                        NowLocal = new ThreadLocal<int?>();
                        NowLocal.Value = i1;
                    }
                    //var nowI = Thread.GetData(Thread.GetNamedDataSlot("NowI"));
                    //if (nowI == null)
                    //{
                    //    nowI = i1;
                    //    Thread.SetData(Thread.GetNamedDataSlot("NowI"), i1);
                    //}

                    Console.WriteLine(string.Format("第{0}次循环, i值：{1}，Local值：{2}，线程ID：{3}\r\n", i1, Methods.NowI, NowLocal.Value, Thread.CurrentThread.ManagedThreadId));
                    //NowI = null; //[ThreadStatic] 线程结束前要手动清理
                });
                t.Start();
            }
            //Console.WriteLine(resultString.ToString());
            Console.Read();




            var ti = ConvertTime(1471881600000);



            #region 静态类、静态构造函数
            /*关于静态构造函数*/
            var s1 = StaticClass.str; //1.调用静态构造函数
            var s11 = StaticClass.str;  //2.不调用静态构造函数
            var s2 = DynamicClass.str;

            #endregion


            #region C#匹配
            //string str = "sfhqweda da gs上由1128878单（入住单号）换至此单由上啥时候";
            //var start = str.IndexOf("由");
            //var end = str.IndexOf("单（入住单号）换至此单");

            //var num = str.Substring(start + 1, end - 1 - start);

            ////Match match = Regex.Match(str, @"^(\w+)的英文名字是(\w+)$");
            //// Regex.Matches(s, @"是(\w+)，")
            //Match match = Regex.Match(str, @"由(\w+)单（");
            //if (match.Success)
            //{
            //    var strs = match.Value;
            //    var matchValue = match.Groups[1].Value;
            //}
            #endregion

            const int a =0;
            //Console.Write(Program.属性);
            Console.Read();
        }

        /*static readonly:运行时确定*/
        public static readonly int 属性 = 1;

        public static readonly int[] x = { 1, 3, 4 };

        public static readonly int[] y = new int[] { 1, 2 };

        //引用类型常量，初始值只能是stringh或者null
        //public const int[] z = { 2, 3 };
       // public const int[] w = new int[] { 2, 55 };

        //private readonly int 字段 { get; set; }

    }



    public interface Test
    {
        //错误:接口不能包含字段
        //int a;
        int b{get;set;}
    }

    /*
     * 抽象类中必须有抽象方法，同时也可以有非抽象方法，既可以有方法的具体实现
     * 接口可以多继承，抽象类不行
     */
}
