using Newtonsoft.Json;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new User()
            {
                name = "沈来",
                age = 26,
                pwd = "123456"
            };

            var redisClient = RedisInstance.GetRedisClient();
            /***********************************************************************/
            //【类型1-1】string字符串
            var city = redisClient.Get<string>("city");
            //【类型1-1】类对象
            //var userModel = redisClient.Set<User>("userkey", user, DateTime.Now.AddMilliseconds(50000));

            var getUserModel = redisClient.Get<User>("userkey");
            
            /***********************************************************************/
            //【Hash】哈希

            redisClient.SetEntryInHash("userHashID", "name", user.name);
            redisClient.SetEntryInHash("userHashID", "age", user.age.ToString());
            redisClient.SetEntryInHash("userHashID", "pwd", user.pwd);

            //整个list获取
            List<string> listKeys = redisClient.GetHashKeys("userHashID");
            List<string> listValues = redisClient.GetHashValues("userHashID");
            //hashValue获取
            string hashValue = redisClient.GetValueFromHash("userHashID", "name");

            /***********************************************************************/
            //【List 链表】
            //1.List作为（Stack）栈类型的使用:【先进后出】
            //通过Push与Pop操作Stack
            /*
            redisClient.PushItemToList("listId", "上海");
            redisClient.PushItemToList("listId", "北京");
            redisClient.PushItemToList("listId", "天津");
            redisClient.PushItemToList("listId", "广州");
            */
            int length = (int)redisClient.GetListCount("listId");
            //for (int i = 0; i < length; i++)
            //{
            //    Console.WriteLine(redisClient.PopItemFromList("listId"));
            //}
            
            //（2）下面我们来看看List作为（Queue）队列的使用：【先进先出】
            //通过DeQueue和EnQueue操作Queue
            //redisClient.EnqueueItemOnList("queueList", "武汉");
            //redisClient.EnqueueItemOnList("queueList", "成都");
            //redisClient.EnqueueItemOnList("queueList", "南京");
            //redisClient.EnqueueItemOnList("queueList", "沈阳");

            int lengthQ = (int)redisClient.GetListCount("queueList");
            //for (int i = 0; i < lengthQ; i++)
            //{
            //    Console.WriteLine(redisClient.DequeueItemFromList("queueList"));
            //}

            //【Set 集合】
            //Set是string类型的无序集合,set是可以自动排重

            //redisClient.AddItemToSet("a3", "ddd");
            //redisClient.AddItemToSet("a3", "ccc");
            //redisClient.AddItemToSet("a3", "tttt");
            //redisClient.AddItemToSet("a3", "sssh");
            //redisClient.AddItemToSet("a3", "hhhh");
            //redisClient.AddItemToSet("a4", "hhhh");
            //redisClient.AddItemToSet("a4", "h777");

            Console.WriteLine("***********求A3集合************");
            HashSet<string> hashSet = redisClient.GetAllItemsFromSet("a3");
            foreach (var value in hashSet)
            {
                Console.WriteLine(value);
            }

            Console.WriteLine("***************求并集*************");
            hashSet.Clear();
            hashSet = redisClient.GetUnionFromSets(new string[] { "a3", "a4" });
            foreach (string value in hashSet)
            {
                Console.WriteLine(value);
            }
            Console.WriteLine("****************求交集******************");
            hashSet.Clear();
            hashSet = redisClient.GetIntersectFromSets("a3", "a4");

            Console.WriteLine("*******************求差集*********************");
            hashSet.Clear();
            hashSet = redisClient.GetDifferencesFromSet("a3", "a4");

            

            //【Sorted Set 有序集合】
            redisClient.AddItemToSortedSet("a5", "ffff",1);
            redisClient.AddItemToSortedSet("a5", "bbbb",2);
            redisClient.AddItemToSortedSet("a5", "gggg",3);
            redisClient.AddItemToSortedSet("a5", "cccc",4);
            redisClient.AddItemToSortedSet("a5", "waaa",5);
            List<string> list = redisClient.GetAllItemsFromSortedSet("a5");
            foreach (string str in list)
            {
                Console.WriteLine(str);
            }
                Console.WriteLine(city);
            Console.WriteLine(JsonConvert.SerializeObject(getUserModel));
            Console.ReadKey();

        }
    }

    public class User
    {
        public string name { get; set; }
        public int age { get; set; }
        public string pwd { get; set; }
    }

    
}
