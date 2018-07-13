using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo
{
    public class AwaitTest
    {
        public async static void Invoke()
        {
            int wokerT;
            int ioT;
            ThreadPool.GetAvailableThreads(out wokerT, out ioT);
            Console.WriteLine("Invoke中：工作者线程数：{0}。I/O线程数：{1} ", wokerT, ioT);
            Console.WriteLine("Invoke中： ThreadId:{0}", Thread.CurrentThread.ManagedThreadId);
            var res = FunV2();



        }

        public static async Task<string> FunV2()
        {
            #region 测试代码
            int workthreadnumber;
            int iothreadnumber;

            // 获得线程池中可用的线程，把获得的可用工作者线程数量赋给workthreadnumber变量
            // 获得的可用I/O线程数量给iothreadnumber变量
            ThreadPool.GetAvailableThreads(out workthreadnumber, out iothreadnumber);
            Console.WriteLine("Task前：工作者线程数：{0}。I/O线程数：{1}", workthreadnumber, iothreadnumber);
            Console.WriteLine("Task.Run前： ThreadId:{0}", Thread.CurrentThread.ManagedThreadId);
            #endregion

            #region 比较 Task.Run 与 await Task.Run
            //Task.Run(() =>
            //{
            //    ////Test() 中执行var res = FunV2();  同步调用，此处可能会启动新线程执行（由OS决定），主线程可继续执行
            //    Console.WriteLine("Task.Run中：.ThreadId:{0}", Thread.CurrentThread.ManagedThreadId);      //启动新线程  
            //});

            //单次挂起，方法内部需要等待； 多次调用FunV2，异步执行，互相之间不需等待
            await Task.Run(() =>
            {
                //Thread.Sleep(1000);
                ////Test() 中执行var res = FunV2();  同步调用，此处可能会启动新线程执行（由OS决定），主线程可继续执行
                #region 测试代码
                int workthreadnumberIng;
                int iothreadnumberIng;

                // 获得线程池中可用的线程，把获得的可用工作者线程数量赋给workthreadnumber变量
                // 获得的可用I/O线程数量给iothreadnumber变量
                ThreadPool.GetAvailableThreads(out workthreadnumberIng, out iothreadnumberIng);
                Console.WriteLine("await Task中：工作者线程数：{0}。I/O线程数：{1}", workthreadnumberIng, iothreadnumberIng);
                Console.WriteLine("Task.Run中： ThreadId:{0}", Thread.CurrentThread.ManagedThreadId); //主线程
                #endregion
                Console.WriteLine("Task.Run中：.ThreadId:{0}", Thread.CurrentThread.ManagedThreadId);      //启动新线程  
            });
            #endregion


            #region 测试代码
            int workthreadnumberAfter;
            int iothreadnumberAfter;

            // 获得线程池中可用的线程，把获得的可用工作者线程数量赋给workthreadnumber变量
            // 获得的可用I/O线程数量给iothreadnumber变量
            ThreadPool.GetAvailableThreads(out workthreadnumberAfter, out iothreadnumberAfter);
            Console.WriteLine("await Task后：工作者线程数：{0}。I/O线程数：{1}", workthreadnumberAfter, iothreadnumberAfter);
            Console.WriteLine("Task.Run后： ThreadId:{0}", Thread.CurrentThread.ManagedThreadId); //主线程
            #endregion

            Thread.Sleep(1000);
            return "ThreadId:" + Thread.CurrentThread.ManagedThreadId.ToString();
        } 
    }
}
