using Data.ControllerSQL;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main()
        {
            //SendValues_test.run();
            var v = TableUsers_CD.Get(2).NAME;
            Console.WriteLine(v);
            Console.ReadKey();
        }
    }
}
