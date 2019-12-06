using System;
using System.Collections.Generic;
using Shouldly;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> steList = new List<string> { "A2", "A1", "A1" };
            try
            {
                steList.ShouldAllBeEqual();

            } catch(Exception EX)
            {
                Console.WriteLine(EX.Message);
                Console.ReadLine();
            }
        }
    }
}
