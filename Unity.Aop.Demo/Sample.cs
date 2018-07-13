using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity.Aop.Demo
{
    #region 反射机制
    #endregion


    #region 针对接口
    public interface ISample
    {
        [AopHandler]
        void DoSomething();
        void DoSomethingNoAop();
    }

    public class Sample1 : ISample
    {
        public void DoSomething()
        {
            Console.WriteLine("Sample1 do something");
        }

        public void DoSomethingNoAop()
        {
            Console.WriteLine("Sample1 do something no aop");
        }
    }
    #endregion

    #region 针对虚方法
    public class SampleClass
    {

        [AopHandler]
        public virtual void SampleVirtual()
        {
            Console.WriteLine("Virtual method");
        }

        public void Sample()
        {
            Console.WriteLine("Sampe method");
        }
    }
    #endregion
}
