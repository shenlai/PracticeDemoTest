using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
namespace TPLDataFlow
{
    public class Program
    {
        #region BufferBlock
        /* BufferBlock
       
        static void Main(string[] args)
        {
            var p = Task.Factory.StartNew(Producer);
            var c = Task.Factory.StartNew(Consumer);
            Task.WaitAll(p, c);
        }


        private static BufferBlock<int> m_buffer = new BufferBlock<int>();
        //Producer
        private static void Producer()
        {
            while (true)
            {
                int item = new Random().Next();
                m_buffer.Post(item);
            }
        }

        //Consumer
        private static void Consumer()
        {
            while (true)
            {
                int item = m_buffer.Receive();
                Process(item);
            }
        }

        public static void Process(int index)
        {
            Console.WriteLine("随机数：{0}",index);
        }

        
        */
        #endregion

        #region ActionBlock

        #region 单线程 同步处理
        //如果使用Action(T)委托，那说明每一个数据的处理完成需要等待这个委托方法结束
        //单线程 同步处理
        //主线程在往ActionBlock中Post数据以后马上返回，具体数据的处理是另外一个线程来做的。数据是异步处理的，但处理本身是同步的(因为是单线程程序)
        //public ActionBlock<int> abSync = new ActionBlock<int>((i) =>
        //{
        //    //Thread.Sleep(1000);
        //    Console.WriteLine(i + "  THreadId:" + Thread.CurrentThread.ManagedThreadId + "  Execute Time:" + DateTime.Now);
        //});

        //public void TestSync()
        //{
        //    for (int i = 0; i < 200; i++)
        //    {
        //        abSync.Post(i);
        //    }
        //    Console.WriteLine("Post Finished");
        //}
        //static void Main(string[] args)
        //{
        //    new Program().TestSync();
        //    Console.ReadLine();
        //}
        #endregion

        #region 多线程 同步处理
        //Func<TInput, Task> action
        //abSync2  由不同线程来处理，数据是异步处理的，但处理本身是同步的（因为:ActionBlock会FIFO的处理每一个数据，而且一次只能处理一个数据，一个处理完了再处理第二个）
        //public ActionBlock<int> abSync2 = new ActionBlock<int>(async (i) => 
        //{
        //    await Task.Delay(1000);
        //    Console.WriteLine(i + "  THreadId:" + Thread.CurrentThread.ManagedThreadId + "  Execute Time:" + DateTime.Now);
        //});
        //public void TestSync()
        //{
        //    for (int i = 0; i < 20; i++)
        //    {
        //        abSync2.Post(i);
        //        Thread.Sleep(500);
        //    }
        //    Console.WriteLine("Post Finished");
        //}
        //static void Main(string[] args)
        //{
        //    new Program().TestSync();
        //    Console.ReadLine();
        //}
        #endregion

        #region 异步 并发
        //定义MaxDegreeOfParallelism =3 一次性并发处理3个消息（不同线程）
        //public ActionBlock<int> abAsync  = new ActionBlock<int>((i) =>
        //{
        //    Thread.Sleep(1000);
        //    Console.WriteLine(i + "  THreadId:" + Thread.CurrentThread.ManagedThreadId + "  Execute Time:" + DateTime.Now);
        //}, new ExecutionDataflowBlockOptions() { MaxDegreeOfParallelism = 3 });
        //public void TestASync()
        //{
        //    for (int i = 0; i < 20; i++)
        //    {
        //        abAsync .Post(i);
        //    }
        //    Console.WriteLine("Post Finished");
        //}
        //static void Main(string[] args)
        //{
        //    new Program().TestASync();
        //    Console.ReadLine();
        //}
        #endregion

        #region ActionBlock也有自己的生命周期
        //public ActionBlock<int> abASync = new ActionBlock<int>((i) =>
        //{
        //    Thread.Sleep(1000);
        //    Console.WriteLine(i + "  THreadId:" + Thread.CurrentThread.ManagedThreadId + "  Execute Time:" + DateTime.Now);
        //}, new ExecutionDataflowBlockOptions() { MaxDegreeOfParallelism = 3 });
        //public void TestAsync()
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        abASync.Post(i);
        //    }
        //    abASync.Complete(); //调用Complete方法是让ActionBlock停止接收数据，
        //    Console.WriteLine("Post finished");
        //    abASync.Post(10);//已经停止接受数据，post无效
        //    abASync.Completion.Wait();//等待abASync完成所有任务处理
        //    Console.WriteLine("Process finished");
        //}

        //static void Main(string[] args)
        //{
        //    new Program().TestAsync();
        //    Console.ReadLine();
        //}

        #endregion



        #endregion

        #region TransformBlock (重载类似ActionBlock)
        //在TransformBlock内部维护了2个Queue，一个InputQueue，一个OutputQueue
        //最终我们可以通过Receive方法来阻塞的一个一个获取OutputQueue中的数据。
        //public TransformBlock<string, string> tbUrl = new TransformBlock<string, string>((url) =>
        // {
        //     WebClient client = new WebClient();
        //     return client.DownloadString(new Uri(url));
        // });

        //public void TestDownloadHtml()
        //{
        //    tbUrl.Post("https://www.baidu.com");
        //    tbUrl.Post("http://www.tujia.com/");

        //    string baiduHtml = tbUrl.Receive();
        //    string tujiaHtml = tbUrl.Receive();
        //    //Post操作和Receive操作可以在不同的线程中进行
        //}

        //static void Main()
        //{
        //    new Program().TestDownloadHtml();
        //    Console.ReadLine();
        //}

        #endregion

        #region TransformManyBlock ????
        //TransformManyBlock和TransformBlock非常类似，关键的不同点是，TransformBlock对应于一个输入数据只有一个输出数据，而TransformManyBlock可以有多个，及可以从InputQueue中取一个数据出来，然后放多个数据放入到OutputQueue中。

        //TransformManyBlock<int, int> tmb = new TransformManyBlock<int, int>((i) =>
        //{
        //    return new int[] { i, i + 1 };
        //});

        //ActionBlock<int> ab = new ActionBlock<int>((i) => { Console.WriteLine(i); });

        //public void TestSync()
        //{
        //    tmb.LinkTo(ab);// 这个做什么？很重要!!
        //    for(int i=0;i<4;i++)
        //    {
        //        tmb.Post(i);
        //        //var res = tmb.Receive();
        //        //int x = 0;
        //    }
        //    Console.WriteLine("Finished post");
        //}

        //static void Main()
        //{
        //    new Program().TestSync();
        //    Console.ReadLine();
        //}

        #endregion

        #region BroadcastBlock
        ////让所有和它相联的目标Block都收到数据的副本,每个关联的Block由独立的线程处理

        //BroadcastBlock<int> bb = new BroadcastBlock<int>((i) => { return i; });
        //ActionBlock<int> displayBlock = new ActionBlock<int>((i) => { Console.WriteLine("Displayed " + i + "  THreadId:" + Thread.CurrentThread.ManagedThreadId + "  Execute Time:" + DateTime.Now);
        //});

        //ActionBlock<int> saveBlock = new ActionBlock<int>((i) => { Console.WriteLine("Saved " + i + "  THreadId:" + Thread.CurrentThread.ManagedThreadId + "  Execute Time:" + DateTime.Now);
        //});
        //ActionBlock<int> sendBlock = new ActionBlock<int>((i) => { Console.WriteLine("Sent " + i + "  THreadId:" + Thread.CurrentThread.ManagedThreadId + "  Execute Time:" + DateTime.Now);
        //});

        //public void TestSync()
        //{
        //    bb.LinkTo(displayBlock);
        //    bb.LinkTo(saveBlock);
        //    bb.LinkTo(sendBlock);

        //    for (int i = 0; i < 4; i++)
        //    {
        //        bb.Post(i);
        //    }
        //    Console.WriteLine("Post Finished");
        //}
        //static void Main()
        //{
        //    new Program().TestSync();
        //    Console.ReadLine();
        //}



        #endregion

        #region WriteOnceBlock
        //它最多只能存储一个数据，一旦这个数据被发送出去以后，这个数据还是会留在Block中，但不会被删除或被新来的数据替换，同样所有的接收者都会收到这个数据的备份
        //WriteOnceBlock<int> wob = new WriteOnceBlock<int>((i) => { return i; });
        //ActionBlock<int> displayBlock = new ActionBlock<int>((i) =>
        //{
        //    Console.WriteLine("Displayed " + i + "  THreadId:" + Thread.CurrentThread.ManagedThreadId + "  Execute Time:" + DateTime.Now);
        //});

        //ActionBlock<int> saveBlock = new ActionBlock<int>((i) =>
        //{
        //    Console.WriteLine("Saved " + i + "  THreadId:" + Thread.CurrentThread.ManagedThreadId + "  Execute Time:" + DateTime.Now);
        //});
        //ActionBlock<int> sendBlock = new ActionBlock<int>((i) =>
        //{
        //    Console.WriteLine("Sent " + i + "  THreadId:" + Thread.CurrentThread.ManagedThreadId + "  Execute Time:" + DateTime.Now);
        //});

        //public void TestSync()
        //{
        //    wob.LinkTo(displayBlock);
        //    wob.LinkTo(saveBlock);
        //    wob.LinkTo(sendBlock);
        //    for (int i = 0; i < 4; i++)
        //    {
        //        wob.Post(i);
        //    }
        //    Console.WriteLine("Post finished");
        //}
        //static void Main()
        //{
        //    new Program().TestSync();
        //    Console.ReadLine();
        //}
        #endregion

        #region BatchBlock ************
        //https://docs.microsoft.com/zh-cn/dotnet/standard/parallel-programming/walkthrough-using-batchblock-and-batchedjoinblock-to-improve-efficiency
        //提供了能够把多个单个的数据组合起来处理的功能

        BatchBlock<int> bab = new BatchBlock<int>(3);
        ActionBlock<int[]> ab = new ActionBlock<int[]>((i) =>
        {
            string s = string.Empty;
            foreach (var m in i)
            {
                Thread.Sleep(1000);
                s += m + " THreadId:" + Thread.CurrentThread.ManagedThreadId + "T:" + DateTime.Now +" ";
            }
            Console.WriteLine(s);
            Console.WriteLine();
        },new ExecutionDataflowBlockOptions() { MaxDegreeOfParallelism = 2 });

        public void TestBatchBlock()
        {
            bab.LinkTo(ab);
            for (int i = 0; i < 10; i++)
            {
                bab.Post(i);
            }
            //bab.Complete();//告知Post数据结束
            //bab.TriggerBatch();
            //bab.Completion.Wait();
            Console.WriteLine("Finished post");
        }

        static void Main()
        {
            Console.WriteLine("THreadId: " + Thread.CurrentThread.ManagedThreadId + "  Execute Time: " + DateTime.Now);
            new Program().TestBatchBlock();
            Console.WriteLine("mainthread do otherthing");
            Console.ReadLine();
        }

        #endregion

        #region JoinBlock
        //JoinBlock需要两个或两个以上的Source Block相连接的
        //只有当两个ITargetBlock都收到各自的数据后，才会放到JoinBlock的OutputQueue中，输出

        //JoinBlock<int, string> jb = new JoinBlock<int, string>();
        //ActionBlock<Tuple<int, string>> ab = new ActionBlock<Tuple<int, string>>((i) =>
        //{
        //    Console.WriteLine(i.Item1 + " " + i.Item2);
        //});

        //public void TestSync()
        //{
        //    jb.LinkTo(ab);

        //    for (int i = 0; i < 5; i++)  //
        //    {
        //        jb.Target1.Post(i);
        //    }

        //    for (int i = 4; i > 0; i--)   //注意，此时 两个block 数据量不一致
        //    {
        //        Thread.Sleep(1000);
        //        jb.Target2.Post(i.ToString());
        //    }

        //    Console.WriteLine("Finished post");
        //}
        //static void Main()
        //{
        //    new Program().TestSync();
        //    Console.ReadLine();
        //}


        #endregion


        #region BatchJoinBlock 

        //BatchedJoinBlock<int, string> bjb = new BatchedJoinBlock<int, string>(3);
        //ActionBlock<Tuple<IList<int>, IList<string>>> ab = new ActionBlock<Tuple<IList<int>, IList<string>>>((i) =>
        //{
        //    Console.WriteLine("-----------------------");
        //    foreach (int m in i.Item1)
        //    {
        //        Console.WriteLine("m:" + m);
        //    }
        //    foreach (var s in i.Item2)
        //    {
        //        Console.WriteLine("s:" + s);
        //    }
        //});

        //public void TestSync()
        //{
        //    bjb.LinkTo(ab);
        //    for (int i = 0; i < 5; i++)
        //    {
        //        bjb.Target1.Post(i);
        //    }
        //    for (int i = 5; i > 0; i--)
        //    {
        //        bjb.Target2.Post(i.ToString());
        //    }
        //    bjb.Complete();//告知post结束，最后数据不足BatchSize的，也会打包处理
        //    //如果不调用，最后不足BatchSize的数据不会处理
        //    Console.WriteLine("Finished post");
        //}


        //static void Main()
        //{
        //    new Program().TestSync();
        //    Console.ReadLine();
        //}
        #endregion


        public ActionBlock<string> abSync = new ActionBlock<string>((i) =>
        {
            Thread.Sleep(1000);
            Console.WriteLine(i + "  THreadId:" + Thread.CurrentThread.ManagedThreadId + "  Execute Time:" + DateTime.Now);
        });

        public TransformBlock<int, string> tbSync = new TransformBlock<int, string>((i) => {
            i = i * 2;
            return i.ToString();
        });
        public void TestSync()
        {
            tbSync.LinkTo(abSync); //将数据丢到abSync 由它来处理
            for (int i = 0; i < 10; i++)
            {
                tbSync.Post(i);
            }
            tbSync.Complete();
            Console.WriteLine("Post finished");

            tbSync.Completion.Wait();
            Console.WriteLine("TransformBlock process finished");
        }
        
        //static void Main()
        //{
        //    new Program().TestSync();
        //    Console.ReadLine();
        //}






    }
}
