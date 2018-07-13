using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitDemo
{
    class Program
    {
        static void Main(string[] args)
        {




        }


       

        public void Method()
        {
            #region Emit 动态创建
            /*
        * public class MyClass  
   {  
       private double a = 0;  
       private double b = 0;  
       public MyClass()  
       {  
       }  
       public MyClass(double a,double b)  
       {  
           this.a = a;  
           this.b = b;  
       }  

       public double Sum()  
       {  
           return a + b;  
       }  
       public double A  
       {  
           get  
           {  
               return a;  
           }  
           set  
           {  
               a = value;  
           }  
       }  
   }  
        */
            #endregion

            //构建程序集
            AssemblyName aName = new AssemblyName("MyEmit");
            AppDomain aDomain = AppDomain.CurrentDomain;



        }




        /// <summary>
        /// 【无返回值】无参方法SayHello()  反编译person.dll即可看见
        /// public class Person
        /// {
        /// // Methods
        ///     public void SayHello()
        ///     {
        ///         Console.WriteLine("蝈蝈");
        ///         Console.ReadLine();
        ///     }
        /// }
        /// </summary>
        public void Method1()
        {
            AssemblyName assemblyName = new AssemblyName("assemblyName");
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("PersonModule", "Person.dll");
            TypeBuilder typeBuilder = moduleBuilder.DefineType("Person", TypeAttributes.Public);
            //*【不同点1】
            MethodBuilder sayHelloMethod = typeBuilder.DefineMethod("SayHello", MethodAttributes.Public, null, null);
            ILGenerator ilOfSayHello = sayHelloMethod.GetILGenerator();
            ilOfSayHello.Emit(OpCodes.Ldstr, "蝈蝈");
            ilOfSayHello.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
            ilOfSayHello.Emit(OpCodes.Call, typeof(Console).GetMethod("ReadLine"));
            //没有返回值所有加OpCodes.Pop
            ilOfSayHello.Emit(OpCodes.Pop);
            ilOfSayHello.Emit(OpCodes.Ret);
            Type personType = typeBuilder.CreateType();
            assemblyBuilder.Save("Person.dll");
            object obj = Activator.CreateInstance(personType);
            MethodInfo methodInfo = personType.GetMethod("SayHello");
            //【不同点2】
            methodInfo.Invoke(obj, null);
        }

    }
}
