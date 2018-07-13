using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo
{
    public class AggregateExceptionArgs : EventArgs
    {
        public AggregateException aggregateException { get; set; }
    }
    
    public  class TaskExceptionHandleTest
    {
        public event EventHandler<AggregateExceptionArgs> AggregateExceptionCatched;

        public void MainMethod()
        {
            //调用wait 主线程等待
            //TestExceptionWait

            //发生异常时启用新线程处理异常，不阻塞主线程
            //ContinueWith();

            //调用wait 发生异常启用新线程处理异常，主线程等待异常处理，即处理完后回到主线程
            //HandExceptionInMainThread();

            //不调用wati 使用事件通知的方式 处理异常的仍然是子线程
            HandExceptionByEvent();

        }


        public void HandExceptionByEvent()
        {
            AggregateExceptionCatched += Program_AggregateExceptionCatched;

            Task t = new Task(() =>
            {
                try
                {
                    Console.WriteLine("B Thread ID:{0}", Thread.CurrentThread.ManagedThreadId);
                    throw new InvalidOperationException("task中发生异常1");
                }
                catch (Exception err)
                {
                    AggregateExceptionArgs errArgs = new AggregateExceptionArgs()
                    {
                        aggregateException = new AggregateException(err)
                    };

                    AggregateExceptionCatched(null, errArgs);
                }
            });
            t.Start();

            Console.WriteLine("主线程马上结束");
            Console.WriteLine("A Thread ID:{0}", Thread.CurrentThread.ManagedThreadId);
            Console.ReadKey();


        }

        static void Program_AggregateExceptionCatched(object sender,AggregateExceptionArgs args)
        {
            foreach (var item in args.aggregateException.InnerExceptions)
            {
                Console.WriteLine("C Thread ID:{0}", Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("异常类型：{0}{1}来自：{2}{3}异常类容：{4}", item.GetType(), Environment.NewLine, item.Source, Environment.NewLine, item.Message);
                //Console.WriteLine("异常类型：{0}{1}来自：{2}{3}异常类容：{4}", item.InnerException.GetType(), Environment.NewLine, item.InnerException.Source, Environment.NewLine, item.InnerException.Message);
            }
        }



        //Task没有提供将任务中的异常包装到主线程的接口。一个可行的办法是，仍旧使用类似Wait的方法来达到此目的。
        //在本建议一开始的代码中，我们对于主工作任务采用Wait的方法，这是不可取的。因为主工作任务也许会持续一段较长的时间，
        //那样会阻塞调用者，并让调用者觉得不能忍受。而本建议的第二段代码中，新任务只完成了处理异常，
        //这意味着新任务不会延续较长时间，所以，在这个新任务上维持等待对于调用者来说，是可以忍受的。
        //所以，我们可以采用这个方法将异常包装到主线程中：
        public void HandExceptionInMainThread()
        {
            Task t = new Task(() =>
            {
                Console.WriteLine("执行task");
                throw new InvalidOperationException("task中发生异常1");
            });
            t.Start();

            Task ttEnd = t.ContinueWith((task) =>
            {
                throw task.Exception;
            }, TaskContinuationOptions.OnlyOnFaulted);

            try
            {
                ttEnd.Wait();   //如果 t不抛异常，理论上ttEnd 不会执行，此处强行执行ttEnd.Wait()任然会导致ttEnd抛出 throw task.Exception异常，逻辑上很奇怪
            }
            catch (AggregateException err)
            {
                foreach (var item in err.InnerExceptions)
                {
                    if (item.InnerException != null)
                    {
                        Console.WriteLine("异常类型：{0}{1}来自：{2}{3}异常类容：{4}", item.InnerException.GetType(), Environment.NewLine, item.InnerException.Source, Environment.NewLine, item.InnerException.Message);
                    }
                    
                }
            }
            Console.WriteLine("主线程马上结束");
            Console.ReadKey();
        }




        //【2】创建延续任务，解决【1】中的阻塞主线程问题, 但是这个方法中异常处理没有回到主线程
        //更多时候我们还需要更进一步将异常处理封装到主线程。
        public void ContinueWith()
        {
            Task t = new Task(() => {
                Console.WriteLine("执行task");
                throw new Exception("task中发生异常1");
            });

            t.Start();

            //只有发生异常时才执行此task
            Task tend = t.ContinueWith((task) => {
                foreach (var e in task.Exception.InnerExceptions)
                {
                    Console.WriteLine("异常类型：{0}{1}来自：{2}{3}异常类容：{4}", e.GetType(), Environment.NewLine, e.Source, Environment.NewLine, e.Message);
                }
            },TaskContinuationOptions.OnlyOnFaulted);
            //TaskContinuationOptions.OnlyOnFaulted :指定只有在延续任务前面的任务引发了未处理异常的情况下才应安排延续任务。 此选项对多任务延续无效。

            Console.WriteLine("主线程即将结束");
            Console.ReadKey();

        }






        //【1】wait waitall waitany  result 等待主线程执行
        public void TestExceptionWait()
        {
            Task t = new Task(() =>
            {
                Console.WriteLine("执行task");
                throw new Exception("task中发生异常1");

            });

            t.Start();

            try
            {
                t.Wait();  //主线程等待task执行
            }
            catch (AggregateException e)
            {
                foreach (var ex in e.InnerExceptions)
                {
                    Console.WriteLine("异常类型：{0}{1}来自：{2}{3}异常类容：{4}", ex.GetType(), Environment.NewLine, ex.Source, Environment.NewLine, ex.Message);
                }
            }
            Console.WriteLine("主线程即将结束");
            Console.ReadKey();

        }




    }
}
