using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace PracticeDemo
{
    internal delegate Object TwoInt32s(Int32 n1, Int32 n2);

    class Program
    {

        /*
        * 委托引入
        * 委托定义 申明 实例化 调用
        * 委托链
        * 泛型委托
        * 
        * 委托与反射
        * 
        * 委托本质
         * 委托的协变和逆变
        */

        #region 委托引入


        //通知枚举
        //public enum Notice
        //{
        //    Message,
        //    Emial,
        //    QQ
        //}
        //static void Main(string[] args)
        //{
        //    SendNotice("短信通知:*****", Notice.Message);
        //    SendNotice("邮件通知:*****", Notice.Emial);
        //    Console.ReadLine();
        //}

        //public static void SendNotice(string content,Notice notice)
        //{
        //    switch (notice)
        //    {
        //        case Notice.Message:
        //            SendMessage(content);break;
        //        case Notice.Emial:
        //            SendEmial(content);break;
        //        case Notice.QQ:
        //            SendQQ(content);break;
        //    }
        //}


        ////短信通知
        //public static void SendMessage(string content)
        //{
        //    Console.WriteLine(content);
        //}

        ////邮件通知
        //public static void SendEmial(string content)
        //{
        //    Console.WriteLine(content);
        //}

        ////QQ通知
        //public static void SendQQ(string content)
        //{
        //    Console.WriteLine(content);
        //}

        #endregion

        #region 委托

        //委托声明：适用于一个无返回值，string类型入参的方法
        //public delegate void DelegateSample(string content);
        //static void Main(string[] args)
        //{
        //    DelegateSample sample = new DelegateSample(SendQQ);
        //    sample += SendEmial;
        //    sample += SendMessage;


        //    foreach (DelegateSample item in sample.GetInvocationList())
        //    {
        //        try
        //        {
        //            //调用委托,获取返回值等处理
        //            item("遍历调用");
        //        }
        //        catch (Exception e)
        //        {
        //            //异常处理
        //            Console.WriteLine("执行方法{0}有异常", item.Method.Name);

        //        }
        //    }
        //    Console.ReadLine();
        //}
        //public static void SendMessage(string content)
        //{
        //    Console.WriteLine(content);
        //}
        //public static void SendEmial(string content)
        //{
        //    throw new Exception("抛出了一个异常");
        //    //Console.WriteLine(content);
        //}
        //public static void SendQQ(string content)
        //{
        //    Console.WriteLine(content);
        //}
        #endregion

        #region 泛型委托
        /*http://www.cnblogs.com/maitian-lf/p/3671782.html*/
        //Action、 Action<T>、Func<T>、Predicate<T>
        //static void Main(string[] args)
        //{
        //    SendNotice("无返回值泛型委托Action<T>***", SendMessage);
        //    Console.WriteLine("有参数有返回值泛型委托Func<T>***求和{0}", Result(2, 3, Sum));
        //    Console.WriteLine("返回值始终为bool型的泛型委托Predicate<T>***最大？{0}", IsMax(3, 4));
        //    Console.ReadLine();
        //}


        //public static int Sum(int a, int b)
        //{
        //    return a + b;
        //}

        //public static bool IsMax(int a, int b)
        //{
        //    return a > b;
        //}

        //public static void SendNotice(string content, Action<string> sample)
        //{
        //    sample(content);
        //}

        //public static int Result(int a, int b, Func<int, int, int> func)
        //{
        //    return func(a, b);
        //}


        //public static void SendMessage(string content)
        //{
        //    Console.WriteLine(content);
        //}

        //public static void SendEmial(string content)
        //{
        //    Console.WriteLine(content);
        //}

        //public void SendQQ(string content)
        //{
        //    Console.WriteLine(content);
        //} 


        #endregion

        #region 委托 反射


        //static void Main(string[] args)
        //{
        //    //将delType参数转换为一个委托类型
        //    Type delType = Type.GetType("PracticeDemo.TwoInt32s");

        //    if (delType == null)
        //    {
        //        Console.WriteLine("Invalid delType argument:" + args[0]);
        //        return;
        //    }
        //    delType.GetMethods();
        //    //获取Add方法
        //    //返回所有public方法 BindingFlags.Instance|BindFlags.NonPublic|BindFlags.static   实例方法|非public|静态方法
        //    //MethodInfo method = (from m in new Program().GetType().GetMethods()
        //    //                     let param = m.GetParameters()
        //    //                     let methodName = m.Name
        //    //                     where methodName == "Subtract" && param != null && param.Length == 2
        //    //                     select m).First();

        //    Delegate d;
        //    try
        //    {
        //        //将Arg1参数转换为一个方法
        //        MethodInfo mi = typeof(Program).GetMethod("Add", BindingFlags.NonPublic | BindingFlags.Static);
        //        //创建包装了静态方法的一个委托对象
        //        d = Delegate.CreateDelegate(delType, mi);
        //        Console.WriteLine("调用委托求和结果：{0}", d.DynamicInvoke(1, 3));
        //    }
        //    catch (ArgumentException e)
        //    {
        //        return;
        //    }

        //    Console.ReadLine();
        //}

        //private static Object Add(Int32 n1, Int32 n2)
        //{
        //    return n1 + n2;
        //}

        //public Object Subtract(Int32 n1, Int32 n2)
        //{
        //    return n1 - n2;
        //}

        //private Object NumChars(string s)
        //{
        //    return s.Length;
        //}

        //public static Object Reverse(String s)
        //{
        //    Char[] chars = s.ToCharArray();
        //    Array.Reverse(chars);
        //    return new String(chars);
        //}

        //public static Object Reverse1(String s)
        //{
        //    Char[] chars = s.ToCharArray();
        //    Array.Reverse(chars);
        //    return new String(chars);
        //}

        #endregion

        #region 委托内部机制

        //委托类型隐含为 sealed，所以不允许从一个委托类型派生任何类型。
        /*
         * 委托实例封装了一个调用列表，该列表列出了一个或多个方法，
         * 每个方法称为一个可调用实体。对于实例方法，可调用实体由该方法和一个相关联的实例组成。对于静态方法，可调用实体仅由一个方法组成。用一个适当的参数集来调用一个委托实例，就是用此给定的参数集来调用该委托实例的每个可调用实体。
         */

        delegate Object DelegateCallBack(FileStream s);
        public int CallBackMethod(Stream s)
        {
            //dosomthing
            return 1;
        }
        public delegate void DelegateSample(string content);
        static void Main(string[] args)
        {
            DelegateSample x = new Program().SendMessage;
            DelegateSample sample = new DelegateSample(new Program().SendQQ);
            sample += SendEmial;
            sample += new Program().SendMessage;
            DelegateSample s = SendEmial;//与 sample 链中SendEmial同一个对象
            sample.Invoke("通知");
            new Program().SendNotice("QQ通知:*****", x);
            Console.ReadLine();
        }

        public void SendMessage(string content)
        {
            Console.WriteLine(content);
        }

        public void SendNotice(string content, DelegateSample sample)
        {
            sample(content);
        }



        

        public static void SendEmial(string content)
        {
            Console.WriteLine(content);
        }

        public void SendQQ(string content)
        {
            Console.WriteLine(content);
        }

        #endregion



    }

}
