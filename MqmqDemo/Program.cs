using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace MqmqDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @".\Private$\shenlaiTestProgram";
            if(MessageQueue.Exists(path))
            {
                //MessageQueue x = new MessageQueue(path);    //实例化一个队列对象
                //MessageQueue y = MessageQueue.Create(path); //Server上新建队列
                //创建队列对象
                using (MessageQueue mq = new MessageQueue(path))
                {
                    //设置队列格式化器
                    mq.Formatter = new XmlMessageFormatter(new string[] { "System.String" });
                    foreach (Message message in mq.GetAllMessages())
                    {
                        Console.WriteLine("消息内容：{0}", message.Body);
                    }
                    Message ms = mq.Receive();
                    Console.WriteLine("接收到的内容：{0}", ms.Body);
                }
            }

            Console.ReadLine();
        }
    }
}
