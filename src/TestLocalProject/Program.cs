using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLocalProject
{
    class Program
    {
        static void Main(string[] args)
        {
            TestClass testClass = new TestClass();
            Should.Throw<PersonalizedException>(() => { testClass.TestMethod(); });

            Console.WriteLine("test");
            Console.ReadLine();
        }
    }
}
