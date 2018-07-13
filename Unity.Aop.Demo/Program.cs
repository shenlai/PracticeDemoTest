using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity.Aop.Demo
{
    class Program
    {
        //http://www.cnblogs.com/luminji/archive/2012/01/10/2318211.html

        //Unity: http://www.cnblogs.com/artech/archive/2010/09/01/1815221.html
        //http://www.cnblogs.com/kyo-yo/archive/2010/12/27/Learning-EntLib-Tenth-Decoupling-Your-System-Using-The-Unity-PART5-Use-Unity-Interceptor.html

        //Munq IOC: http://my.oschina.net/hongjita/blog/150552
        static void Main(string[] args)
        {

            var container = new UnityContainer();
            
            container.AddNewExtension<Interception>()
                .RegisterType<ISample, Sample1>();

            //接口
            container.Configure<Interception>()
                .SetInterceptorFor<ISample>(new InterfaceInterceptor());
            var sample = container.Resolve<ISample>(); /****获得一个代理类实例*****/
            sample.DoSomething();
            sample.DoSomethingNoAop();

            /*虚拟类*/
            container.Configure<Interception>().SetInterceptorFor<SampleClass>(new VirtualMethodInterceptor());
            var samp = container.Resolve<SampleClass>();
            samp.Sample();
            samp.SampleVirtual();

            Console.ReadLine();



        }
    }
}
