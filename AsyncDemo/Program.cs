using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo
{
    //http://stackoverflow.com/questions/21033150/any-difference-between-await-task-run-return-and-return-task-run
    class Program
    {
        static SemaphoreSlim _sem = new SemaphoreSlim(3);    // 我们限制能同时访问的线程数量是3

        static Stopwatch stop = new Stopwatch();
        static Stopwatch stop1 = new Stopwatch();
        
        static void Main(string[] args)
        {
            #region 信号量
            //for (int i = 1; i <= 5; i++) new Thread(Enter).Start(i);
            #endregion

            #region await
            //Test();
            //Console.WriteLine("1.ThreadId:{0}", Thread.CurrentThread.ManagedThreadId);

            #endregion

            //多线程+同步   多线程+异步
            //Methods.Invoke();


            //await 线程池查看
            //AwaitTest.Invoke();

            //Task测试
            //System.Diagnostics.Debug.WriteLine("Main1: ThreadId : " + Thread.CurrentThread.ManagedThreadId);
            //new TaskTest().DisplayValue();// 不需要等待
            //System.Diagnostics.Debug.WriteLine("Main2: ThreadId : " + Thread.CurrentThread.ManagedThreadId);
            //System.Diagnostics.Debug.WriteLine("MyClass() End.");  



            //TaskExceptionHandleTest.cs

            //new TaskExceptionHandleTest().MainMethod();
            Asy_ClassA asy = new Asy_ClassA();
            var str = asy.AsyncSleep();    //main 方法为非异步方法，不能在这里使用 await asy.AsyncSleep();  此处不阻塞主线程
                                           //str.Result  主线程等待
            Console.WriteLine("没有等待，先执行");
            Console.ReadLine();
        }

        public static async Task Test()
        {
            stop1.Start();
            stop.Start();
            Console.WriteLine("2.ThreadId:{0}", Thread.CurrentThread.ManagedThreadId);
            //await 不会创建新的线程，Task中方法 会创建新的线程
            //await Fun(); //方法返回值前面加 async 之后，方法里面就可以用await了  (等待Fun执行完成:同步执行)  
            var res = FunV2();

            var t1 = stop.ElapsedMilliseconds;
            stop.Restart();


            Thread.Sleep(500);
            var t2 = stop.ElapsedMilliseconds;
            stop.Restart();
            Console.WriteLine("FunV2之前执行？");
            var t3 = stop.ElapsedMilliseconds;
            stop.Restart();
            Console.WriteLine("FunV2之前执行结果：{0}", res.Result);  //等待
            var t4 = stop.ElapsedMilliseconds;
            var t5 = stop1.ElapsedMilliseconds;
            stop.Restart();
           // Console.ReadLine();
        }

        /// <summary>
        /// await Fun();
        /// await关键字标记的异步任务Fun()，这个异步任务必须是以Task或者Task<TResult>作为返回值的，
        /// 而异步任务执行完成时实际返回的类型是void或者TResult
        /// </summary>
        /// <returns></returns>
        public static async Task Fun()
        {
            Thread.Sleep(1000);
            //await Task.Delay(1000);  // 返回值前面加 async 之后，方法里面就可以用await了
            Console.WriteLine("3.ThreadId:{0}", Thread.CurrentThread.ManagedThreadId);
        }

        public static async Task<string> FunV2()
        {
            //Thread.Sleep(1000); //由于没有创建新线程，此处仍然由主线程执行。
            //await Task.Delay(1000);  // 返回值前面加 async 之后，方法里面就可以用await了
            //Task.Run(() =>
            //{
            //    //Test() 中执行var res = FunV2();  同步调用，等待执行完成，相当于单线程同步执行
            //    Console.WriteLine("3.ThreadId:{0}", Thread.CurrentThread.ManagedThreadId);   
            //});

            await Task.Run(() =>
            {
                ////Test() 中执行var res = FunV2();  同步调用，此处可能会启动新线程执行（由OS决定），主线程可继续执行
                Console.WriteLine("3.ThreadId:{0}", Thread.CurrentThread.ManagedThreadId);
            });



            Thread.Sleep(1000);
            return "ThreadId:" + Thread.CurrentThread.ManagedThreadId.ToString();
        }



        #region
        static void Enter(object id)
        {
            Console.WriteLine(id + " 开始排队...");
            _sem.Wait();
            Console.WriteLine(id + " 开始执行！");
            Thread.Sleep(1000 * (int)id);
            Console.WriteLine(id + " 执行完毕，离开！不释放信号量");
            //_sem.Release();
        }

        #endregion


        #region async await

       


       


        #endregion
    }
}
