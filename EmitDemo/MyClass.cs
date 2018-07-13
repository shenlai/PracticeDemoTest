using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmitDemo
{
    public class MyClass
    {
        private double a = 0;
        private double b = 0;
        public MyClass()
        {
        }
        public MyClass(double a, double b)
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
}
