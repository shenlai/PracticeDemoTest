using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace MsmqServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string path= @".\Private$\shenlaiTestProgram";
            //创建消息队列
            if (!MessageQueue.Exists(path))
            {
                using (MessageQueue mq = MessageQueue.Create(path))
                {
                    mq.Label = "shenlaiTestProgramName";
                    Console.WriteLine("已创建队列");
                    Console.WriteLine("队列路径：{0}", mq.Path);
                    Console.WriteLine("队列名：{0}", mq.QueueName);
                    mq.Send("MSmq Test", "测试试试");
                }

            }

            //访问
            if (MessageQueue.Exists(path))
            {
                //获得消息队列
                MessageQueue mq = new MessageQueue(path);
                //mq.Send("Sending MSMQ private message" + DateTime.Now.ToLongDateString(), "测试时2");
                mq.Send("Sending MSMQ private message", "测试时2");
                Console.WriteLine("Private Message is sent to {0}", mq.Path);
            }

            Console.Read();
        }
    }
}
