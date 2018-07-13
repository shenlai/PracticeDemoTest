using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo
{
    public class TaskTest
    {

        

        //外部调用 DisplayValue时不阻塞
        //控制台：
        //         MyClass() End.
        //         Value is : 456.408491620298
        public async void DisplayValue()
        {
            Stopwatch stop = new Stopwatch();
            stop.Start();

            System.Diagnostics.Debug.WriteLine("DisplayValue1: ThreadId : " + Thread.CurrentThread.ManagedThreadId);
            /*********************************************************************************************************/
            var result = await GetValueAsync(1234.5, 1.01);//此处需要等待？因为单线程， await挂起并不会挂起主线程（挂起过程中，Main方法中new TaskTest().DisplayValue()以后的代码任然会执行）
            /*********************************************************************************************************/
            //这之后的所有代码都会被封装成委托，在GetValueAsync任务完成时调用  
            var time = stop.ElapsedMilliseconds;
            System.Diagnostics.Debug.WriteLine("DisplayValue2: ThreadId : " + Thread.CurrentThread.ManagedThreadId);
            System.Diagnostics.Debug.WriteLine("Value is : " + result);
        }

        public Task<double> GetValueAsync(double num1, double num2)
        {
            Thread.Sleep(1000);
            System.Diagnostics.Debug.WriteLine("GetValueAsync: ThreadId : " + Thread.CurrentThread.ManagedThreadId);
            //以上两步 在 Main方法中：
            /*System.Diagnostics.Debug.WriteLine("Main2: ThreadId : " + Thread.CurrentThread.ManagedThreadId);
             System.Diagnostics.Debug.WriteLine("MyClass() End.");  
             */ //之前执行 
            
            return Task.Run(() =>
            {

                System.Diagnostics.Debug.WriteLine("Task.Run: ThreadId : " + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(10000);
                for (int i = 0; i < 100; i++)
                {
                    num1 = num1 / num2;
                }
                return num1;
            });
        }



    }
}
