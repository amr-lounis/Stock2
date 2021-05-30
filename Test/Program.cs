
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        int b = 0; 
        static void Main(string[] args)
        {
            B b= new B();
            b.run();
        }
    }
    class B
    {
        public int v = 0;
        public void run()
        {
            A a = new A();
            a.rece(this,10);
        }
    }
    class A
    {
        public int v1 = 0;
        public void rece(object _sender,int _i)
        {
            v1 = _i;
            var b = _sender as B;
            b.v = 100;
        }
    }
}
