using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MsmqManager
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @".\Private$\20151019Night";
           // //【1】创建队列
           //// QueueManger.CreateQueue(path, true);
           // QueueManger.CreateQueue(path);
            
           // //【2】向队列发送消息
           // Student std1 = new Student() { Name = "一条消息", Age = 25 };

           // QueueManger.SendMessage<Student>(std1, path);

           // //【3】接受消息（peek方式）
           // //Student ret = QueueManger.ReceiveMessageByPeek<Student>(path);
             
           // //Console.WriteLine("消息内容：姓名{0}，年龄：{1}", ret.Name, ret.Age);

           // //【4】receive方式

           // Student receiveMessage = QueueManger.ReceiveMessage<Student>(path);

           // Console.WriteLine("消息内容：姓名{0}，年龄：{1}", receiveMessage.Name, receiveMessage.Age);
            
            //【5】消息队列的事务性
             string path1 = @".\Private$\20151019NightTransaction";
             QueueManger.CreateQueue(path1, true);
             Student ts = new Student() { Name = "测试", Age = 26 };
             MessageQueueTransaction tran = new MessageQueueTransaction();
             try
             {
                 tran.Begin();
                 for (int i = 0; i < 5; i++)
                 {
                     Student item = new Student() { Name = "测测而成" + i, Age = 25 + i };
                     QueueManger.SendMessage<Student>(item, path1, tran);
                     //if (i == 4)
                     //    throw new Exception();
                 }
                 tran.Commit();
             }
             catch (Exception e)
             {
                 tran.Abort();//回滚
             }

            Console.Read();

        }
    }
}
