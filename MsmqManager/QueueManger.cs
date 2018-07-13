using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace MsmqManager
{
    public class QueueManger
    {
        /// <summary>
        /// 创建队列
        /// </summary>
        /// <param name="path">队列路径</param>
        /// <param name="transactional">是否是事务队列</param>
        public static void CreateQueue(string path, bool transactional = false)
        {
            try
            {
                if(!MessageQueue.Exists(path))
                {
                    MessageQueue.Create(path,transactional);
                    Console.WriteLine("队列创建成功");
                }else
                { Console.WriteLine("队列已存在"); }

            }
            catch(MessageQueueException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// 删除队列
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteQueue(string path)
        {
            try
            {
                if (MessageQueue.Exists(path))
                {
                    MessageQueue.Delete(path);
                    Console.WriteLine("{0}队列已删除", path);
                }
                else
                {
                    Console.WriteLine("{0}队列不存在", path);
                }

            }
            catch (MessageQueueException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T">消息内容数据类型</typeparam>
        /// <param name="body">消息内容</param>
        /// <param name="path">队列名称</param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static bool SendMessage<T>(T body, string path, MessageQueueTransaction tran = null)
        {
            try
            {
                 //连接到本地的队列
                MessageQueue myQueue = new MessageQueue(path);
                Message myMessage = new Message();
                myMessage.Body = body;
                //指定格式化器
                myMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
                //发送消息到队列
                if (tran == null)
                {
                    myQueue.Send(myMessage);
                }
                else
                {
                    myQueue.Send(myMessage, tran);
                }
                Console.WriteLine("消息已成功发送到{0}队列",path);
                return true;
            }
            catch(ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }

        /// <summary>
        /// 接受消息(消息出队列)
        /// </summary>
        /// <typeparam name="T">消息内容数据类型</typeparam>
        /// <param name="path">消息名称</param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static T ReceiveMessage<T>(string path, MessageQueueTransaction tran = null)
        {
            //连接到本地队列
            MessageQueue myQueue = new MessageQueue(path);
            myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
            try
            {
                //从队列中接受消息
                Message myMessage = tran == null ? myQueue.Receive() : myQueue.Receive(tran);
                return (T)myMessage.Body;
            }
            catch (MessageQueueException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (InvalidCastException e)
            {
                //无效类型转换或显示转换引发的异常
                Console.WriteLine(e.Message);
            }
            return default(T);
        }

        /// <summary>
        /// 查看消息（消息不出队列）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="path">名称</param>
        /// <returns></returns>
        public static T ReceiveMessageByPeek<T>(string path)
        {
            //连接队列
            MessageQueue queue = new MessageQueue(path);
            //设置格式化器
            queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
            try
            {
                //从队列中接受消息
                Message message = queue.Peek();
                return (T)message.Body;

            }
            catch (InvalidCastException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (MessageQueueException e)
            {
                Console.WriteLine(e.Message);
            }
            return default(T);
        }

        /// <summary>
        /// 获取队列中所有消息
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="path">队列名</param>
        /// <returns></returns>
        public static List<T> ReceiveAllMessage<T>(string path)
        {
            //连接本地队列
            MessageQueue queue = new MessageQueue(path);
            //设置队列格式化器
            queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
            List<T> ret = new List<T>();
            try 
            {
                Message[] messages = queue.GetAllMessages();
                messages.ToList().ForEach((message)=>{ret.Add((T)message.Body);});
                return ret;
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

    }
}
