using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo
{
    public class Methods
    {

        public static void Invoke()
        {
            #region Task.Run()
            //Stopwatch stop = new Stopwatch();
            //stop.Start();
            //for(int x=0;x<10;x++)
            //{
            //    //Random ra = new Random(1000);
            //    //int num = ra.Next(1, 1000);
            //    int num = (10 - x) * 100;
            //    WriteText(x, num);  ////我们这里没有用 await,所以下面的代码可以继续执行
            //     /*await 实质是在调用awaitable对象的GetResult方法,实现多线程异步，创建新的线程 Task.Run(), 不等待 async 修饰的方法（异步函数）*/
            //}
            //var t1 = stop.ElapsedMilliseconds;
            //stop.Stop();
            #endregion

            #region await async
            Stopwatch stop = new Stopwatch();
            stop.Start();
            for (int x = 0; x < 10; x++)
            {
                //Random ra = new Random(1000);
                //int num = ra.Next(1, 1000);
                int num = (10 - x) * 100;
                //WriteTextAsync(x, num);  //此路调用下去 是单线程同步执行，
                WriteText(x, num);
                //await 实质是在调用awaitable对象的GetResult方法,实现多线程异步，创建新的线程 Task.Run(), 不等待 async 修饰的方法（异步函数）
            }
            var t1 = stop.ElapsedMilliseconds;
            stop.Stop();
            #endregion
        }

        #region Task.Run()
        public static async Task WriteText(int x,int num)
        {
            await Task.Run(() => { Write(x,num); });
            //Task.Run(() => { Write(x, num); });      //此处两种写法都是异步执行
        }

        public static void Write(int x,int num)
        {
            
            Thread.Sleep(num);
            FileStream fs = new FileStream(@"F:\Project\PracticeDemo\AsyncDemo\IO.txt", FileMode.Append, FileAccess.Write, FileShare.Write, 100, true);
            byte[] writebytes = new byte[1000000];
            //这里要注意，如果写入的字符串很小，则.Net会使用辅助线程写，因为这样比较快
            string writeContens = "尝试将内容以异XXXXXXXXxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx步方式写到text文件中\r\n";
            string content = string.Format("第{0}次调用，延迟{1}毫秒执行，当前线程ID:{2}，{3}", x, num, Thread.CurrentThread.ManagedThreadId.ToString(), writeContens);
            writebytes = Encoding.UTF8.GetBytes(content);
            //调用异步写入方法将信息写入到文件
            //fs.BeginWrite(writebytes, 0, writebytes.Length, new AsyncCallback(EndWriteCallback), fs);
            
            //fs.WriteAsync
            fs.Write(writebytes, 0, writebytes.Length);
        }

        #endregion

        #region await async
        public static async Task WriteTextAsync(int x, int num)
        {
            //Console.WriteLine(x + ":当前线程：" + Thread.CurrentThread.ManagedThreadId);
            /*******************单线程，不同 WriteTextAsync之间调用需要等待*************************/
            await WriteAsync(x, num); 
        
        }

        public static async Task WriteAsync(int x, int num)
        {

            Thread.Sleep(num);
            FileStream fs = new FileStream(@"F:\Project\PracticeDemo\AsyncDemo\IO.txt", FileMode.Append, FileAccess.Write, FileShare.Write, 100, true);
            byte[] writebytes = new byte[1000000];
            //这里要注意，如果写入的字符串很小，则.Net会使用辅助线程写，因为这样比较快
            string writeContens = "尝试将内容以异XXXXXXXXxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx步方式写到text文件中\r\n";
            string content = string.Format("第{0}次调用，延迟{1}毫秒执行，当前线程ID:{2}，{3}", x, num, Thread.CurrentThread.ManagedThreadId.ToString(), writeContens);
            writebytes = Encoding.UTF8.GetBytes(content);
            //调用异步写入方法将信息写入到文件
            //fs.BeginWrite(writebytes, 0, writebytes.Length, new AsyncCallback(EndWriteCallback), fs);
            fs.BeginWrite(writebytes, 0, writebytes.Length, null, fs);
            //fs.WriteAsync
            //fs.Write(writebytes, 0, writebytes.Length);
        }

        #endregion
    }
}
