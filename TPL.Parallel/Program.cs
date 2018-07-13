using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPL.ParallelDemo
{

    //Parallel.For(from,to,init,body,finally)
    //Parallel.ForEach(list,init,body,finally)
    // body 中发生异常，任然执行finally
   
    class Program
    {
        static object lockob = new object();
        static int num = 0;
        static int num1 = 0;
        //资源状态锁，0--未被占有， 1--已被占有  
        static int locknum = 0;
        static string result = "";
        static void Main(string[] args)
        {
            Stopwatch t = new Stopwatch();
            t.Start();
            /*foreach*/

            var list = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(i);
            }
            DAO dao = null;
            try
            {
                Parallel.ForEach(list, () => {

                    dao = new DAO();
                    Console.WriteLine("init thread:{0},task:{1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
                    return Thread.CurrentThread.ManagedThreadId.ToString();
                },
            (i, loop, res) => {

                //if (i % 3 == 0)
                //{
                //    Console.WriteLine("body:{0},strInit:{1},thraed:{2},task:{3},不走后面拉", i, res, Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
                //    return i.ToString();
                //}
                if (i % 3 == 0)
                {
                    throw new InvalidOperationException("task中发生异常1");
                }
                Console.WriteLine("body:{0},strInit:{1},thraed:{2},task:{3}", i, res, Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
                return i.ToString();
            },
                (res) => {
                    Console.WriteLine("finally {0},thraed:{1}", res, Thread.CurrentThread.ManagedThreadId);
                   // throw new InvalidOperationException("task中发生异常1");

                }
                );
            }
            catch (AggregateException e)
            {
                Console.WriteLine("Mess:{0}", e.Message);
            }
            




            /*for*/
            //ParallelLoopResult plr =
            //        Parallel.For(0, 10, () => {
            //            Console.WriteLine("init thread:{0},task:{1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
            //            return Thread.CurrentThread.ManagedThreadId.ToString();
            //        },
            //        (i, pls, strInit) => {
            //            Console.WriteLine("body:{0},strInit:{1},thraed:{2},task:{3}", i, strInit, Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
            //            return i.ToString();
            //        },
            //        (strI) => {
            //            lock (lockob)
            //            {
            //                Console.WriteLine("finally {0},thraed:{1}", strI, Thread.CurrentThread.ManagedThreadId);
            //            }

            //        });

            /*for*/
            //Parallel.For(0, 10, () =>
            //{
            //    Console.WriteLine("init thread:{0},task:{1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
            //    return "";
            //},
            //    (i, loop, res) =>
            //{
            //    var str = "A" + res;
            //    dosomething(i);

            //    res = i.ToString();
            //    Console.WriteLine("body index:{3}, thread：{0}，localInit:{1},TLoca:{2}", Thread.CurrentThread.ManagedThreadId, str, res, i);
            //    //res += "," + i.ToString(); 必须累加
            //    return res; //Tlocal :具有累加效果
            //}, (res) =>
            //{
            //    //每个线程执行一次
            //    Console.WriteLine("finally线程ID：{0},res:{1}", Thread.CurrentThread.ManagedThreadId, res);
            //    //lock (lockob)
            //    //{
            //    //    Console.WriteLine("finally线程ID：{0},res:{1}", Thread.CurrentThread.ManagedThreadId, res);
            //    //}
            //});
            t.Stop();
            Console.WriteLine("耗时：{0}，num:{1},num{2}", t.ElapsedMilliseconds, num, num1);
            Console.WriteLine("结果：{0}，", result);

            t.Start();
            num = 0;
            for (int i = 0; i < 30; i++)
            {
                //dosomething();
                //new Business().dosomething(num);
            }
            t.Stop();
            Console.WriteLine("耗时：{0}，num:{1},num{2}", t.ElapsedMilliseconds, num, num1);
            Console.ReadKey();



        }

        public static void dosomething(int i)
        {
            //Console.WriteLine("i:{0}", i);
            Thread.Sleep(200);
            num++;
            //Interlocked.Increment(ref num1);
            //Thread.Sleep(1000);

        }

    }

    public class Business
    {
        public void dosomething(int i)
        {

            //Thread.Sleep(1000);

        }
    }

    public enum T1
    {

    }

    public class DAO
    {
        public int hotelid { get; set; }


    }

}
