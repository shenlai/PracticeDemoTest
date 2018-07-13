using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AopDemo.Reflection;

namespace AopDemo
{
    [AOPTest]
    public class TestClass : ContextBoundObject
    {
        //[AOPTest] 类属性
        public void Print()
        {
            Console.WriteLine("Print");
        }

        //public void PrintWithNotAop()
        //{
        //    Console.WriteLine("PrintWithNotAop");
        //}

    }

    public class TestClassOther
    {
        public void Print()
        {
            //Console.WriteLine("Print");
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            #region 继承ContextBoundObject性能
            //Stopwatch sw = new Stopwatch();
            //long t = 0, t2 = 0;
            
            //sw.Start();
            //for (int i = 0; i < 100000; i++)
            //{
            //    new TestClass().Print();
            //}
            //t = sw.ElapsedMilliseconds;
            //sw.Restart();
            //for (int i = 0; i < 100000; i++)
            //{
            //    new TestClassOther().Print();
            //}
            //t2 = sw.ElapsedMilliseconds;
            //sw.Stop();

            //Console.WriteLine("继承时间t:{0},不继承时间t2:{1}", t, t2);
            //Console.ReadLine();
            #endregion

            TestClass tc = new TestClass();
            tc.Print();
            //tc.PrintWithNotAop();
            /*
             * /*Output
             * AOP Call Begin
             * Print
             * AOP Call End
             */

            Console.ReadLine();


        }
    }
}
