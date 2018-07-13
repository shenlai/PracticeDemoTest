using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity.Aop.Demo
{
    public class AopHandler:ICallHandler
    {
        public int Order { get; set; }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            Console.WriteLine("MethodName:{0}", input.MethodBase.Name);
            Console.WriteLine("Params:");
            for(int i=0;i<input.Arguments.Count;i++)
            {
                Console.WriteLine("{0}:{1}", input.Arguments.ParameterName(i), input.Arguments[i]);
            }

            Console.WriteLine("before:do something");
            var retvalue = getNext()(input, getNext);
            Console.WriteLine("after:do something");
            return retvalue;

        }
    }

    public class AopHandlerAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(Microsoft.Practices.Unity.IUnityContainer container)
        {
            return new AopHandler();
        }
    }
}
