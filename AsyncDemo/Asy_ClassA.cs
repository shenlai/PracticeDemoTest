using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo
{
    public class Asy_ClassA
    {
        public async Task<string>  AsyncSleep()
        {
            Console.WriteLine("step2,线程ID:{0}", System.Threading.Thread.CurrentThread.ManagedThreadId);

            //await关键字表示“等待”Task.Run传入的逻辑执行完毕，此时(等待时)AsyncSleep的调用方能继续往下执行（准确地说，是当前线程不会被阻塞）
            //Task.Run将开辟一个新线程执行指定逻辑
            await Task.Run(() => Sleep(10));
            
            Console.WriteLine("step4,线程ID:{0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
            return "123";
        }

        private void Sleep(int second)
        {
            Console.WriteLine("step3,线程ID:{0}", System.Threading.Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(second * 1000);
        }

    }
}
